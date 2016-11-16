using System;

namespace Phi.MobileWebApp.Helpers
{
    public static class DateTimeHelpers
    {
        public static string ToShortString(DateTime time, int languageId)
        {
            if (languageId == 2) // Russian
            {
                return time.ToString("HH:mm");
            }
            return time.ToString("h:mm tt");
        }
    }
}