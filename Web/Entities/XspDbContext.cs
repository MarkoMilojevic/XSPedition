using System.Data.Entity;
using Web.Entities.Core;
using Web.Entities.Denormalized;
using Web.Entities.Lookups;
using Web.Entities.System;

namespace Web.Entities
{
	public class XspDbContext : DbContext
	{
		public XspDbContext() //: base("AzureConnection")
		{
			
		}

		#region CORE

		public DbSet<Account> Accounts { get; set; }
		public DbSet<Balance> Balances { get; set; }
		public DbSet<CorporateAction> CorporateActions { get; set; }
		public DbSet<FieldValue> FieldValues { get; set; }
		public DbSet<Option> Options { get; set; }
		public DbSet<Payment> Payments { get; set; }
		public DbSet<Payout> Payouts { get; set; }
		public DbSet<Response> Responses { get; set; }

		#endregion CORE

		#region DENORMALIZED VIEWS

		public DbSet<CaTimelineView> CaTimelineViews { get; set; }
		public DbSet<ScrubbingProcessView> ScrubbingProcessViews { get; set; }
		public DbSet<NotificationProcessView> NotificationProcessViews { get; set; }
		public DbSet<ResponseProcessView> ResponseProcessViews { get; set; }
		public DbSet<InstructionProcessView> InstructionProcessViews { get; set; }
		public DbSet<PaymentProcessView> PaymentProcessViews { get; set; }

		#endregion DENORMALIZED VIEWS

		#region LOOKUPS

		public DbSet<CaTypeLookup> CaTypeLookups { get; set; }
		public DbSet<FieldLookup> FieldLookups { get; set; }
		public DbSet<OptionTypeLookup> OptionTypeLookups { get; set; }
		public DbSet<PayoutTypeLookup> PayoutTypeLookups { get; set; }
		public DbSet<ProcessTypeLookup> ProcessTypeLookups { get; set; }

		#endregion LOOKUPS

		#region SYSTEM

		public DbSet<DatesConfiguration> DatesConfigurations { get; set; }
		public DbSet<EventLog> EventLogs { get; set; }

		#endregion SYSTEM

	}
}
