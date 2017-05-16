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

		#region DENORMALIZED VIEWS

		public DbSet<CaTimelineView> CaTimelineViews { get; set; }

		public DbSet<Scrubbing> Scrubbing { get; set; }

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
