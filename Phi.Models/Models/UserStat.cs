using System;
using System.Collections.Generic;

namespace Phi.Models.Models
{
    public partial class UserStat
    {
        public int Id { get; set; }
        public string Browser { get; set; }
        public string IPAddress { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public Nullable<System.DateTime> DateTime { get; set; }
        public string UrlReferrer { get; set; }
        public string Action { get; set; }
        public string ActionPage { get; set; }
    }
}
