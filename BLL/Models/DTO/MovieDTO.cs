using DVDRENTAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.DTO
{
    public class MovieDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public int? AgeRestriction { get; set; }
        public float? Price { get; set; }

        public List<CopyDTO> Copies { get; private set; }

        public List<ActorDTO> Actors { get;  set; }

        public static MovieDTO Create(movy movie)
        {
            return new MovieDTO
            {
                Id = movie.movie_id,
                Title = movie.title,
                Year = movie.year,
                AgeRestriction = movie.age_restriction,
                Price = movie.price.Value,
                Actors = movie?.actors?.Select(x => new ActorDTO
                {
                    Birthday = x.birthday?.DateTime,
                    FirstName = x.first_name,
                    LastName = x.last_name,
                    Id = x.actor_id
                }).ToList(),
                Copies = movie?.copies?.Select(x => new CopyDTO
                {
                    Available = x.available,
                    Id = x.copy_id
                }).ToList()
            };

        }

    }
}
