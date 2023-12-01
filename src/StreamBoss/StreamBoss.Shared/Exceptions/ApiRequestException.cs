using StreamBoss.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamBoss.Shared.Exceptions
{
    public class ApiRequestException : Exception
    {
        public ApiRequestException(RequestError error) : base($"{error.StatusCode} - {error.StatusDescription}: {error.Message}")
        {
            Error = error;
        }

        public RequestError Error { get; }
    }
}
