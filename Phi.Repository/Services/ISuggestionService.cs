/*
 * COPYRIGHT 2014-2016 Anton Yarkov
 * 
 * Email: anton.yarkov@gmail.com
 * Skype: optiklab
*/
namespace Phi.Repository.Services
{
    using System;
    using System.Collections.Generic;
    using Phi.Models.Models;

    public interface ISuggestionService
    {
        SuggestionsResult GetSuggestion(string woeid, int languageId, string userId);
    }
}
