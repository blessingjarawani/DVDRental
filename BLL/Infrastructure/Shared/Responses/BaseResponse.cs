using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Infrastructure.Shared.Responses
{
   public class BaseResponse
    {
        public string Error { get; set; }

        public string Info { get; set; }
        public bool Success { get; set; }
    }
}
