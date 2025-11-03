using System;

namespace StockService.Domain.Utility
{
    public class ExceptionCustom : Exception
    {
        public ExceptionCustom(string message) : base(message)
        {

        }

    }
}
