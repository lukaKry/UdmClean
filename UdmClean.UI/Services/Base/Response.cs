using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UdmClean.UI.Services.Base
{
    public class Response<T>
    {
        public string Message { get; set; }
        public string ValidationErrors { get; set; }
        public bool Success { get; set; }
        public T GetT { get; set; }
    }
}
