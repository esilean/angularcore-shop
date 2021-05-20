using System;

namespace AngularShop.Core.Errors
{
    public class SomeException : Exception
    {
        public int StatusCode { get; set; }
        public SomeException(int statusCode)
        {
            StatusCode = statusCode;
        }
    }
}