using DVDRENTAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.DTO
{
    public class CopyDTO
    {
        public int Id { get; set; }
        public bool? Available { get; set; }
        public MovieDTO Movie { get; private set; }
        public static CopyDTO Create(copy copy)
        {
            return new CopyDTO
            {
                Id = copy.copy_id,
                Available = copy.available.Value,
                Movie = new MovieDTO
                {
                    Id = copy.movy.movie_id,
                    Year = copy.movy.year,
                    Price = copy.movy.price,
                    Title = copy.movy.title,
                },
            };

        }
    }
}

