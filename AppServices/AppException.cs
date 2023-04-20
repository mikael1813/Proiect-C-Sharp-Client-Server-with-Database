using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AppServices
{
    [Serializable]
    public class AppException : ApplicationException
    {
        public AppException()
        {
        }

        public AppException(string message) : base(message)
        {
        }

        public AppException(string message, Exception innerException) : base(message, innerException)
        {
        }
        public AppException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
