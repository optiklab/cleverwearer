using System;

namespace Phi.Repository.External
{
    public class News
    {
        /// <summary>
        /// 
        /// </summary>
        public News()
        {
        }

        /// <summary>
        /// Copy-constructor
        /// </summary>
        /// <param name="toCopy"></param>
        public News(News toCopy)
        {
            this.Header = toCopy.Header;
            this.ThemeOrCategory = toCopy.ThemeOrCategory;
            this.Description = toCopy.Description;
            this.PublishDateTime = toCopy.PublishDateTime;
            this.SourceLink = toCopy.SourceLink;
            this.UniqueId = toCopy.UniqueId;
            this.Tags = toCopy.Tags;
        }

        public string Header { get; set; }
        public string ThemeOrCategory { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> PublishDateTime { get; set; }
        public string SourceLink { get; set; }
        public string UniqueId { get; set; }
        public string Tags { get; set; }
    }
}
