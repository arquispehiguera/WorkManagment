using System;

namespace WorkManagment.Core.Exceptions
{
    public class BussinessExceptions : Exception
    {
        public BussinessExceptions()
        {

        }
        public BussinessExceptions(string message) : base(message)
        {

        }
    }
}
