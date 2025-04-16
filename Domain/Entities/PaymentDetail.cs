using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Entities
{
    public class PaymentDetail
    {
        public int Id { get; set; }
        public string PaymentMethod { get; set; } = null!;
        public double Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; set; }

        //Relation related Field
        public Order Order { get; set; } = null!;
        public int OrderId { get; set; }

    }
}
