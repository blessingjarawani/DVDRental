using DVDRENTAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.DTO
{
    public class ActorDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? Birthday { get; set; }
        public List<MovieDTO> Movies { get; private set; }

        public static ActorDTO Create(actor actor)
        {

            return new ActorDTO
            {
                Id = actor.actor_id,
                FirstName = actor.first_name,
                LastName = actor.last_name,
                Birthday = actor.birthday?.DateTime,
                Movies = actor.movies?.Select(x => MovieDTO.Create(x)).ToList()
            };

        }

    }
}
