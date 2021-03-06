﻿using BLL.Infrastructure.Shared.Responses;
using BLL.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Infrastructure.Shared.Interfaces
{
   public interface IActorsLogic
    {
        ObjectResponse<List<ActorDTO>> GetAll();
        ObjectResponse<bool> AddOrUpdate(ActorDTO actor);

    }
}
