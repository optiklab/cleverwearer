/*
 * COPYRIGHT 2014-2016 Anton Yarkov
 * 
 * Email: anton.yarkov@gmail.com
 * Skype: optiklab
*/

namespace Phi.MobileWebApp.Models
{
    using System.Collections.Generic;
    using Phi.Models.Models;
    using Resources;
    using Validation;

    public class MainViewModel
    {
        public MainViewModel()
        {
            IsDefaultLocation = true;

            SelectedLocations = new List<LocationViewModel>();
            ActionTypes = new List<ActionType>();
            Providers = new List<ItemProvider>();
            ItemTypes = new Dictionary<int, string>();
            CommonSuggestionTypes = new List<string>();
            NewsDevicesItems = new List<Blog>();
            NewsClothesItems = new List<Blog>();
            NewsStatupsItems = new List<Blog>();
        }

        #region Public properties

        public int Language { get; set; }
        public bool IsDefaultLocation { get; set; }
        [ValidLength(Length = 100, ErrorMessageResourceType = typeof(GlobalResources), ErrorMessageResourceName = "LocationLenghtLimit")]
        public string Location { get; set; }
        [ValidLength(Length = 100, ErrorMessageResourceType = typeof(GlobalResources), ErrorMessageResourceName = "SelectedLocationLenghtLimit")]
        public string SelectedLocation { get; set; }
        public IList<LocationViewModel> SelectedLocations { get; set; }
        public IList<ActionType> ActionTypes { get; set; }
        public IList<string> CommonSuggestionTypes { get; set; }
        public IList<ItemProvider> Providers { get; set; }
        public IDictionary<int, string> ItemTypes { get; set; }
        public List<Blog> NewsDevicesItems { get; set; }
        public List<Blog> NewsClothesItems { get; set; }
        public List<Blog> NewsStatupsItems { get; set; }
        public string PromoVideoDescription { get; set; }

        #endregion
    }
}