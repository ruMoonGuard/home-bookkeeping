using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeBookkeeping.Models.API
{
    public class OperationResult<T>
    {
        public T Model { get; set; }

        public int StatusCode { get; set; }

        public OperationResult(T model, int statusCode)
        {
            Model = model;
            StatusCode = statusCode;
        }
    }
}
