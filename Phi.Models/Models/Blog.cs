using System;
using System.Collections.Generic;

namespace Phi.Models.Models
{
    public partial class Blog
    {
        public Blog()
        {
            this.BlogStars = new List<BlogStar>();
            this.BlogComments = new List<BlogComment>();
        }

        public int Id { get; set; }
        public string Theme { get; set; }
        public string Article { get; set; }
        public string Header { get; set; }
        public Nullable<int> Rating { get; set; }
        public string Tags { get; set; }
        public Nullable<System.DateTime> PublishDate { get; set; }
        public string UniqueId { get; set; }
        public string SourceUrl { get; set; }
        public Nullable<int> LanguageId { get; set; }
        public string ProviderName { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public virtual ICollection<BlogStar> BlogStars { get; set; }
        public virtual ICollection<BlogComment> BlogComments { get; set; }
    }
}
