using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Http;
using Web.DTO;
using Web.Entities;
using Web.Entities.Core;
using Web.Entities.Denormalized;
using Web.Entities.Registries;
using Web.Entities.Shared;
using Web.Models.Shared;
using Web.ViewModels;

namespace Web.Controllers
{
	public class EventsController : ApiController
	{
		private readonly XspDbContext _context;

		public EventsController()
		{
			_context = new XspDbContext();
		}
        
		#region SCRUBBING

		#region COMMAND

		[HttpPost]
		[Route("api/events/scrubbingcommand")]
		public IHttpActionResult PostScrubbing([FromBody] ScrubCaCommand command)
		{
			CorporateAction ca = _context.CorporateActions.SingleOrDefault(c => c.CaId == command.CaId);
			if (ca == null)
			{
				CreateCorporateActionAndFields(command);
			}
			else
			{
				SetFieldValues(command, ca);
			}

			return Ok();
		}

		private void CreateCorporateActionAndFields(ScrubCaCommand command)
		{
			CorporateAction ca = CreateCorporateAction(command);
			CreateFields(ca);
			SetFieldValues(command, ca);
		}

		private CorporateAction CreateCorporateAction(ScrubCaCommand scrubbingEvent)
		{
			CorporateAction ca = new CorporateAction();

			ca.CaId = scrubbingEvent.CaId;
			ca.CaTypeRegistryId = scrubbingEvent.CaTypeId.Value;

			_context.CorporateActions.Add(ca);
			_context.SaveChanges();

			foreach (OptionDto optionDto in scrubbingEvent.Options)
			{
				Option option = new Option();
				option.CaId = scrubbingEvent.CaId;
				option.OptionNumber = optionDto.OptionNumber;
				option.OptionTypeRegistryId = optionDto.OptionTypeId.Value;

				_context.Options.Add(option);
				_context.SaveChanges();

				foreach (PayoutDto payoutDto in optionDto.Payouts)
				{
					Payout payout = new Payout();
					payout.OptionId = option.OptionId;
					payout.PayoutNumber = payoutDto.PayoutNumber;
					payout.PayoutTypeRegistryId = payoutDto.PayoutTypeId.Value;

					_context.Payouts.Add(payout);
				}

				_context.SaveChanges();
			}

			return _context.CorporateActions.Single(c => c.CaId == ca.CaId);
		}

		private void CreateFields(CorporateAction ca)
		{
			List<FieldRegistry> caFields = _context.FieldRegistry.Where(fld => fld.CaTypeRegistryId == ca.CaTypeRegistryId).ToList();
			_context.FieldValues.AddRange(caFields.Select(fld => new FieldValue
			{
				CaId = ca.CaId,
				FieldRegistryId = fld.FieldRegistryId,
				IsScrubbed = false
			}));

			foreach (Option option in ca.Options)
			{
				List<FieldRegistry> optionFields = _context.FieldRegistry.Where(field => field.OptionTypeRegistryId == option.OptionTypeRegistryId).ToList();
				_context.FieldValues.AddRange(optionFields.Select(fld => new FieldValue
				{
					OptionId = option.OptionId,
					FieldRegistryId = fld.FieldRegistryId,
					IsScrubbed = false
				}));

				foreach (Payout payout in option.Payouts)
				{
					List<FieldRegistry> payoutFields = _context.FieldRegistry.Where(field => field.PayoutTypeRegistryId == payout.PayoutTypeRegistryId).ToList();
					_context.FieldValues.AddRange(payoutFields.Select(fld => new FieldValue
					{
						PayoutId = payout.PayoutId,
						FieldRegistryId = fld.FieldRegistryId,
						IsScrubbed = false
					}));
				}
			}

			_context.SaveChanges();
		}

		private void SetFieldValues(ScrubCaCommand @event, CorporateAction ca)
		{
			if (@event.Fields != null)
			{
				foreach (KeyValuePair<int, string> pair in @event.Fields)
				{
					int fieldLookupId = pair.Key;
					string fieldValue = pair.Value;

					Expression<Func<FieldValue, bool>> expression = fld => fld.CaId == @event.CaId && fld.FieldRegistryId == fieldLookupId;
					SetFieldValue(fieldValue, expression);
				}
			}

			if (@event.Options != null)
			{
				foreach (OptionDto optionDto in @event.Options)
				{
					Option option = ca.Options.SingleOrDefault(opt => opt.OptionNumber == optionDto.OptionNumber);

					if (optionDto.Fields != null)
					{
						foreach (KeyValuePair<int, string> pair in optionDto.Fields)
						{
							int fieldLookupId = pair.Key;
							string fieldValue = pair.Value;

							Expression<Func<FieldValue, bool>> expression = fld => fld.OptionId == option.OptionId && fld.FieldRegistryId == fieldLookupId;
							SetFieldValue(fieldValue, expression);
						}
					}

					if (optionDto.Payouts != null)
					{
						foreach (PayoutDto payoutDto in optionDto.Payouts)
						{
							if (payoutDto.Fields != null)
							{
								Payout payout = option.Payouts.SingleOrDefault(p => p.PayoutNumber == payoutDto.PayoutNumber);
								foreach (KeyValuePair<int, string> pair in payoutDto.Fields)
								{
									int fieldLookupId = pair.Key;
									string fieldValue = pair.Value;

									Expression<Func<FieldValue, bool>> expression = fld => fld.PayoutId == payout.PayoutId && fld.FieldRegistryId == fieldLookupId;
									SetFieldValue(fieldValue, expression);
								}
							}
						}
					}
				}
			}

			_context.SaveChanges();
		}

		private void SetFieldValue(string fieldValue, Expression<Func<FieldValue, bool>> expression)
		{
			FieldValue field = _context.FieldValues.SingleOrDefault(expression);
			if (field != null)
			{
				field.IsScrubbed = true;
				if (field.Field.FieldType == "DATE")
				{
					field.Value = fieldValue;
				}
			}

			
			_context.EventLogs.Add(new EventLog
			{
				// TODO
			});
			
    }

#endregion COMMAND

    #region EVENT

    [HttpPost]
		[Route("api/events/scrubbingevent")]
		public IHttpActionResult PostScrubbing([FromBody] CaScrubbedEvent @event)
		{
			CorporateAction ca = _context.CorporateActions.SingleOrDefault(c => c.CaId == @event.CaId);

			CaTimelineView timelineView = _context.CaTimelineViews.SingleOrDefault(ctv => ctv.CaId == @event.CaId);
			if (timelineView == null)
			{
				// TODO
				// CREATE CA TIMELINE VIEW
			}

			List<ScrubbingProcessView> scrubbingViews = _context.ScrubbingProcessViews.Where(view => view.CaId == @event.CaId).ToList();
			if (scrubbingViews.Count == 0)
			{
				List<FieldValue> fields = _context.FieldValues.Where(fld => fld.CaId == ca.CaId).ToList();
				_context.ScrubbingProcessViews.AddRange(fields.Select(fld => new ScrubbingProcessView
				{
					CaId = ca.CaId,
					FieldRegistryId = fld.Field.FieldRegistryId,
					FieldDisplay = fld.Field.FieldDisplay + " (IN)",
					ProcessedDateCategory = ProcessedDateCategory.Missing,
					IsSrubbed = false
				}));

				foreach (Option option in ca.Options)
				{
					fields = _context.FieldValues.Where(fld => fld.OptionId == option.OptionId).ToList();
					_context.ScrubbingProcessViews.AddRange(fields.Select(fld => new ScrubbingProcessView
					{
						OptionId = option.OptionId,
						FieldRegistryId = fld.Field.FieldRegistryId,
						FieldDisplay = "Option #" + option.OptionNumber + " - " + fld.Field.FieldDisplay + " (IN)",
						ProcessedDateCategory = ProcessedDateCategory.Missing,
						IsSrubbed = false
					}));

					foreach (Payout payout in option.Payouts)
					{
						fields = _context.FieldValues.Where(fld => fld.PayoutId == payout.PayoutId).ToList();
						_context.ScrubbingProcessViews.AddRange(fields.Select(fld => new ScrubbingProcessView
						{
							PayoutId = payout.PayoutId,
							FieldRegistryId = fld.Field.FieldRegistryId,
							FieldDisplay = "Option #" + option.OptionNumber + " - " + "Payout #" + payout.PayoutNumber + " - " + fld.Field.FieldDisplay + " (IN)",
							ProcessedDateCategory = ProcessedDateCategory.Missing,
							IsSrubbed = false
						}));
					}
				}

				_context.SaveChanges();
			}

			List<FieldValue> flds = _context.FieldValues.Where(fld => fld.CaId == ca.CaId).ToList();
			foreach (Option option in ca.Options)
			{
				flds.AddRange(_context.FieldValues.Where(fld => fld.OptionId == option.OptionId));
				foreach (Payout payout in option.Payouts)
				{
					flds.AddRange(_context.FieldValues.Where(fld => fld.PayoutId == payout.PayoutId).ToList());
				}
			}

			foreach (FieldValue field in flds)
			{
				ScrubbingProcessView view;

				if (field.CaId != null)
				{
					scrubbingViews = _context.ScrubbingProcessViews.Where(v => v.CaId == @event.CaId).ToList();
					view = scrubbingViews.Single(v => v.CaId == field.CaId && v.FieldRegistryId == field.FieldRegistryId);
					if (view != null && view.IsSrubbed == false)
					{
						view.IsSrubbed = field.IsScrubbed;
						view.ProcessedDateCategory = GetProcessedDateCategory(@event.Date, timelineView.ScrubbingTarget, timelineView.ScrubbingCritical);
						view.FieldDisplay = view.FieldDisplay.Substring(0, view.FieldDisplay.Length - 4) + "(CO)";
					}
				}

				if (field.OptionId != null)
				{
					scrubbingViews = _context.ScrubbingProcessViews.Where(v => v.OptionId == field.OptionId).ToList();
					view = scrubbingViews.Single(v => v.OptionId == field.OptionId && v.FieldRegistryId == field.FieldRegistryId);
					if (view != null && view.IsSrubbed == false)
					{
						view.IsSrubbed = field.IsScrubbed;
						view.ProcessedDateCategory = GetProcessedDateCategory(@event.Date, timelineView.ScrubbingTarget, timelineView.ScrubbingCritical);
						view.FieldDisplay = view.FieldDisplay.Substring(0, view.FieldDisplay.Length - 4) + "(CO)";
					}
				}

				if (field.PayoutId != null)
				{
					scrubbingViews = _context.ScrubbingProcessViews.Where(v => v.PayoutId == field.PayoutId).ToList();
					view = scrubbingViews.Single(v => v.PayoutId == field.PayoutId && v.FieldRegistryId == field.FieldRegistryId);
					if (view != null && view.IsSrubbed == false)
					{
						view.IsSrubbed = field.IsScrubbed;
						view.ProcessedDateCategory = GetProcessedDateCategory(@event.Date, timelineView.ScrubbingTarget, timelineView.ScrubbingCritical);
						view.FieldDisplay = view.FieldDisplay.Substring(0, view.FieldDisplay.Length - 4) + "(CO)";
					}
				}
			}

			_context.SaveChanges();

			return Ok();
		}

		[HttpGet]
		[Route("api/events/scrubbing/{caId}")]
		public IHttpActionResult GetScrubbing(int caId)
		{
			CorporateAction ca = _context.CorporateActions.Single(c => c.CaId == caId);
			List<ScrubbingProcessView> scrubbingViews = _context.ScrubbingProcessViews.Where(view => view.CaId == caId).ToList();
			foreach (Option option in ca.Options)
			{
				scrubbingViews.AddRange(_context.ScrubbingProcessViews.Where(view => view.OptionId == option.OptionId));
				foreach (Payout payout in option.Payouts)
				{
					scrubbingViews.AddRange(_context.ScrubbingProcessViews.Where(view => view.PayoutId == payout.PayoutId));
				}
			}

			List<string> targetDateItems = scrubbingViews.Where(view => view.ProcessedDateCategory == ProcessedDateCategory.TargetDate).Select(spv => spv.FieldDisplay).ToList();
			List<string> criticalDateItems = scrubbingViews.Where(view => view.ProcessedDateCategory == ProcessedDateCategory.CriticalDate).Select(spv => spv.FieldDisplay).ToList();
			int processedItemCount = scrubbingViews.Count(view => view.IsSrubbed);
			int totalItemCount = scrubbingViews.Count;

			return Ok(new CaProcessViewModel(ProcessType.Scrubbing, targetDateItems, criticalDateItems, processedItemCount, totalItemCount));
		}

		private ProcessedDateCategory GetProcessedDateCategory(DateTime eventDate, DateTime targetDate, DateTime criticalDate)
		{
			if (eventDate <= targetDate)
			{
				return ProcessedDateCategory.TargetDate;
			}

			if (eventDate <= criticalDate)
			{
				return ProcessedDateCategory.CriticalDate;
			}

			return ProcessedDateCategory.LateDate;
		}

		#endregion EVENT

		#endregion SCRUBBING
        
	}
}
