/*
 * COPYRIGHT 2014-2016 Anton Yarkov
 * 
 * Email: anton.yarkov@gmail.com
 * Skype: optiklab
*/

namespace Phi.MobileWebApp.Models
{
    using System;
    using System.Collections.Generic;

    public class FullSuggestionModel
    {
        public FullSuggestionModel()
        {
            this.SuggestedItems = new Dictionary<string, IList<SuggestionModel>>();
            this.CommonSuggestedItems = new Dictionary<string, IList<SuggestionModel>>();
        }

        public string ForecastDescription { get; set; }

        public Dictionary<string, IList<SuggestionModel>> SuggestedItems { get; set; }
        public Dictionary<string, IList<SuggestionModel>> CommonSuggestedItems { get; set; }
    }
}