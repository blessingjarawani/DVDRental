using DVDRENTAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Infrastructure.Shared.Abstracts
{
    public class BaseLogic
    {
        protected IUnitOfwork _unitOfWork;
        public const string DB_GET_ERROR = "Unable to get Data from Database";
        public const string DB_SAVE_ERROR = "Unable to save data in Database";
        public const string DB_ERROR_INSERT = "Unable to add data in Database";
        public const string DB_ERROR_UPDATE = "Unable to update data in Database";
        public BaseLogic(IUnitOfwork unitOfwork)
        {
            _unitOfWork = unitOfwork;
        }


    }

}
