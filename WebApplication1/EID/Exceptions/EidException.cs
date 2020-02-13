using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.EID.Exceptions
{
    public class EidException: Exception
    {
        public string Method { get; set; }
        public int Status { get; set; }
    }
}
