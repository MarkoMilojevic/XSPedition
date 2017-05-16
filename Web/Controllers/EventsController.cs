using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Web.DTO;
using Web.Entities;
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

        [HttpGet]
        [Route("api/events/scrubbing/{caId}")]
        public IHttpActionResult GetScrubbing(int caId)
        {
            List<ScrubbingInfo> scrubbingViews = _context.Scrubbing.Where(view => view.CaId == caId).ToList();
            List<string> targetDateItems = scrubbingViews.Where(view => view.ProcessedDateCategory == ProcessedDateCategory.TargetDate).Select(spv => spv.FieldDisplay).ToList();
            List<string> criticalDateItems = scrubbingViews.Where(view => view.ProcessedDateCategory == ProcessedDateCategory.CriticalDate).Select(spv => spv.FieldDisplay).ToList();
            int processedItemCount = scrubbingViews.Count(view => view.IsSrubbed);
            int totalItemCount = scrubbingViews.Count;

            return Ok(new CaProcessViewModel(ProcessType.Scrubbing, targetDateItems, criticalDateItems, processedItemCount, totalItemCount));
        }

        [HttpPost]
        [Route("api/events/scrubbing")]
        public IHttpActionResult PostScrubbing([FromBody] ScrubCaCommand command)
        {
            List<ScrubbingInfo> scrubbingViews = _context.Scrubbing.Where(view => view.CaId == command.CaId).ToList();
            if (scrubbingViews.Count == 0)
            {
                CreateScrubbingInfo(command);
            }

            UpdateScrubbingInfo(command);

            return Ok();
        }

        private void CreateScrubbingInfo(ScrubCaCommand command)
        {
            ScrubbingInfo info = null;
            
            List<int> caFieldRegistryIds = _context
                                            .CaTypeFieldMap
                                            .Where(map => map.CaTypeRegistryId == command.CaTypeId.Value)
                                            .Select(map => map.FieldRegistryId)
                                            .ToList();

            List<FieldRegistry> caFields = _context
                                            .FieldRegistry
                                            .Where(fld => caFieldRegistryIds.Contains(fld.FieldRegistryId))
                                            .ToList();

            foreach (FieldRegistry caField in caFields)
            {
                info = new ScrubbingInfo();
                info.CaId = command.CaId;
                info.CaTypeId = command.CaTypeId.Value;
                info.OptionNumber = null;
                info.OptionTypeId = null;
                info.PayoutNumber = null;
                info.PayoutTypeId = null;
                info.FieldDisplay = caField.FieldDisplay + " (IN)";
                info.ProcessedDateCategory = ProcessedDateCategory.Missing;
                info.IsSrubbed = false;

                _context.Scrubbing.Add(info);
            }

            foreach (OptionDto optionDto in command.Options)
            {
                List<int> optionFieldRegistryIds = _context
                                                    .OptionTypeFieldMap
                                                    .Where(map => map.OptionTypeRegistryId == optionDto.OptionTypeId.Value)
                                                    .Select(map => map.FieldRegistryId)
                                                    .ToList();

                List<FieldRegistry> optionFields = _context
                                                    .FieldRegistry
                                                    .Where(fld => optionFieldRegistryIds.Contains(fld.FieldRegistryId))
                                                    .ToList();

                foreach (FieldRegistry optionField in optionFields)
                {
                    info = new ScrubbingInfo();
                    info.CaId = command.CaId;
                    info.CaTypeId = command.CaTypeId.Value;
                    info.OptionNumber = optionDto.OptionNumber;
                    info.OptionTypeId = optionDto.OptionTypeId.Value;
                    info.PayoutNumber = null;
                    info.PayoutTypeId = null;
                    info.FieldDisplay = optionField.FieldDisplay + " (IN)";
                    info.ProcessedDateCategory = ProcessedDateCategory.Missing;
                    info.IsSrubbed = false;

                    _context.Scrubbing.Add(info);
                }

                foreach (PayoutDto payoutDto in optionDto.Payouts)
                {
                    List<int> payoutFieldRegistryIds = _context
                                                        .PayoutTypeFieldMap
                                                        .Where(map => map.PayoutTypeRegistryId == payoutDto.PayoutTypeId.Value)
                                                        .Select(map => map.FieldRegistryId)
                                                        .ToList();

                    List<FieldRegistry> payoutFields = _context
                                                        .FieldRegistry
                                                        .Where(fld => payoutFieldRegistryIds.Contains(fld.FieldRegistryId))
                                                        .ToList();

                    foreach (FieldRegistry payoutField in payoutFields)
                    {
                        info = new ScrubbingInfo();
                        info.CaId = command.CaId;
                        info.CaTypeId = command.CaTypeId.Value;
                        info.OptionNumber = optionDto.OptionNumber;
                        info.OptionTypeId = optionDto.OptionTypeId.Value;
                        info.PayoutNumber = payoutDto.PayoutNumber;
                        info.PayoutTypeId = payoutDto.PayoutTypeId.Value;
                        info.FieldDisplay = payoutField.FieldDisplay + " (IN)";
                        info.ProcessedDateCategory = ProcessedDateCategory.Missing;
                        info.IsSrubbed = false;

                        _context.Scrubbing.Add(info);
                    }
                }
            }

            _context.SaveChanges();
        }

        private void UpdateScrubbingInfo(ScrubCaCommand command)
        {
            CaTimelineView timeline = _context.CaTimelineViews.First(t => t.CaId == command.CaId);

            ScrubbingInfo info = null;

            if (command.Fields != null)
            {
                foreach (KeyValuePair<int, string> caField in command.Fields)
                {
                    info = new ScrubbingInfo();
                    info.CaId = command.CaId;
                    info.CaTypeId = command.CaTypeId != null ? command.CaTypeId.Value : GetCaTypeId(command.CaId);
                    info.OptionNumber = null;
                    info.OptionTypeId = null;
                    info.PayoutNumber = null;
                    info.PayoutTypeId = null;

                    string fieldDisplay = _context.FieldRegistry.Single(fld => fld.FieldRegistryId == caField.Key).FieldDisplay;
                    info.FieldDisplay = fieldDisplay.Substring(0, fieldDisplay.Length - 4) + " (CO)";

                    info.ProcessedDateCategory = GetProcessedDateCategory(@command.EventDate, timeline.ScrubbingTarget, timeline.ScrubbingCritical);
                    info.IsSrubbed = true;

                    _context.Scrubbing.Add(info);
                }
            }

            if (command.Options != null)
            {
                foreach (OptionDto optionDto in command.Options)
                {
                    if (optionDto.Fields != null)
                    {
                        foreach (KeyValuePair<int, string> optionField in optionDto.Fields)
                        {
                            info = new ScrubbingInfo();
                            info.CaId = command.CaId;
                            info.CaTypeId = command.CaTypeId != null ? command.CaTypeId.Value : GetCaTypeId(command.CaId);
                            info.OptionNumber = optionDto.OptionNumber;
                            info.OptionTypeId = optionDto.OptionTypeId != null ? optionDto.OptionTypeId.Value : GetOptionTypeId(command.CaId, optionDto.OptionNumber);
                            info.PayoutNumber = null;
                            info.PayoutTypeId = null;

                            string fieldDisplay = _context.FieldRegistry.Single(fld => fld.FieldRegistryId == optionField.Key).FieldDisplay;
                            info.FieldDisplay = fieldDisplay.Substring(0, fieldDisplay.Length - 4) + " (CO)";

                            info.ProcessedDateCategory = GetProcessedDateCategory(@command.EventDate, timeline.ScrubbingTarget, timeline.ScrubbingCritical);
                            info.IsSrubbed = true;

                            _context.Scrubbing.Add(info);
                        }
                    }

                    if (optionDto.Payouts != null)
                    {
                        foreach (PayoutDto payoutDto in optionDto.Payouts)
                        {
                            if (payoutDto.Fields != null)
                            {
                                foreach (KeyValuePair<int, string> payoutField in payoutDto.Fields)
                                {
                                    info = new ScrubbingInfo();
                                    info.CaId = command.CaId;
                                    info.CaTypeId = command.CaTypeId != null ? command.CaTypeId.Value : GetCaTypeId(command.CaId);
                                    info.OptionNumber = optionDto.OptionNumber;
                                    info.OptionTypeId = optionDto.OptionTypeId != null ? optionDto.OptionTypeId.Value : GetOptionTypeId(command.CaId, optionDto.OptionNumber);
                                    info.PayoutNumber = payoutDto.PayoutNumber;
                                    info.PayoutTypeId = payoutDto.PayoutTypeId != null ? payoutDto.PayoutTypeId.Value : GetPayoutTypeId(command.CaId, optionDto.OptionNumber, payoutDto.PayoutNumber);

                                    string fieldDisplay = _context.FieldRegistry.Single(fld => fld.FieldRegistryId == payoutField.Key).FieldDisplay;
                                    info.FieldDisplay = fieldDisplay.Substring(0, fieldDisplay.Length - 4) + " (CO)";

                                    info.ProcessedDateCategory = GetProcessedDateCategory(@command.EventDate, timeline.ScrubbingTarget, timeline.ScrubbingCritical);
                                    info.IsSrubbed = true;

                                    _context.Scrubbing.Add(info);
                                }
                            }
                        }
                    }
                }
            }

            _context.SaveChanges();
        }

        private int GetCaTypeId(int caId)
        {
            return _context.Scrubbing.First(s => s.CaId == caId && s.CaTypeId != null).CaTypeId.Value;
        }

        private int GetOptionTypeId(int caId, int optionNumber)
        {
            return _context.Scrubbing.First(s => s.CaId == caId && s.OptionNumber == optionNumber && s.OptionTypeId != null).OptionTypeId.Value;
        }

        private int GetPayoutTypeId(int caId, int optionNumber, int payoutNumber)
        {
            return _context.Scrubbing.First(s => s.CaId == caId && s.OptionNumber == optionNumber && s.PayoutNumber == payoutNumber && s.PayoutTypeId != null).PayoutTypeId.Value;
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

        #endregion SCRUBBING
    }
}
