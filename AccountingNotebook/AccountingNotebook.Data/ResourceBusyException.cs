using System;

namespace AccountingNotebook.Data
{
    public class ResourceBusyException : Exception
    {
        public ResourceBusyException(string message)
            :base(message)
        {
        }
    }
}
