using System;
using System.Collections.Generic;

namespace Phi.Models.Models
{
    public partial class EmailAccount
    {
        public EmailAccount()
        {
            this.QueuedEmails = new List<QueuedEmail>();
            this.MessageTemplates = new List<MessageTemplate>();
        }

        public string Email { get; set; }
        public string DisplayName { get; set; }
        public int Id { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool EnableSsl { get; set; }
        public bool UseDefaultCredentials { get; set; }
        public virtual ICollection<QueuedEmail> QueuedEmails { get; set; }
        public virtual ICollection<MessageTemplate> MessageTemplates { get; set; }
    }
}
