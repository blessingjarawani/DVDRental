using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class RentalFilters
    {
        public int ? ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime ? RentalDateFrom { get; set; }
        public DateTime ? RentalDateTo { get; set; }
        public int? DaysOverDue { get; set; } 
    }
}
