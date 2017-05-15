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

		public IHttpActionResult Get(int caId, int processTypeId, DateTime date, int targetId, bool isProcessed)
		{
			CaProcess result = null;

			ProcessTypeLookup processType = _context.ProcessTypeLookups.Single(pt => pt.Type == (ProcessType) processTypeId);
			CaEvent @event = new CaEvent {CaId = caId, ProcessTypeId = processTypeId, Date = date, TargetId = targetId, IsProcessed = isProcessed};
			switch (processType.Type)
			{
				case ProcessType.Scrubbing:
					result = HandleScrubbingEvent(@event);
					break;
				case ProcessType.Notification:
					break;
				case ProcessType.Response:
					break;
				case ProcessType.Instruction:
					break;
				case ProcessType.Payment:
					break;
			}

			if (result == null)
			{
				return NotFound();
			}

			return Ok(result);
		}

		#region SCRUBBING

		private CaProcess HandleScrubbingEvent(CaEvent @event)
		{
			bool success;
			
			success = UpdateSrubbingWriteStore(@event);
			if (!success)
			{
				return null;
			}

			success = UpdateSrubbingReadStore(@event);
			if (!success)
			{
				return null;
			}

			return ReadScrubbingReadStore(@event.CaId);
		}

		private bool UpdateSrubbingWriteStore(CaEvent @event)
		{
			FieldValue field = _context.FieldValues.SingleOrDefault(f => f.CaId == @event.CaId && f.FieldLookupId == @event.TargetId);
			if (field == null)
			{
				return false;
			}

			field.IsScrubbed = @event.IsProcessed;
			_context.SaveChanges();

			return true;
		}

		private bool UpdateSrubbingReadStore(CaEvent @event)
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
		private CaProcess ReadScrubbingReadStore(int caId)
		{
            List<ScrubbingProcessView> scrubbingViews = _context.ScrubbingProcessViews.Where(cpv => cpv.CaId == caId).ToList();
            
			var targetDateItems = scrubbingViews.Where(spv => spv.ProcessedDateCategory == ProcessedDateCategory.TargetDate).Select(spv => spv.FieldDisplay).ToList();
			var criticalDateItems = scrubbingViews.Where(spv => spv.ProcessedDateCategory == ProcessedDateCategory.CriticalDate).Select(spv => spv.FieldDisplay).ToList();
            var processedItemCount = scrubbingViews.Count(spv => spv.IsSrubbed);
            var totalItemCount = scrubbingViews.Count;

            return new CaProcess(ProcessType.Scrubbing, targetDateItems, criticalDateItems, processedItemCount, totalItemCount);
		}

		#endregion SCRUBBING

		private ProcessedDateCategory GetProcessedDateCategory(CaEvent @event, CaTimelineView timelineView, ScrubbingProcessView scrubbingView)
		{
			if (@event.Date <= timelineView.ScrubbingTarget)
			{
				return ProcessedDateCategory.TargetDate;
			}

			if (@event.Date <= timelineView.ScrubbingCritical)
			{
				return ProcessedDateCategory.CriticalDate;
			}

			return ProcessedDateCategory.LateDate;
		}
	}
}
