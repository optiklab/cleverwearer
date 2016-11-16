using System;
using System.Collections.Generic;

namespace Phi.Models.Models
{
    public partial class QueuedEmail
    {
        public int Priority { get; set; }
        public string FromAddress { get; set; }
        public int Id { get; set; }
        public string FromName { get; set; }
        public string ToAddress { get; set; }
        public string ToName { get; set; }
        public string CC { get; set; }
        public string Bcc { get; set; }
        public string EmailSubject { get; set; }
        public string Body { get; set; }
        public System.DateTime DateCreated { get; set; }
        public int SentTries { get; set; }
        public Nullable<System.DateTime> SentUTC { get; set; }
        public Nullable<int> EmailAccountId { get; set; }
        public virtual EmailAccount EmailAccount { get; set; }
    }
}
