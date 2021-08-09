using System;
using System.ComponentModel.DataAnnotations;

namespace PointOS.DataAccess.Entities
{
    public class Currency
    {
        public int Id { get; set; }
        //[Required]
        //public Guid GuidValue { get; set; }
        [Required]
        [MaxLength(50)]
        public string CurrencyName { get; set; }
        [Required]
        [MaxLength(10)]
        public string Abbreviation { get; set; }
    }
}
