using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Web.Entities.Core;
using Web.Entities.Denormalized;
using Web.Entities.Registries;
using Web.Entities.Registries.Maps;
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

		#region REGISTRIES

		public DbSet<CaTypeRegistry> CaTypeRegistry { get; set; }
		public DbSet<FieldRegistry> FieldRegistry { get; set; }
		public DbSet<OptionTypeRegistry> OptionTypeRegistry { get; set; }
		public DbSet<PayoutTypeRegistry> PayoutTypeRegistry { get; set; }
		public DbSet<ProcessTypeRegistry> ProcessTypeRegistry { get; set; }
        public DbSet<CaTypeFieldMap> CaTypeFieldMap { get; set; }
        public DbSet<OptionTypeFieldMap> OptionTypeFieldMap { get; set; }
        public DbSet<PayoutTypeFieldMap> PayoutTypeFieldMap { get; set; }

        #endregion REGISTRIES

        #region SYSTEM

        public DbSet<DatesConfiguration> DatesConfigurations { get; set; }
		public DbSet<EventLog> EventLogs { get; set; }

		#endregion SYSTEM

	    protected override void OnModelCreating(DbModelBuilder modelBuilder)
	    {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
	}
}
