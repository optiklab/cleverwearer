using System;
using System.Collections.Generic;

namespace Phi.Models.Models
{
    public partial class MessageTemplate
    {
        public string Name { get; set; }
        public string BccEmailAddresses { get; set; }
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsActive { get; set; }
        public Nullable<int> EmailAccountId { get; set; }
        public virtual EmailAccount EmailAccount { get; set; }
    }
}
