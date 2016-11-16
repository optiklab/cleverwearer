/*
 * COPYRIGHT 2014-2016 Anton Yarkov
 * 
 * Email: anton.yarkov@gmail.com
 * Skype: optiklab
*/

namespace Phi.Repository.Services
{
    public enum CommonSuggestionType
    {
        Child03,
        Child36,
        Child69,
        Child912,
        Child1218,
        Child1824
    }

    public class CommonSuggestionItem
    {
        public string Name { get; set; }

        public int Term { get; set; }

        public static string GetTypeName(CommonSuggestionType type, int languageId)
        {
            switch (type)
            {
                case CommonSuggestionType.Child03:
                    return (languageId == 1) ? "Child 0-3 months" : "Ребенок 0-3 мес";
                case CommonSuggestionType.Child36:
                    return (languageId == 1) ? "Child 3-6 months" : "Ребенок 3-6 мес";
                case CommonSuggestionType.Child69:
                    return (languageId == 1) ? "Child 6-9 months" : "Ребенок 6-9 мес";
                case CommonSuggestionType.Child912:
                    return (languageId == 1) ? "Child 9-12 months" : "Ребенок 9-12 мес";
                case CommonSuggestionType.Child1218:
                    return (languageId == 1) ? "Child 12-18 months" : "Ребенок 12-18 мес";
                case CommonSuggestionType.Child1824:
                    return (languageId == 1) ? "Child 18-24 months" : "Ребенок 18-24 мес";
                default:
                    return string.Empty;
            }
        }
    }
}
