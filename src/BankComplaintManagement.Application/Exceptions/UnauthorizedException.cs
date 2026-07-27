using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Application.Exceptions
{
    public class UnauthorizedException
    : BaseException
    {

        public UnauthorizedException(
            string message)
            : base(message, 401)
        {

        }

    }

}