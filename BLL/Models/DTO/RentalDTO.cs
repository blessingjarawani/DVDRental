using DVDRENTAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.DTO
{
    public class RentalDTO
    {
        public DateTime? DateOfRental { get; set; }
        public DateTime? DateOfReturn { get; set; }
        public ClientDTO Client { get; private set; }
        public CopyDTO Copy { get; private set; }

        public static RentalDTO Create(rental rental)
        {
            return new RentalDTO
            {
                DateOfRental = rental.date_of_rental.Value.DateTime,
                DateOfReturn = rental.date_of_return.Value.DateTime,
                Copy = CopyDTO.Create(rental?.copy),
                Client = new ClientDTO
                {
                    Id = rental.client.client_id,
                    Birthday = rental.client.birthday.Value.DateTime,
                    LastName = rental.client.last_name,
                    FirstName = rental.client.first_name
                }
            };
        }
    }
}
