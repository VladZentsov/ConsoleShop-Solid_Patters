using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ConsoleApp1.Validation
{
    [Serializable]
    public class UIException:Exception
    {
        public UIException() { }

        public UIException(string message):base(message)
        {
        }

        public UIException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UIException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
