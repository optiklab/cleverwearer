
namespace Phi.Repository.Enums
{
    /// <summary>
    /// Suggestion term is a formal language which describes
    /// in words level of comfort and what to suggest.
    /// At the same time it categorizes current weather conditions
    /// in the way of suggestion. We have no flag EXTREME_COLD, which 
    /// would describe the weather itself, but instead we have ACTIVE_HEAT,
    /// which at the same time maps us to
    /// 1. type of items to suggest
    /// 2. weather conditions
    /// </summary>
    public enum SuggestionTerms
    {
        NOT_DETERMINED = 1,
        // Need active heat, which means extreme cold - take active insoles, or stay at home =) etc.
        ACTIVE_HEAT = 2,
        // Need static heat, which means cold - take sweater, fur coat, and other winter cloth.
        STATIC_HEAT = 4,
        // No matter cold or heat wind: just take face&eye protection, etc.
        WIND_PROTECTION = 8,
        // Take sun glasses, cap, etc.
        SUN_PROTECTION = 16,
        // Take umbrella, gumboots, etc.
        WATER_PROTECTION = 32,
        // Means fully COMFORT conditions like: 20°C, 50% humidity, less wind
        NO_PROTECTION = 64,
        // Take your tools to swim in pool, sea, ocean... Means comfort water conditions, etc.
        SWIMMING_TOOLS = 128,
        // Take very light clothes.
        STATIC_COOLING = 256,
        // Take pocket air conditioner or something like this =).
        ACTIVE_COOLING = 512,
        // YKWIM
        INSECTS_PROTECTION = 1024,
        // Warning from earth or sea animals in the forest, sea, ocean, etc.
        ANIMALS_PROTECTION = 2048,
        // Take your headache tablets (from pressure, for example).
        MEDICAL_PROTECTION = 4096,
        // Take your anti-radiation suit.
        RADIATION_PROTECTION = 8192
    }
}
