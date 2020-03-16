using BLL.Models.DTO;
using DVDRENTAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Infrastructure.Helpers
{
    public class Mappers
    {
        public static actor MapActorDTO(ActorDTO actor)
        {
            return new actor
            {
                actor_id = actor.Id,
                first_name = actor.FirstName,
                last_name = actor.LastName
            };
        }
    }
}
