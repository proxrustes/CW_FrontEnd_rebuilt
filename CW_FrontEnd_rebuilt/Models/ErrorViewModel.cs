using System;

namespace CW_FrontEnd_rebuilt.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId );
    }
}
