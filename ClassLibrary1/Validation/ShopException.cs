using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ClassLibrary1.Validation
{
    [Serializable]
    public class ShopException:Exception
    {
        public ShopException()
        {

        }
        public ShopException(string message) : base(message)
        {

        }

        public ShopException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ShopException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
