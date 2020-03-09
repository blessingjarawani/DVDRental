using DVDRENTAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.DTO
{
    public class ClientDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? Birthday { get; set; }
        public static ClientDTO Create(client client)
        {
            return new ClientDTO
            {
                Id = client.client_id,
                FirstName = client.first_name,
                LastName = client.last_name,
                Birthday = client.birthday.Value.DateTime
            };
        }
    }
}
