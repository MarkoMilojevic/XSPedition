using System;
using System.Collections.Generic;
using Web.Entities.Shared;

namespace Web.DTO
{
	public class CaProcess
	{
	    public CaProcess(ProcessType processType, List<string> targetDateItems, List<string> criticalDateItems, int processedItemCount, int totalItemCount)
	    {
	        switch (processType)
	        {
	            case ProcessType.Scrubbing:
	                this.id = "scrub";
                    this.title = "CA Scrubbing";
                    this.processActionName = "Scrubbed";
	                break;
	            case ProcessType.Notification:
                    this.id = "notif";
                    this.title = "Notification";
                    this.processActionName = "Sent";
                    break;
	            case ProcessType.Response:
                    this.id = "respo";
                    this.title = "Response";
                    this.processActionName = "Submitted";
                    break;
	            case ProcessType.Instruction:
                    this.id = "instr";
                    this.title = "Instruction";
                    this.processActionName = "Instructed";
                    break;
	            case ProcessType.Payment:
                    this.id = "payme";
                    this.title = "Payment";
                    this.processActionName = "Settled";
                    break;
	        }

	        this.targetDateItems = targetDateItems;
	        this.criticalDateItems = criticalDateItems;
	        this.processedItemCount = processedItemCount;
	        this.totalItemCount = totalItemCount;
            this.processPercentage = (((decimal)this.processedItemCount) / this.totalItemCount) * 100;
        }
        
		public readonly string id;

        public readonly string title;

        public readonly List<string> targetDateItems;

        public readonly List<string> criticalDateItems;

        public readonly string processActionName;

        public readonly int totalItemCount;

        public readonly int processedItemCount;

        public readonly decimal processPercentage;
	}
}