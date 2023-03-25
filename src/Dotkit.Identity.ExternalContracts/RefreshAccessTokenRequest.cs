using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotkit.Identity.ExternalContracts
{
    public sealed class RefreshAccessTokenRequest
    {
        public string RefreshToken { get; set; }
    }
}
