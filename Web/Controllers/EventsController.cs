using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Web.Entities;
using Web.Entities.Registries;
using Web.Entities.Shared;
using Web.ViewModels;
using Xspedition.Common;
using Xspedition.Common.Commands;
using Xspedition.Common.Dto;

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
            List<ScrubbingInfo> scrubbingInfo = _context.ScrubbingInfo.Where(view => view.CaId == caId).ToList();
            List<string> targetDateItems = scrubbingInfo.Where(view => view.ProcessedDateCategory == ProcessedDateCategory.TargetDate).Select(spv => spv.FieldDisplay).ToList();
            List<string> criticalDateItems = scrubbingInfo.Where(view => view.ProcessedDateCategory == ProcessedDateCategory.CriticalDate).Select(spv => spv.FieldDisplay).ToList();
            List<string> lateDateItems = scrubbingInfo.Where(view => view.ProcessedDateCategory == ProcessedDateCategory.LateDate).Select(spv => spv.FieldDisplay).ToList();
            List<string> missingItems = scrubbingInfo.Where(view => view.ProcessedDateCategory == ProcessedDateCategory.Missing).Select(spv => spv.FieldDisplay).ToList();

            int processedItemCount = scrubbingInfo.Count(view => view.IsSrubbed);
            int totalItemCount = scrubbingInfo.Count;

            return Ok(new CaProcessViewModel(ProcessType.Scrubbing, targetDateItems, criticalDateItems, lateDateItems, missingItems, processedItemCount, totalItemCount));
        }

        [HttpPost]
        [Route("api/events/scrubbing")]
        public IHttpActionResult PostScrubbing([FromBody] ScrubCaCommand command)
        {
            List<ScrubbingInfo> scrubbingViews = _context.ScrubbingInfo.Where(view => view.CaId == command.CaId).ToList();
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
                                            .Where(map => map.CaTypeRegistryId == command.CaTypeId)
                                            .Select(map => map.FieldRegistryId)
                                            .ToList();

            List<FieldRegistry> caFields = _context
                                            .FieldRegistry
                                            .Where(fld => caFieldRegistryIds.Contains(fld.FieldRegistryId))
                                            .ToList();

            foreach (FieldRegistry caField in caFields)
            {
                info = new ScrubbingInfo();
                info.FieldRegistryId = caField.FieldRegistryId;
                info.CaId = command.CaId;
                info.CaTypeId = command.CaTypeId;
                info.OptionNumber = null;
                info.OptionTypeId = null;
                info.PayoutNumber = null;
                info.PayoutTypeId = null;
                info.FieldDisplay = caField.FieldDisplay + " (IN)";
                info.ProcessedDateCategory = ProcessedDateCategory.Missing;
                info.IsSrubbed = false;

                _context.ScrubbingInfo.Add(info);
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
                    info.FieldRegistryId = optionField.FieldRegistryId;
                    info.CaId = command.CaId;
                    info.CaTypeId = command.CaTypeId;
                    info.OptionNumber = optionDto.OptionNumber;
                    info.OptionTypeId = optionDto.OptionTypeId.Value;
                    info.PayoutNumber = null;
                    info.PayoutTypeId = null;
                    info.FieldDisplay = "O #" + optionDto.OptionNumber + " - " + optionField.FieldDisplay + " (IN)";
                    info.ProcessedDateCategory = ProcessedDateCategory.Missing;
                    info.IsSrubbed = false;

                    _context.ScrubbingInfo.Add(info);
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
                        info.FieldRegistryId = payoutField.FieldRegistryId;
                        info.CaId = command.CaId;
                        info.CaTypeId = command.CaTypeId;
                        info.OptionNumber = optionDto.OptionNumber;
                        info.OptionTypeId = optionDto.OptionTypeId.Value;
                        info.PayoutNumber = payoutDto.PayoutNumber;
                        info.PayoutTypeId = payoutDto.PayoutTypeId.Value;
                        info.FieldDisplay = "O #" + optionDto.OptionNumber + " " + "P #" + payoutDto.PayoutNumber + " - " + payoutField.FieldDisplay + " (IN)";
                        info.ProcessedDateCategory = ProcessedDateCategory.Missing;
                        info.IsSrubbed = false;

                        _context.ScrubbingInfo.Add(info);
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
                    info = _context.ScrubbingInfo.Single(s => s.FieldRegistryId == caField.Key && s.CaId == command.CaId && s.OptionNumber == null && s.PayoutNumber == null);

                    info.PayoutTypeId = command.CaTypeId;
                    info.FieldDisplay = info.FieldDisplay.Substring(0, info.FieldDisplay.Length - 4) + " (CO)";
                    info.ProcessedDateCategory = GetProcessedDateCategory(@command.EventDate, timeline.ScrubbingTarget, timeline.ScrubbingCritical);
                    info.IsSrubbed = true;
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
                            info = _context.ScrubbingInfo.Single(s => s.FieldRegistryId == optionField.Key && s.CaId == command.CaId && s.OptionNumber == optionDto.OptionNumber && s.PayoutNumber == null);

                            info.PayoutTypeId = GetOptionTypeId(command.CaId, optionDto.OptionNumber);
                            info.FieldDisplay = info.FieldDisplay.Substring(0, info.FieldDisplay.Length - 4) + " (CO)";
                            info.ProcessedDateCategory = GetProcessedDateCategory(@command.EventDate, timeline.ScrubbingTarget, timeline.ScrubbingCritical);
                            info.IsSrubbed = true;
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
                                    info = _context.ScrubbingInfo.Single(s => s.FieldRegistryId == payoutField.Key && s.CaId == command.CaId && s.OptionNumber == optionDto.OptionNumber && s.PayoutNumber == payoutDto.PayoutNumber);

                                    info.PayoutTypeId = GetPayoutTypeId(command.CaId, optionDto.OptionNumber, payoutDto.PayoutNumber);
                                    info.FieldDisplay = info.FieldDisplay.Substring(0, info.FieldDisplay.Length - 4) + " (CO)";
                                    info.ProcessedDateCategory = GetProcessedDateCategory(@command.EventDate, timeline.ScrubbingTarget, timeline.ScrubbingCritical);
                                    info.IsSrubbed = true;
                                }
                            }
                        }
                    }
                }
            }

            _context.SaveChanges();
        }

        private int GetOptionTypeId(int caId, int optionNumber)
        {
            return _context.ScrubbingInfo.First(s => s.CaId == caId && s.OptionNumber == optionNumber && s.OptionTypeId != null).OptionTypeId.Value;
        }

        private int GetPayoutTypeId(int caId, int optionNumber, int payoutNumber)
        {
            return _context.ScrubbingInfo.First(s => s.CaId == caId && s.OptionNumber == optionNumber && s.PayoutNumber == payoutNumber && s.PayoutTypeId != null).PayoutTypeId.Value;
        }

        #endregion SCRUBBING

        #region NOTIFICATIONS

        [HttpGet]
        [Route("api/events/notifications/{caId}")]
        public IHttpActionResult GetNotifications(int caId)
        {
            List<NotificationInfo> notifications = _context.NotificationsInfo.Where(view => view.CaId == caId).ToList();
            List<string> targetDateItems = notifications.Where(view => view.ProcessedDateCategory == ProcessedDateCategory.TargetDate).Select(spv => spv.FieldDisplay).ToList();
            List<string> criticalDateItems = notifications.Where(view => view.ProcessedDateCategory == ProcessedDateCategory.CriticalDate).Select(spv => spv.FieldDisplay).ToList();
            List<string> lateDateItems = notifications.Where(view => view.ProcessedDateCategory == ProcessedDateCategory.LateDate).Select(spv => spv.FieldDisplay).ToList();
            List<string> missingItems = notifications.Where(view => view.ProcessedDateCategory == ProcessedDateCategory.Missing).Select(spv => spv.FieldDisplay).ToList();
            int processedItemCount = notifications.Count(view => view.IsSent && (view.ProcessedDateCategory == ProcessedDateCategory.TargetDate || view.ProcessedDateCategory == ProcessedDateCategory.CriticalDate));
            int totalItemCount = notifications.Count;

            return Ok(new CaProcessViewModel(ProcessType.Notification, targetDateItems, criticalDateItems, lateDateItems, missingItems, processedItemCount, totalItemCount));
        }

        [HttpPost]
        [Route("api/events/notifications")]
        public IHttpActionResult PostNotifications([FromBody] NotifyCommand command)
        {
            List<NotificationInfo> notifications = _context.NotificationsInfo.Where(view => view.CaId == command.CaId).ToList();
            if (notifications.Count == 0)
            {
                CreateNotificationsInfo(command);
            } else
            {
                UpdateNotificationsInfo(command);
            }

            return Ok();
        }

        private void CreateNotificationsInfo(NotifyCommand command)
        {
            foreach(NotificationDto notificationDto in command.Notifications)
            {
                NotificationInfo notification = new NotificationInfo();

                notification.CaId = command.CaId;
                notification.CaTypeId = command.CaTypeId;
                notification.VolManCho = command.VolManCho;
                notification.AccountNumber = notificationDto.AccountNumber;
                notification.Recipient = notificationDto.Recipient;
                notification.FieldDisplay = notificationDto.Recipient + " (" + notificationDto.AccountNumber + ")";

                if(notificationDto.IsSent)
                {
                    notification.ProcessedDateCategory = CalculateProcessedDateCategory(ProcessType.Notification, command.CaId, command.EventDate );
                    notification.IsSent = true;
                    notification.ProcessedDate = command.EventDate;
                }
                else
                {
                    notification.ProcessedDateCategory = ProcessedDateCategory.Missing;
                    notification.IsSent = false;
                    notification.ProcessedDate = null;
                }
                _context.NotificationsInfo.Add(notification);
            }
            _context.SaveChanges();
        }

        private void UpdateNotificationsInfo(NotifyCommand command)
        {
            foreach (NotificationDto notificationDto in command.Notifications)
            {
                NotificationInfo notification = _context.NotificationsInfo.FirstOrDefault(notif => notif.CaId == command.CaId && notif.AccountNumber == notificationDto.AccountNumber && notif.Recipient == notificationDto.Recipient);
                
                if(notification == null)
                {
                    continue;
                }

                if (notificationDto.IsSent)
                {
                    notification.ProcessedDateCategory = CalculateProcessedDateCategory(ProcessType.Notification, command.CaId, command.EventDate);
                    notification.IsSent = true;
                    notification.ProcessedDate = command.EventDate;
                }
                else
                {
                    notification.ProcessedDateCategory = ProcessedDateCategory.Missing;
                    notification.IsSent = false;
                    notification.ProcessedDate = null;
                }
            }

            _context.SaveChanges();
        }

        #endregion

        #region INSTRUCTIONS

        [HttpGet]
        [Route("api/events/instructions/{caId}")]
        public IHttpActionResult GetInstructions(int caId)
        {
            List<InstructionInfo> instructions = _context.InstructionsInfo.Where(view => view.CaId == caId).ToList();
            List<string> targetDateItems = instructions.Where(view => view.ProcessedDateCategory == ProcessedDateCategory.TargetDate).Select(spv => spv.FieldDisplay).ToList();
            List<string> criticalDateItems = instructions.Where(view => view.ProcessedDateCategory == ProcessedDateCategory.CriticalDate).Select(spv => spv.FieldDisplay).ToList();
            List<string> lateDateItems = instructions.Where(view => view.ProcessedDateCategory == ProcessedDateCategory.LateDate).Select(spv => spv.FieldDisplay).ToList();
            List<string> missingItems = instructions.Where(view => view.ProcessedDateCategory == ProcessedDateCategory.Missing).Select(spv => spv.FieldDisplay).ToList();
            int processedItemCount = instructions.Count(view => view.IsInstructed && (view.ProcessedDateCategory == ProcessedDateCategory.TargetDate || view.ProcessedDateCategory == ProcessedDateCategory.CriticalDate));
            int totalItemCount = instructions.Count;

            return Ok(new CaProcessViewModel(ProcessType.Instruction, targetDateItems, criticalDateItems, lateDateItems, missingItems, processedItemCount, totalItemCount));
        }

        [HttpPost]
        [Route("api/events/instructions")]
        public IHttpActionResult PostInstructions([FromBody] InstructCommand command)
        {
            List<InstructionInfo> instructions = _context.InstructionsInfo.Where(view => view.CaId == command.CaId).ToList();
            if (instructions.Count == 0)
            {
                CreateInstructionsInfo(command);
            }
            else
            {
                UpdateInstructionInfo(command);
            }

            return Ok();
        }

        private void CreateInstructionsInfo(InstructCommand command)
        {
            foreach (InstructionDto instructionDto in command.Instructions)
            {
                InstructionInfo instruction = new InstructionInfo();

                instruction.CaId = command.CaId;
                instruction.CaTypeId = command.CaTypeId;
                instruction.VolManCho = command.VolManCho;
                instruction.AccountNumber = instructionDto.AccountNumber;
                instruction.FieldDisplay = instructionDto.AccountNumber;

                if (instructionDto.IsInstructed)
                {
                    instruction.ProcessedDateCategory = CalculateProcessedDateCategory(ProcessType.Instruction, command.CaId, command.EventDate);
                    instruction.IsInstructed = true;
                    instruction.ProcessedDate = command.EventDate;
                }
                else
                {
                    instruction.ProcessedDateCategory = ProcessedDateCategory.Missing;
                    instruction.IsInstructed = false;
                    instruction.ProcessedDate = null;
                }
                _context.InstructionsInfo.Add(instruction);
            }
            _context.SaveChanges();
        }

        private void UpdateInstructionInfo(InstructCommand command)
        {
            foreach (InstructionDto instructionDto in command.Instructions)
            {
                InstructionInfo instruction = _context.InstructionsInfo.FirstOrDefault(ins => ins.CaId == command.CaId && ins.AccountNumber == instructionDto.AccountNumber);

                if (instruction == null)
                {
                    continue;
                }

                if (instructionDto.IsInstructed)
                {
                    instruction.ProcessedDateCategory = CalculateProcessedDateCategory(ProcessType.Instruction, command.CaId, command.EventDate);
                    instruction.IsInstructed = true;
                    instruction.ProcessedDate = command.EventDate;
                }
                else
                {
                    instruction.ProcessedDateCategory = ProcessedDateCategory.Missing;
                    instruction.IsInstructed = false;
                    instruction.ProcessedDate = null;
                }
            }

            _context.SaveChanges();
        }

        #endregion

        #region PAYMENTS

        [HttpGet]
        [Route("api/events/payments/{caId}")]
        public IHttpActionResult GetPayments(int caId)
        {
            List<PaymentInfo> payments = _context.PaymentsInfo.Where(view => view.CaId == caId).ToList();
            List<string> targetDateItems = payments.Where(view => view.ProcessedDateCategory == ProcessedDateCategory.TargetDate).Select(spv => spv.FieldDisplay).ToList();
            List<string> criticalDateItems = payments.Where(view => view.ProcessedDateCategory == ProcessedDateCategory.CriticalDate).Select(spv => spv.FieldDisplay).ToList();
            List<string> lateDateItems = payments.Where(view => view.ProcessedDateCategory == ProcessedDateCategory.LateDate).Select(spv => spv.FieldDisplay).ToList();
            List<string> missingItems = payments.Where(view => view.ProcessedDateCategory == ProcessedDateCategory.Missing).Select(spv => spv.FieldDisplay).ToList();
            int processedItemCount = payments.Count(view => view.IsSettled && (view.ProcessedDateCategory == ProcessedDateCategory.TargetDate || view.ProcessedDateCategory == ProcessedDateCategory.CriticalDate));
            int totalItemCount = payments.Count;

            return Ok(new CaProcessViewModel(ProcessType.Payment, targetDateItems, criticalDateItems, lateDateItems, missingItems, processedItemCount, totalItemCount));
        }

        [HttpPost]
        [Route("api/events/payments")]
        public IHttpActionResult PostPayments([FromBody] PayCommand command)
        {
            List<PaymentInfo> payments = _context.PaymentsInfo.Where(view => view.CaId == command.CaId).ToList();
            if (payments.Count == 0)
            {
                CreatePaymentInfo(command);
            }
            else
            {
                UpdatePaymentInfo(command);
            }

            return Ok();
        }

        private void CreatePaymentInfo(PayCommand command)
        {
            foreach (PaymentDto paymentDto in command.Payments)
            {
                PaymentInfo payment = new PaymentInfo();

                payment.CaId = command.CaId;
                payment.CaTypeId = command.CaTypeId;
                payment.VolManCho = command.VolManCho;
                payment.AccountNumber = paymentDto.AccountNumber;
                payment.FieldDisplay = "O #" + paymentDto.OptionNumber + " " + "P #" + paymentDto.PayoutNumber + " - " + paymentDto.AccountNumber;
                payment.OptionNumber = paymentDto.OptionNumber;
                payment.PayoutNumber = paymentDto.PayoutNumber;

                if (paymentDto.IsSettled)
                {
                    payment.ProcessedDateCategory = CalculateProcessedDateCategory(ProcessType.Payment, command.CaId, command.EventDate);
                    payment.IsSettled = true;
                    payment.ProcessedDate = command.EventDate;
                }
                else
                {
                    payment.ProcessedDateCategory = ProcessedDateCategory.Missing;
                    payment.IsSettled = false;
                    payment.ProcessedDate = null;
                }
                _context.PaymentsInfo.Add(payment);
            }
            _context.SaveChanges();
        }

        private void UpdatePaymentInfo(PayCommand command)
        {
            foreach (PaymentDto paymentDto in command.Payments)
            {
                PaymentInfo payment = _context.PaymentsInfo.FirstOrDefault(pay => pay.CaId == command.CaId && pay.AccountNumber == paymentDto.AccountNumber && pay.OptionNumber == paymentDto.OptionNumber && pay.PayoutNumber == paymentDto.PayoutNumber);

                if (payment == null)
                {
                    continue;
                }

                if (paymentDto.IsSettled)
                {
                    payment.ProcessedDateCategory = CalculateProcessedDateCategory(ProcessType.Payment, command.CaId, command.EventDate);
                    payment.IsSettled = true;
                    payment.ProcessedDate = command.EventDate;
                }
                else
                {
                    payment.ProcessedDateCategory = ProcessedDateCategory.Missing;
                    payment.IsSettled = false;
                    payment.ProcessedDate = null;
                }
            }

            _context.SaveChanges();
        }

        #endregion

        #region COMMON METHODS
        private ProcessedDateCategory CalculateProcessedDateCategory(ProcessType processType, int caId, DateTime eventDate)
        {
            DateTime targetDate = new DateTime();
            DateTime criticalDate = new DateTime();
            CaTimelineView caTimeline = _context.CaTimelineViews.SingleOrDefault(ctv => ctv.CaId == caId);

            if(caTimeline == null)
            {
                return ProcessedDateCategory.Missing;
            }

            switch (processType)
            {
                case ProcessType.Scrubbing:
                    targetDate = caTimeline.ScrubbingTarget;
                    criticalDate = caTimeline.ScrubbingCritical;
                    break;
                case ProcessType.Notification:
                    targetDate = caTimeline.NotificationTarget;
                    criticalDate = caTimeline.NotificationCritical;
                    break;
                case ProcessType.Response:
                    targetDate = caTimeline.ResponseTarget;
                    criticalDate = caTimeline.ResponseCritical;
                    break;
                case ProcessType.Instruction:
                    targetDate = caTimeline.InstructionTarget;
                    criticalDate = caTimeline.InstructionCritical;
                    break;
                case ProcessType.Payment:
                    targetDate = caTimeline.PaymentTarget;
                    criticalDate = caTimeline.PaymentCritical;
                    break;
            }

            return GetProcessedDateCategory(eventDate, targetDate, criticalDate);
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

        #endregion
    }
}
