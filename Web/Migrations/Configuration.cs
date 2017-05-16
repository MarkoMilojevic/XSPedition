using System;
using Web.Entities.Core;
using Web.Entities.Denormalized;
using Web.Entities.Lookups;
using Web.Entities.Shared;
using Web.Models.Shared;

namespace Web.Migrations
{
	using System.Data.Entity.Migrations;

	internal sealed class Configuration : DbMigrationsConfiguration<Web.Entities.XspDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Web.Entities.XspDbContext context)
        {
			context.CaTypeLookups.AddOrUpdate(lu => lu.CaTypeLookupId,
				new CaTypeLookup { CaTypeLookupId = 1, Code = "RES" }
			);

			context.CorporateActions.AddOrUpdate(ca => ca.CaId, 
				new CorporateAction { CaId = 1, CaTypeLookupId = 1 }
		    );

			context.FieldLookups.AddOrUpdate(lu => lu.FieldLookupId,
				new FieldLookup { FieldLookupId = 1, CaTypeLookupId = 1, FieldType = "DATE", FieldDisplay = "Ex Date", IsRequired = true }
			);

	        context.FieldValues.AddOrUpdate(fld => fld.FieldValueId, 
				new FieldValue { FieldValueId = 1, CaId = 1, FieldLookupId = 1, IsScrubbed = false }
		    );

	        context.ProcessTypeLookups.AddOrUpdate(lu => lu.ProcessTypeLookupId,
		        new ProcessTypeLookup {ProcessTypeLookupId = 1, Type = ProcessType.Scrubbing, Display = "CA Scrubbing"}
			);

	        context.CaTimelineViews.AddOrUpdate(ctv => ctv.CaTimelineViewId, 
				new CaTimelineView
				{
					CaTimelineViewId = 1,
					CaId = 1,
					ScrubbingTarget = DateTime.Now.AddDays(5),
					ScrubbingCritical = DateTime.Now.AddDays(10),
					NotificationTarget = DateTime.Now.AddDays(15),
					NotificationCritical = DateTime.Now.AddDays(20),
					ResponseTarget = DateTime.Now.AddDays(25),
					ResponseCritical = DateTime.Now.AddDays(30),
					InstructionTarget = DateTime.Now.AddDays(35),
					InstructionCritical = DateTime.Now.AddDays(40),
					PaymentTarget = DateTime.Now.AddDays(45),
					PaymentCritical = DateTime.Now.AddDays(50),
				}
			);

	        context.Scrubbing.AddOrUpdate(spv => spv.ScrubbingProcessViewId, 
				new Scrubbing
				{
					ScrubbingProcessViewId = 1,
					CaId = 1,
					FieldLookupId = 1,
					IsSrubbed = false,
					FieldDisplay = "Ex Date (IN)",
					ProcessedDateCategory = ProcessedDateCategory.Missing
				}
		    );
        }
    }
}
