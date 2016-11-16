using System;

namespace Phi.Models
{
    public class RejectedByProviderException : Exception
    {
        public RejectedByProviderException(string s) : base(s) { }
    }
}
