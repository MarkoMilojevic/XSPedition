using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Web.DTO;
using Web.Entities;
using Web.Entities.Core;
using Web.Entities.Denormalized;
using Web.Entities.Lookups;
using Web.Entities.Shared;
using Web.Models.Shared;

namespace Web.Controllers
{
	public class EventsController : ApiController
	{
		private readonly XspDbContext _context;

		public EventsController()
		{
			_context = new XspDbContext();
		}

        [HttpGet]
        [Route("api/events/scrubbing/{caId}")]
        public IHttpActionResult GetScrubbing(int caId)
        {
            List<ScrubbingProcessView> scrubbingViews = _context.ScrubbingProcessViews.Where(cpv => cpv.CaId == caId).ToList();

            List<string> targetDateItems = scrubbingViews.Where(spv => spv.ProcessedDateCategory == ProcessedDateCategory.TargetDate).Select(spv => spv.FieldDisplay).ToList();
            List<string> criticalDateItems = scrubbingViews.Where(spv => spv.ProcessedDateCategory == ProcessedDateCategory.CriticalDate).Select(spv => spv.FieldDisplay).ToList();
            int processedItemCount = scrubbingViews.Count(spv => spv.IsSrubbed);
            int totalItemCount = scrubbingViews.Count;

            return Ok(new CaProcess(ProcessType.Scrubbing, targetDateItems, criticalDateItems, processedItemCount, totalItemCount));
        }

        [HttpPost]
        [Route("api/events/scrubbing")]
        public IHttpActionResult PostScrubbing([FromBody] ScrubbingEventDto scrubbingEvent)
		{
            try
            {
                if (scrubbingEvent == null)
                {
                    return BadRequest();
                }

                bool result = HandleScrubbingEvent(scrubbingEvent);
                if (!result)
                {
                    return BadRequest();
                }

                return Ok();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        #region SCRUBBING

        private bool HandleScrubbingEvent(ScrubbingEventDto scrubbingEvent)
		{
			bool success = UpdateSrubbingWriteStore(scrubbingEvent);
			if (!success)
			{
				return false;
			}

			return UpdateSrubbingReadStore(scrubbingEvent);
		}

		private bool UpdateSrubbingWriteStore(ScrubbingEventDto scrubbingEvent)
		{
		    CorporateAction corpAction = _context.CorporateActions.SingleOrDefault(ca => ca.CaId == scrubbingEvent.CaId);
		    if (corpAction == null)
		    {
		        corpAction = CreateCorporateAction(scrubbingEvent);
		        InitializeFields(corpAction);

		    }
		    else
		    {
		        corpAction = UpdateCorporateAction(corpAction, scrubbingEvent);
		    }
			return true;
		}

        private void InitializeFields(CorporateAction corpAction)
        {
            var CaFields = _context.FieldLookups.Where(field => field.CaTypeLookupId == corpAction.CaTypeLookupId).ToList();

            _context.FieldValues.AddRange(CaFields.Select(f => new FieldValue
            {
                CaId = corpAction.CaId,
                FieldLookupId = f.FieldLookupId,
                IsScrubbed = false
            }));

            foreach (Option opt in corpAction.Options)
            {
                var OptionFields = _context.FieldLookups.Where(field => field.OptionTypeLookupId == opt.OptionTypeLookupId).ToList();

                _context.FieldValues.AddRange(OptionFields.Select(f => new FieldValue
                {
                    OptionId = opt.OptionId,
                    FieldLookupId = f.FieldLookupId,
                    IsScrubbed = false
                }));

                foreach (Payout pay in opt.Payouts)
                {
                    var PayoutFields = _context.FieldLookups.Where(field => field.PayoutTypeLookupId == pay.PayoutTypeLookupId).ToList();

                    _context.FieldValues.AddRange(PayoutFields.Select(f => new FieldValue
                    {
                        PayoutId = pay.PayoutId,
                        FieldLookupId = f.FieldLookupId,
                        IsScrubbed = false
                    }));
                }
            }
            _context.SaveChanges();
        }

        private CorporateAction UpdateCorporateAction(CorporateAction corpAction, ScrubbingEventDto scrubbingEvent)
        {
            throw new NotImplementedException();
        }

        private CorporateAction CreateCorporateAction(ScrubbingEventDto scrubbingEvent)
        {
            CorporateAction corpAction = new CorporateAction
            {
                CaId = scrubbingEvent.CaId,
                CaTypeLookupId = scrubbingEvent.CaTypeId.Value
            };

            _context.CorporateActions.Add(corpAction);
            _context.SaveChanges();

            foreach (OptionDto optionDto in scrubbingEvent.Options)
            {
                Option option = new Option
                {
                    CaId = scrubbingEvent.CaId,
                    OptionNumber = optionDto.OptionNumber,
                    OptionTypeLookupId = optionDto.OptionTypeId.Value
                };

                _context.Options.Add(option);
                _context.SaveChanges();

                foreach (PayoutDto payoutDto in optionDto.Payouts)
                {
                    Payout payout = new Payout
                    {
                        OptionId = option.OptionId,
                        PayoutNumber = payoutDto.PayoutNumber,
                        PayoutTypeLookupId = payoutDto.PayoutTypeId.Value
                    };
                    
                    _context.Payouts.Add(payout);
                }

                _context.SaveChanges();
            }

            return corpAction;
        }




































        private bool UpdateSrubbingReadStore(ScrubbingEventDto @event)
		{
			CaTimelineView timelineView = _context.CaTimelineViews.SingleOrDefault(ctv => ctv.CaId == @event.CaId);
			if (timelineView == null)
			{
				return false;
			}

			ScrubbingProcessView scrubbingView = _context.ScrubbingProcessViews.SingleOrDefault(spv => spv.CaId == @event.CaId && spv.FieldLookupId == @event.TargetId);
			if (scrubbingView == null)
			{
				return false;
			}

			scrubbingView.IsSrubbed = @event.IsProcessed;
			scrubbingView.ProcessedDateCategory = GetProcessedDateCategory(@event, timelineView, scrubbingView);

			FieldLookup fieldLookup = _context.FieldLookups.Single(fld => fld.FieldLookupId == @event.TargetId);
			scrubbingView.FieldDisplay = fieldLookup.FieldDisplay + " (" + (scrubbingView.IsSrubbed ? "CO" : "IN") + ")";

			_context.SaveChanges();

			return true;
		}

	    #endregion SCRUBBING

		private ProcessedDateCategory GetProcessedDateCategory(ScrubbingEventDto @event, CaTimelineView timelineView, ScrubbingProcessView scrubbingView)
		{
			if (@event.EventDate <= timelineView.ScrubbingTarget)
			{
				return ProcessedDateCategory.TargetDate;
			}

			if (@event.EventDate <= timelineView.ScrubbingCritical)
			{
				return ProcessedDateCategory.CriticalDate;
			}

			return ProcessedDateCategory.LateDate;
		}
	}
}
