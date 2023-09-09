namespace Domain.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class BranchEntity
    {
        public int BranchId { get; set; }
        public int Code { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Identification { get; set; }
        public DateTime CreateDate { get; set; }

        [ForeignKey("Currency")]
        public int CurrencyId { get; set; }
        public CurrencyEntity Currency { get; set; }
    }
}
