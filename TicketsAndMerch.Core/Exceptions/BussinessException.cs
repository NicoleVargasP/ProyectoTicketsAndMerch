using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsAndMerch.Core.Exceptions
{
    public class BussinessException : Exception
    {
        public int StatusCode { get; set; } = 500;
        public string ErrorCode { get; set; }

        public BussinessException()
        {
        }

        public BussinessException(string message) : base(message)
        {
        }

        public BussinessException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public BussinessException(string message, int statusCode) : base(message)
        {
            StatusCode = statusCode;
        }

        public BussinessException(string message, string errorCode, int statusCode) : base(message)
        {
            ErrorCode = errorCode;
            StatusCode = statusCode;
        }
    }

}