﻿using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
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

		#region DENORMALIZED VIEWS

		public DbSet<CaTimelineView> CaTimelineViews { get; set; }

		public DbSet<ScrubbingInfo> ScrubbingInfo { get; set; }

		public DbSet<NotificationInfo> NotificationsInfo { get; set; }

        public DbSet<ResponseInfo> ResponsesInfo { get; set; }

        public DbSet<InstructionInfo> InstructionsInfo { get; set; }

		public DbSet<PaymentInfo> PaymentsInfo { get; set; }

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
