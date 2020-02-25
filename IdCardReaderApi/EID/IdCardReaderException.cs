using System;

namespace IdCardReaderApi.EID
{
    public class IdCardReaderException: Exception
    {
        public string Method { get; set; }
        public int Status { get; set; }
        public string StatusMessage { get; set; }
        public string DisplayMessage { get; set; }
    }
}
