using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.Entities.Core
{
	public class Account
	{
		[Key]
		public int AccountId { get; set; }

		[Required]
		public string AccountNumber { get; set; }

		[Required]
		public string AccountOwner { get; set; }

        [Required]
        public string BusinessEntity { get; set; }

        public virtual List<Balance> Balances { get; set; }
	}
}
