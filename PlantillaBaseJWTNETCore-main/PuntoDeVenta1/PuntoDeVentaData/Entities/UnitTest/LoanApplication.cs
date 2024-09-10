using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities.UnitTest
{
    public class LoanApplication
    {
        [Key]
        public long Id { get; set; }
        [ForeignKey("Customer")]
        public long CustomerId { get; set; }
        public decimal Amount { get; set; }
        public bool IsApproved { get; set; }
        public virtual Customer Customer { get; set; }  
    }
}
