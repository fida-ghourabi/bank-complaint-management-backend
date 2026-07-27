using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Application.Exceptions
{
    public class NotFoundException
    : BaseException
    {

        public NotFoundException(
            string message)
            : base(message, 404)
        {

        }

    }
}
