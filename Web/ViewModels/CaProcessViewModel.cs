using System.Collections.Generic;
using Web.Entities.Shared;

namespace Web.ViewModels
{
	public class CaProcessViewModel
	{
	    public CaProcessViewModel(ProcessType processType, List<string> targetDateItems, List<string> criticalDateItems, List<string> lateDateItems, List<string> missingItems, int processedItemCount, int totalItemCount)
	    {
	        switch (processType)
	        {
	            case ProcessType.Scrubbing:
	                this.id = "scrub";
                    this.title = "CA Scrubbing";
                    this.processActionName = "Scrubbed";
                    this.processType = ProcessType.Scrubbing;
                    break;
	            case ProcessType.Notification:
                    this.id = "notif";
                    this.title = "Notification";
                    this.processActionName = "Sent";
                    this.processType = ProcessType.Notification;
                    break;
	            case ProcessType.Response:
                    this.id = "respo";
                    this.title = "Response";
                    this.processActionName = "Submitted";
                    this.processType = ProcessType.Response;
                    break;
	            case ProcessType.Instruction:
                    this.id = "instr";
                    this.title = "Instruction";
                    this.processActionName = "Instructed";
                    this.processType = ProcessType.Instruction;
                    break;
	            case ProcessType.Payment:
                    this.id = "payme";
                    this.title = "Payment";
                    this.processActionName = "Settled";
                    this.processType = ProcessType.Payment;
                    break;
	        }

	        this.targetDateItems = targetDateItems;
	        this.criticalDateItems = criticalDateItems;
	        this.lateDateItems = lateDateItems;
	        this.missingItems = missingItems;
            this.processedItemCount = processedItemCount;
	        this.totalItemCount = totalItemCount;
            this.processPercentage = (((decimal)this.processedItemCount) / this.totalItemCount) * 100;
        }
        
		public readonly string id;

        public readonly string title;

        public readonly List<string> targetDateItems;

        public readonly List<string> criticalDateItems;

        public readonly List<string> lateDateItems;
        
        public readonly List<string> missingItems;

        public readonly string processActionName;

        public readonly int totalItemCount;

        public readonly int processedItemCount;

        public readonly decimal processPercentage;

        public readonly ProcessType processType;
    }
}