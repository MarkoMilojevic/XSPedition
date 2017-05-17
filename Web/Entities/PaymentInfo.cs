﻿using System;
using System.ComponentModel.DataAnnotations;
using Xspedition.Common;

namespace Web.Entities
{
    public class PaymentInfo
    {
        [Key]
        public int PaymentInfoId { get; set; }

        public int CaId { get; set; }

        public int? CaTypeId { get; set; }

        public string VolManCho { get; set; }

        public string FieldDisplay { get; set; }

        public string AccountNumber { get; set; }

        public int OptionNumber { get; set; }

        public int PayoutNumber { get; set; }

        public DateTime? ProcessedDate { get; set; }

        public ProcessedDateCategory ProcessedDateCategory { get; set; }

        public bool IsSettled { get; set; }
    }
}