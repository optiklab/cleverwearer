using System;

namespace Phi.ImportersTest
{
    public class OpenWeatherMapProviderTest
    {
        /// <summary>
        /// Request: http://api.openweathermap.org/data/2.5/weather?id=2643743&units=metric&mode=xml&appid=c47e6a2a4ef2cd520d735ca4cdda9d93
        /// </summary>
        private const string WEATHER_REQUEST = @"
<current>
<city id=""2643743"" name=""London"">
<coord lon=""-0.13"" lat=""51.51""/>
<country>GB</country>
<sun rise=""2016-03-25T05:49:52"" set=""2016-03-25T18:23:31""/>
</city>
<temperature value=""11.47"" min=""10.7"" max=""13"" unit=""metric""/>
<humidity value=""46"" unit=""%""/>
<pressure value=""1015"" unit=""hPa""/>
<wind>
<speed value=""3.6"" name=""Gentle Breeze""/>
<gusts/>
<direction value=""330"" code=""NNW"" name=""North-northwest""/>
</wind>
<clouds value=""20"" name=""few clouds""/>
<visibility/>
<precipitation mode=""no""/>
<weather number=""801"" value=""few clouds"" icon=""02d""/>
<lastupdate value=""2016-03-25T11:57:05""/>
</current>";
    }
}
