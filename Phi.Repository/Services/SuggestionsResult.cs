/*
 * COPYRIGHT 2014-2016 Anton Yarkov
 * 
 * Email: anton.yarkov@gmail.com
 * Skype: optiklab
*/

using Phi.Models.Models;
using System.Collections.Generic;

namespace Phi.Repository.Services
{
    public class SuggestionsResult
    {
        public SuggestionsResult()
        {
            Suggestions = new Dictionary<string, Suggestion>();
            CommonSuggestions = new Dictionary<string, List<string>>();
        }

        public Dictionary<string, Suggestion> Suggestions { get; set; }
        public Dictionary<string, List<string>> CommonSuggestions { get; set; }
    }
}
