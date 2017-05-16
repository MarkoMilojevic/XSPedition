using System;
using Web.Entities.Core;
using Web.Entities.Denormalized;
using Web.Entities.Registries;
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
            /*
			context.CaTypeRegistry.AddOrUpdate(lu => lu.CaTypeRegistryId,
				new CaTypeRegistry { CaTypeRegistryId = 1, Code = "RES" }
			);

			context.CorporateActions.AddOrUpdate(ca => ca.CaId, 
				new CorporateAction { CaId = 1, CaTypeRegistryId = 1 }
		    );

			context.FieldRegistry.AddOrUpdate(lu => lu.FieldRegistryId,
				new FieldRegistry { FieldRegistryId = 1, FieldType = "DATE", FieldDisplay = "Ex Date" }
			);

	        context.FieldValues.AddOrUpdate(fld => fld.FieldValueId, 
				new FieldValue { FieldValueId = 1, CaId = 1, FieldRegistryId = 1, IsScrubbed = false }
		    );

	        context.ProcessTypeRegistry.AddOrUpdate(lu => lu.ProcessTypeRegistryId,
		        new ProcessTypeRegistry {ProcessTypeRegistryId = 1, Type = ProcessType.Scrubbing, Display = "CA Scrubbing"}
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

	        context.ScrubbingProcessViews.AddOrUpdate(spv => spv.ScrubbingProcessViewId, 
				new ScrubbingProcessView
				{
					ScrubbingProcessViewId = 1,
					CaId = 1,
					FieldRegistryId = 1,
					IsSrubbed = false,
					FieldDisplay = "Ex Date (IN)",
					ProcessedDateCategory = ProcessedDateCategory.Missing
				}
		    );
            */
        }
    }
}


/*

--INSERT ACCOUNTS
INSERT INTO [dbo].[Accounts]
           ([AccountNumber]
           ,[AccountOwner]
		   ,[BusinessEntity])
     VALUES
           ('01LFXA','', 'APAC'),
		   ('01LMCD','', 'NA'),
		   ('GU020446','', 'EMEA'),
		   ('0ST71','', 'APAC'),
		   ('102213A','', 'NA'),
		   ('10DS855','', 'NA'),
		   ('20122557','', 'NA'),
		   ('220892IR','', 'EMEA'),
		   ('260941','', 'NA'),
		   ('FC12B','', 'NA')


--INSERT CATYPELOOKUPS
INSERT INTO [dbo].[CaTypeLookups]
           ([CaTypeLookupId], [Code])
     VALUES
           (1, 'DRIP'), (2, 'DVOP'), (3, 'PINK'), (4, 'CONV'),
		   (5, 'PCAL'), (6, 'RHTS'), (7, 'TEND'), (8, 'INTR'),
		   (9, 'DVCA')

--INSERT OPTIONTYPELOOKUPS
INSERT INTO [dbo].[OptionTypeLookups]
           ([OptionTypeLookupId], [Code])
     VALUES
           (1, 'Cash'), (2, 'Security'), (3, 'Cash and Security'), (4, 'Exercise'),
		   (5, 'No Action')

--INSERT PAYOUTTYPELOOKUPS
INSERT INTO [dbo].[PayoutTypeLookups]
           ([PayoutTypeLookupId], [Code])
     VALUES
           (1, 'Principal Cash'), (2, 'Security'), (3, 'Interest'), (4, 'Dividend')

--INSERT FIELDLOOKUPS
INSERT INTO [dbo].[FieldLookups]
           ([FieldLookupId]
		   ,[FieldDisplay]
           ,[FieldType])
     VALUES
			--CA Polja, jos nisu poredana po tipovima
           (1,'Announcement Date','DATE'),
		   (2,'Base Denomination','NUMBER'),
		   (3,'CA Cancelled','BOOL'),
		   (4,'Effective Date','DATE'),
		   (5,'Ex Date','DATE'),
		   (6,'Interest Period','STRING'),
		   (7,'Lottery Date','DATE'),
		   (8,'Offeror','STRING'),
		   (9,'Publication Date','DATE'),
		   (10,'Record Date','DATE'),

		   --Option polja
		   (101,'Expiration Date','DATE'),
		   (102,'Market date','DATE'),
		   (103,'Minimum Quantity To Instruct','NUMBER'),
		   (104,'Option Active','BOOL'),
		   (105,'Proration Rate','NUMBER'),
		   (106,'Response Due Date','DATE'),
		   (107,'Subscription Date','DATE'),

		   --Payout polja
		   (1001,'Currency','STRING'),
		   (1002,'Fractional Share Rule','NUMBER'),
		   (1003,'Gross Amount','NUMBER'),
		   (1004,'Interest Rate','NUMBER'),
		   (1005,'Net Amount','NUMBER'),
		   (1006,'New Shares','NUMBER'),
		   (1007,'Old Shares','NUMBER'),
		   (1008,'Payable Date','DATE'),
		   (1009,'Payment Date','DATE'),
		   (1010,'Payout Security ID','STRING'),
		   (1011,'Payout Security ID Type','STRING'),
		   (1012,'Price','NUMBER'),
		   (1013,'Rate Type','STRING'),
		   (1014,'Value Date','DATE'),
		   (1015,'Withholding Tax Rate','NUMBER') 
*/
