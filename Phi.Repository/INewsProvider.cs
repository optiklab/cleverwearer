using System;
using System.Collections.Generic;

namespace Phi.Repository
{
    public interface INewsProvider
    {
        string ProviderName { get; }
        int LanguageId { get; }
        int UpdatePeriodHours { get; }
        List<Phi.Repository.External.News> GetNews();
    }
}
