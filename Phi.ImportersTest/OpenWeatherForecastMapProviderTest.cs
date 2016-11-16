using System;

namespace Phi.ImportersTest
{
    public class OpenWeatherForecastMapProviderTest
    {
        /// <summary>
        /// Request: http://api.openweathermap.org/data/2.5/weather?id=2643743&units=metric&mode=xml&appid=c47e6a2a4ef2cd520d735ca4cdda9d93
        /// </summary>
        private const string WEATHER_REQUEST = @"
<weatherdata>
    <location>
        <name>London</name>
        <type/>
        <country>GB</country>
        <timezone/>
        <location altitude=""0"" latitude=""51.50853"" longitude=""-0.12574"" geobase=""geonames"" geobaseid=""0""/>
    </location>
    <credit/>
    <meta>
        <lastupdate/>
        <calctime>0.0049</calctime>
        <nextupdate/>
    </meta>
    <sun rise=""2016-03-27T05:44:54"" set=""2016-03-27T18:27:09""/>
    <forecast>
        <time from=""2016-03-27T15:00:00"" to=""2016-03-27T18:00:00"">
            <symbol number=""500"" name=""light rain"" var=""10d""/>
            <precipitation unit=""3h"" value=""0.015"" type=""rain""/>
            <windDirection deg=""208.001"" code=""SSW"" name=""South-southwest""/>
            <windSpeed mps=""8.91"" name=""Fresh Breeze""/>
            <temperature unit=""celsius"" value=""9.11"" min=""7.68"" max=""9.11""/>
            <pressure unit=""hPa"" value=""1001.83""/>
            <humidity value=""93"" unit=""%""/>
            <clouds value=""broken clouds"" all=""64"" unit=""%""/>
        </time>
        <time from=""2016-03-27T18:00:00"" to=""2016-03-27T21:00:00"">
            <symbol number=""500"" name=""light rain"" var=""10n""/>
            <precipitation unit=""3h"" value=""0.0625"" type=""rain""/>
            <windDirection deg=""185.001"" code=""S"" name=""South""/>
            <windSpeed mps=""6.46"" name=""Moderate breeze""/>
            <temperature unit=""celsius"" value=""8.05"" min=""6.7"" max=""8.05""/>
            <pressure unit=""hPa"" value=""999.67""/>
            <humidity value=""93"" unit=""%""/>
            <clouds value=""overcast clouds"" all=""88"" unit=""%""/>
        </time>
        <time from=""2016-03-27T21:00:00"" to=""2016-03-28T00:00:00"">
            <symbol number=""501"" name=""moderate rain"" var=""10n""/>
            <precipitation unit=""3h"" value=""4.53"" type=""rain""/>
            <windDirection deg=""161.502"" code=""SSE"" name=""South-southeast""/>
            <windSpeed mps=""8.88"" name=""Fresh Breeze""/>
            <temperature unit=""celsius"" value=""8.78"" min=""7.5"" max=""8.78""/>
            <pressure unit=""hPa"" value=""992.7""/>
            <humidity value=""96"" unit=""%""/>
            <clouds value=""overcast clouds"" all=""92"" unit=""%""/>
        </time>
        <time from=""2016-03-28T00:00:00"" to=""2016-03-28T03:00:00"">
            <symbol number=""501"" name=""moderate rain"" var=""10n""/>
            <precipitation unit=""3h"" value=""7.54"" type=""rain""/>
            <windDirection deg=""195"" code=""SSW"" name=""South-southwest""/>
            <windSpeed mps=""12.57"" name=""Strong breeze""/>
            <temperature unit=""celsius"" value=""10.84"" min=""9.63"" max=""10.84""/>
            <pressure unit=""hPa"" value=""984.64""/>
            <humidity value=""94"" unit=""%""/>
            <clouds value=""overcast clouds"" all=""92"" unit=""%""/>
        </time>
        <time from=""2016-03-28T03:00:00"" to=""2016-03-28T06:00:00"">
            <symbol number=""501"" name=""moderate rain"" var=""10d""/>
            <precipitation unit=""3h"" value=""3.525"" type=""rain""/>
            <windDirection deg=""190.502"" code=""S"" name=""South""/>
            <windSpeed mps=""14.11"" name=""High wind, near gale""/>
            <temperature unit=""celsius"" value=""10.22"" min=""9.09"" max=""10.22""/>
            <pressure unit=""hPa"" value=""978.65""/>
            <humidity value=""96"" unit=""%""/>
            <clouds value=""overcast clouds"" all=""92"" unit=""%""/>
        </time>
        <time from=""2016-03-28T06:00:00"" to=""2016-03-28T09:00:00"">
            <symbol number=""500"" name=""light rain"" var=""10d""/>
            <precipitation unit=""3h"" value=""0.165"" type=""rain""/>
            <windDirection deg=""277.501"" code=""W"" name=""West""/>
            <windSpeed mps=""14.2"" name=""High wind, near gale""/>
            <temperature unit=""celsius"" value=""6.63"" min=""5.58"" max=""6.63""/>
            <pressure unit=""hPa"" value=""986.91""/>
            <humidity value=""95"" unit=""%""/>
            <clouds value=""overcast clouds"" all=""92"" unit=""%""/>
        </time>
        <time from=""2016-03-28T09:00:00"" to=""2016-03-28T12:00:00"">
            <symbol number=""801"" name=""few clouds"" var=""02d""/>
            <precipitation/>
            <windDirection deg=""263.009"" code=""W"" name=""West""/>
            <windSpeed mps=""9.3"" name=""Fresh Breeze""/>
            <temperature unit=""celsius"" value=""9.69"" min=""8.71"" max=""9.69""/>
            <pressure unit=""hPa"" value=""993.77""/>
            <humidity value=""98"" unit=""%""/>
            <clouds value=""few clouds"" all=""20"" unit=""%""/>
        </time>
        <time from=""2016-03-28T12:00:00"" to=""2016-03-28T15:00:00"">
            <symbol number=""801"" name=""few clouds"" var=""02d""/>
            <precipitation/>
            <windDirection deg=""254.002"" code=""WSW"" name=""West-southwest""/>
            <windSpeed mps=""8.07"" name=""Fresh Breeze""/>
            <temperature unit=""celsius"" value=""9.51"" min=""8.61"" max=""9.51""/>
            <pressure unit=""hPa"" value=""996.46""/>
            <humidity value=""94"" unit=""%""/>
            <clouds value=""few clouds"" all=""12"" unit=""%""/>
        </time>
        <time from=""2016-03-28T15:00:00"" to=""2016-03-28T18:00:00"">
            <symbol number=""500"" name=""light rain"" var=""10d""/>
            <precipitation unit=""3h"" value=""0.015"" type=""rain""/>
            <windDirection deg=""242.002"" code=""WSW"" name=""West-southwest""/>
            <windSpeed mps=""4.9"" name=""Gentle Breeze""/>
            <temperature unit=""celsius"" value=""8.06"" min=""7.23"" max=""8.06""/>
            <pressure unit=""hPa"" value=""997.99""/>
            <humidity value=""94"" unit=""%""/>
            <clouds value=""scattered clouds"" all=""48"" unit=""%""/>
        </time>
        <time from=""2016-03-28T18:00:00"" to=""2016-03-28T21:00:00"">
            <symbol number=""500"" name=""light rain"" var=""10n""/>
            <precipitation unit=""3h"" value=""0.98"" type=""rain""/>
            <windDirection deg=""225.504"" code=""SW"" name=""Southwest""/>
            <windSpeed mps=""2.57"" name=""Light breeze""/>
            <temperature unit=""celsius"" value=""6.33"" min=""5.58"" max=""6.33""/>
            <pressure unit=""hPa"" value=""999.24""/>
            <humidity value=""98"" unit=""%""/>
            <clouds value=""overcast clouds"" all=""88"" unit=""%""/>
        </time>
        <time from=""2016-03-28T21:00:00"" to=""2016-03-29T00:00:00"">
            <symbol number=""500"" name=""light rain"" var=""10n""/>
            <precipitation unit=""3h"" value=""0.485"" type=""rain""/>
            <windDirection deg=""261.5"" code=""W"" name=""West""/>
            <windSpeed mps=""4.01"" name=""Gentle Breeze""/>
            <temperature unit=""celsius"" value=""5.42"" min=""4.74"" max=""5.42""/>
            <pressure unit=""hPa"" value=""1001.49""/>
            <humidity value=""97"" unit=""%""/>
            <clouds value=""scattered clouds"" all=""32"" unit=""%""/>
        </time>
        <time from=""2016-03-29T00:00:00"" to=""2016-03-29T03:00:00"">
            <symbol number=""500"" name=""light rain"" var=""10n""/>
            <precipitation unit=""3h"" value=""0.015"" type=""rain""/>
            <windDirection deg=""231.503"" code=""SW"" name=""Southwest""/>
            <windSpeed mps=""4.27"" name=""Gentle Breeze""/>
            <temperature unit=""celsius"" value=""4.46"" min=""3.85"" max=""4.46""/>
            <pressure unit=""hPa"" value=""1003.52""/>
            <humidity value=""95"" unit=""%""/>
            <clouds value=""broken clouds"" all=""64"" unit=""%""/>
        </time>
        <time from=""2016-03-29T03:00:00"" to=""2016-03-29T06:00:00"">
            <symbol number=""500"" name=""light rain"" var=""10d""/>
            <precipitation unit=""3h"" value=""0.065"" type=""rain""/>
            <windDirection deg=""238.505"" code=""WSW"" name=""West-southwest""/>
            <windSpeed mps=""4.66"" name=""Gentle Breeze""/>
            <temperature unit=""celsius"" value=""5.25"" min=""4.72"" max=""5.25""/>
            <pressure unit=""hPa"" value=""1005.05""/>
            <humidity value=""96"" unit=""%""/>
            <clouds value=""scattered clouds"" all=""36"" unit=""%""/>
        </time>
        <time from=""2016-03-29T06:00:00"" to=""2016-03-29T09:00:00"">
            <symbol number=""801"" name=""few clouds"" var=""02d""/>
            <precipitation/>
            <windDirection deg=""236.002"" code=""SW"" name=""Southwest""/>
            <windSpeed mps=""7.52"" name=""Moderate breeze""/>
            <temperature unit=""celsius"" value=""8.04"" min=""7.59"" max=""8.04""/>
            <pressure unit=""hPa"" value=""1006.57""/>
            <humidity value=""100"" unit=""%""/>
            <clouds value=""few clouds"" all=""24"" unit=""%""/>
        </time>
        <time from=""2016-03-29T09:00:00"" to=""2016-03-29T12:00:00"">
            <symbol number=""500"" name=""light rain"" var=""10d""/>
            <precipitation unit=""3h"" value=""0.595"" type=""rain""/>
            <windDirection deg=""232.5"" code=""SW"" name=""Southwest""/>
            <windSpeed mps=""5.72"" name=""Moderate breeze""/>
            <temperature unit=""celsius"" value=""8.09"" min=""7.71"" max=""8.09""/>
            <pressure unit=""hPa"" value=""1007.28""/>
            <humidity value=""99"" unit=""%""/>
            <clouds value=""broken clouds"" all=""56"" unit=""%""/>
        </time>
        <time from=""2016-03-29T12:00:00"" to=""2016-03-29T15:00:00"">
            <symbol number=""501"" name=""moderate rain"" var=""10d""/>
            <precipitation unit=""3h"" value=""5.035"" type=""rain""/>
            <windDirection deg=""192.001"" code=""SSW"" name=""South-southwest""/>
            <windSpeed mps=""9.61"" name=""Fresh Breeze""/>
            <temperature unit=""celsius"" value=""7.86"" min=""7.56"" max=""7.86""/>
            <pressure unit=""hPa"" value=""1004.46""/>
            <humidity value=""100"" unit=""%""/>
            <clouds value=""overcast clouds"" all=""92"" unit=""%""/>
        </time>
        <time from=""2016-03-29T15:00:00"" to=""2016-03-29T18:00:00"">
            <symbol number=""501"" name=""moderate rain"" var=""10d""/>
            <precipitation unit=""3h"" value=""4.235"" type=""rain""/>
            <windDirection deg=""226.503"" code=""SW"" name=""Southwest""/>
            <windSpeed mps=""9.66"" name=""Fresh Breeze""/>
            <temperature unit=""celsius"" value=""8.28"" min=""8.05"" max=""8.28""/>
            <pressure unit=""hPa"" value=""1003.09""/>
            <humidity value=""93"" unit=""%""/>
            <clouds value=""broken clouds"" all=""76"" unit=""%""/>
        </time>
        <time from=""2016-03-29T18:00:00"" to=""2016-03-29T21:00:00"">
            <symbol number=""803"" name=""broken clouds"" var=""04n""/>
            <precipitation/>
            <windDirection deg=""243.501"" code=""WSW"" name=""West-southwest""/>
            <windSpeed mps=""8.37"" name=""Fresh Breeze""/>
            <temperature unit=""celsius"" value=""6.04"" min=""5.89"" max=""6.04""/>
            <pressure unit=""hPa"" value=""1004.79""/>
            <humidity value=""91"" unit=""%""/>
            <clouds value=""broken clouds"" all=""64"" unit=""%""/>
        </time>
        <time from=""2016-03-29T21:00:00"" to=""2016-03-30T00:00:00"">
            <symbol number=""803"" name=""broken clouds"" var=""04n""/>
            <precipitation/>
            <windDirection deg=""239.501"" code=""WSW"" name=""West-southwest""/>
            <windSpeed mps=""5.31"" name=""Gentle Breeze""/>
            <temperature unit=""celsius"" value=""5.13"" min=""5.05"" max=""5.13""/>
            <pressure unit=""hPa"" value=""1005.95""/>
            <humidity value=""93"" unit=""%""/>
            <clouds value=""broken clouds"" all=""56"" unit=""%""/>
        </time>
        <time from=""2016-03-30T00:00:00"" to=""2016-03-30T03:00:00"">
            <symbol number=""803"" name=""broken clouds"" var=""04n""/>
            <precipitation/>
            <windDirection deg=""224.503"" code=""SW"" name=""Southwest""/>
            <windSpeed mps=""5.31"" name=""Gentle Breeze""/>
            <temperature unit=""celsius"" value=""4.56"" min=""4.56"" max=""4.56""/>
            <pressure unit=""hPa"" value=""1005.79""/>
            <humidity value=""93"" unit=""%""/>
            <clouds value=""broken clouds"" all=""64"" unit=""%""/>
        </time>
        <time from=""2016-03-30T03:00:00"" to=""2016-03-30T06:00:00"">
            <symbol number=""500"" name=""light rain"" var=""10d""/>
            <precipitation unit=""3h"" value=""0.005"" type=""rain""/>
            <windDirection deg=""258.007"" code=""WSW"" name=""West-southwest""/>
            <windSpeed mps=""5.66"" name=""Moderate breeze""/>
            <temperature unit=""celsius"" value=""4.5"" min=""4.5"" max=""4.5""/>
            <pressure unit=""hPa"" value=""1006.78""/>
            <humidity value=""93"" unit=""%""/>
            <clouds value=""scattered clouds"" all=""32"" unit=""%""/>
        </time>
        <time from=""2016-03-30T06:00:00"" to=""2016-03-30T09:00:00"">
            <symbol number=""801"" name=""few clouds"" var=""02d""/>
            <precipitation/>
            <windDirection deg=""259.504"" code=""W"" name=""West""/>
            <windSpeed mps=""5.96"" name=""Moderate breeze""/>
            <temperature unit=""celsius"" value=""6.7"" min=""6.7"" max=""6.7""/>
            <pressure unit=""hPa"" value=""1008.94""/>
            <humidity value=""100"" unit=""%""/>
            <clouds value=""few clouds"" all=""12"" unit=""%""/>
        </time>
        <time from=""2016-03-30T09:00:00"" to=""2016-03-30T12:00:00"">
            <symbol number=""802"" name=""scattered clouds"" var=""03d""/>
            <precipitation/>
            <windDirection deg=""254.003"" code=""WSW"" name=""West-southwest""/>
            <windSpeed mps=""8.17"" name=""Fresh Breeze""/>
            <temperature unit=""celsius"" value=""8.73"" min=""8.73"" max=""8.73""/>
            <pressure unit=""hPa"" value=""1010.26""/>
            <humidity value=""98"" unit=""%""/>
            <clouds value=""scattered clouds"" all=""36"" unit=""%""/>
        </time>
        <time from=""2016-03-30T12:00:00"" to=""2016-03-30T15:00:00"">
            <symbol number=""800"" name=""clear sky"" var=""02d""/>
            <precipitation/>
            <windDirection deg=""271.502"" code=""W"" name=""West""/>
            <windSpeed mps=""7.52"" name=""Moderate breeze""/>
            <temperature unit=""celsius"" value=""9.07"" min=""9.07"" max=""9.07""/>
            <pressure unit=""hPa"" value=""1011.58""/>
            <humidity value=""92"" unit=""%""/>
            <clouds value=""clear sky"" all=""8"" unit=""%""/>
        </time>
        <time from=""2016-03-30T15:00:00"" to=""2016-03-30T18:00:00"">
            <symbol number=""801"" name=""few clouds"" var=""02d""/>
            <precipitation/>
            <windDirection deg=""279.5"" code=""W"" name=""West""/>
            <windSpeed mps=""4.2"" name=""Gentle Breeze""/>
            <temperature unit=""celsius"" value=""7.54"" min=""7.54"" max=""7.54""/>
            <pressure unit=""hPa"" value=""1012.93""/>
            <humidity value=""79"" unit=""%""/>
            <clouds value=""few clouds"" all=""12"" unit=""%""/>
        </time>
        <time from=""2016-03-30T18:00:00"" to=""2016-03-30T21:00:00"">
            <symbol number=""802"" name=""scattered clouds"" var=""03n""/>
            <precipitation/>
            <windDirection deg=""265.501"" code=""W"" name=""West""/>
            <windSpeed mps=""2.77"" name=""Light breeze""/>
            <temperature unit=""celsius"" value=""4.21"" min=""4.21"" max=""4.21""/>
            <pressure unit=""hPa"" value=""1014.82""/>
            <humidity value=""82"" unit=""%""/>
            <clouds value=""scattered clouds"" all=""36"" unit=""%""/>
        </time>
        <time from=""2016-03-30T21:00:00"" to=""2016-03-31T00:00:00"">
            <symbol number=""802"" name=""scattered clouds"" var=""03n""/>
            <precipitation/>
            <windDirection deg=""248.502"" code=""WSW"" name=""West-southwest""/>
            <windSpeed mps=""1.63"" name=""Light breeze""/>
            <temperature unit=""celsius"" value=""2.32"" min=""2.32"" max=""2.32""/>
            <pressure unit=""hPa"" value=""1015.63""/>
            <humidity value=""90"" unit=""%""/>
            <clouds value=""scattered clouds"" all=""36"" unit=""%""/>
        </time>
        <time from=""2016-03-31T00:00:00"" to=""2016-03-31T03:00:00"">
            <symbol number=""801"" name=""few clouds"" var=""02n""/>
            <precipitation/>
            <windDirection deg=""247.502"" code=""WSW"" name=""West-southwest""/>
            <windSpeed mps=""1.32"" name=""Calm""/>
            <temperature unit=""celsius"" value=""0.93"" min=""0.93"" max=""0.93""/>
            <pressure unit=""hPa"" value=""1015.54""/>
            <humidity value=""89"" unit=""%""/>
            <clouds value=""few clouds"" all=""24"" unit=""%""/>
        </time>
        <time from=""2016-03-31T03:00:00"" to=""2016-03-31T06:00:00"">
            <symbol number=""801"" name=""few clouds"" var=""02d""/>
            <precipitation/>
            <windDirection deg=""236.002"" code=""SW"" name=""Southwest""/>
            <windSpeed mps=""2.41"" name=""Light breeze""/>
            <temperature unit=""celsius"" value=""0.62"" min=""0.62"" max=""0.62""/>
            <pressure unit=""hPa"" value=""1016.61""/>
            <humidity value=""84"" unit=""%""/>
            <clouds value=""few clouds"" all=""20"" unit=""%""/>
        </time>
        <time from=""2016-03-31T06:00:00"" to=""2016-03-31T09:00:00"">
            <symbol number=""800"" name=""clear sky"" var=""01d""/>
            <precipitation/>
            <windDirection deg=""282.002"" code=""WNW"" name=""West-northwest""/>
            <windSpeed mps=""2.67"" name=""Light breeze""/>
            <temperature unit=""celsius"" value=""6.13"" min=""6.13"" max=""6.13""/>
            <pressure unit=""hPa"" value=""1018.1""/>
            <humidity value=""96"" unit=""%""/>
            <clouds value=""clear sky"" all=""0"" unit=""%""/>
        </time>
        <time from=""2016-03-31T09:00:00"" to=""2016-03-31T12:00:00"">
            <symbol number=""801"" name=""few clouds"" var=""02d""/>
            <precipitation/>
            <windDirection deg=""306.01"" code=""NW"" name=""Northwest""/>
            <windSpeed mps=""4.07"" name=""Gentle Breeze""/>
            <temperature unit=""celsius"" value=""8.7"" min=""8.7"" max=""8.7""/>
            <pressure unit=""hPa"" value=""1018.85""/>
            <humidity value=""89"" unit=""%""/>
            <clouds value=""few clouds"" all=""20"" unit=""%""/>
        </time>
        <time from=""2016-03-31T12:00:00"" to=""2016-03-31T15:00:00"">
            <symbol number=""803"" name=""broken clouds"" var=""04d""/>
            <precipitation/>
            <windDirection deg=""327.509"" code=""NNW"" name=""North-northwest""/>
            <windSpeed mps=""5.11"" name=""Gentle Breeze""/>
            <temperature unit=""celsius"" value=""9.03"" min=""9.03"" max=""9.03""/>
            <pressure unit=""hPa"" value=""1019.45""/>
            <humidity value=""79"" unit=""%""/>
            <clouds value=""broken clouds"" all=""56"" unit=""%""/>
        </time>
        <time from=""2016-03-31T15:00:00"" to=""2016-03-31T18:00:00"">
            <symbol number=""801"" name=""few clouds"" var=""02d""/>
            <precipitation/>
            <windDirection deg=""329"" code=""NNW"" name=""North-northwest""/>
            <windSpeed mps=""3.9"" name=""Gentle Breeze""/>
            <temperature unit=""celsius"" value=""7.44"" min=""7.44"" max=""7.44""/>
            <pressure unit=""hPa"" value=""1021.03""/>
            <humidity value=""71"" unit=""%""/>
            <clouds value=""few clouds"" all=""12"" unit=""%""/>
        </time>
        <time from=""2016-03-31T18:00:00"" to=""2016-03-31T21:00:00"">
            <symbol number=""800"" name=""clear sky"" var=""01n""/>
            <precipitation/>
            <windDirection deg=""331.006"" code=""NNW"" name=""North-northwest""/>
            <windSpeed mps=""2.91"" name=""Light breeze""/>
            <temperature unit=""celsius"" value=""3.91"" min=""3.91"" max=""3.91""/>
            <pressure unit=""hPa"" value=""1023.31""/>
            <humidity value=""79"" unit=""%""/>
            <clouds value=""clear sky"" all=""0"" unit=""%""/>
        </time>
        <time from=""2016-03-31T21:00:00"" to=""2016-04-01T00:00:00"">
            <symbol number=""800"" name=""clear sky"" var=""01n""/>
            <precipitation/>
            <windDirection deg=""323.502"" code=""NW"" name=""Northwest""/>
            <windSpeed mps=""1.31"" name=""Calm""/>
            <temperature unit=""celsius"" value=""1.1"" min=""1.1"" max=""1.1""/>
            <pressure unit=""hPa"" value=""1024.63""/>
            <humidity value=""92"" unit=""%""/>
            <clouds value=""clear sky"" all=""0"" unit=""%""/>
        </time>
        <time from=""2016-04-01T00:00:00"" to=""2016-04-01T03:00:00"">
            <symbol number=""800"" name=""clear sky"" var=""01n""/>
            <precipitation/>
            <windDirection deg=""231.002"" code=""SW"" name=""Southwest""/>
            <windSpeed mps=""1.16"" name=""Calm""/>
            <temperature unit=""celsius"" value=""-0.37"" min=""-0.37"" max=""-0.37""/>
            <pressure unit=""hPa"" value=""1024.84""/>
            <humidity value=""85"" unit=""%""/>
            <clouds value=""clear sky"" all=""0"" unit=""%""/>
        </time>
        <time from=""2016-04-01T03:00:00"" to=""2016-04-01T06:00:00"">
            <symbol number=""801"" name=""few clouds"" var=""02d""/>
            <precipitation/>
            <windDirection deg=""161.001"" code=""SSE"" name=""South-southeast""/>
            <windSpeed mps=""1.32"" name=""Calm""/>
            <temperature unit=""celsius"" value=""-0.91"" min=""-0.91"" max=""-0.91""/>
            <pressure unit=""hPa"" value=""1025.01""/>
            <humidity value=""83"" unit=""%""/>
            <clouds value=""few clouds"" all=""12"" unit=""%""/>
        </time>
        <time from=""2016-04-01T06:00:00"" to=""2016-04-01T09:00:00"">
            <symbol number=""800"" name=""clear sky"" var=""01d""/>
            <precipitation/>
            <windDirection deg=""176.501"" code=""S"" name=""South""/>
            <windSpeed mps=""3.21"" name=""Light breeze""/>
            <temperature unit=""celsius"" value=""6.57"" min=""6.57"" max=""6.57""/>
            <pressure unit=""hPa"" value=""1024.81""/>
            <humidity value=""91"" unit=""%""/>
            <clouds value=""clear sky"" all=""0"" unit=""%""/>
        </time>
        <time from=""2016-04-01T09:00:00"" to=""2016-04-01T12:00:00"">
            <symbol number=""802"" name=""scattered clouds"" var=""03d""/>
            <precipitation/>
            <windDirection deg=""188.5"" code=""S"" name=""South""/>
            <windSpeed mps=""6.21"" name=""Moderate breeze""/>
            <temperature unit=""celsius"" value=""10.21"" min=""10.21"" max=""10.21""/>
            <pressure unit=""hPa"" value=""1023.59""/>
            <humidity value=""79"" unit=""%""/>
            <clouds value=""scattered clouds"" all=""32"" unit=""%""/>
        </time>
        <time from=""2016-04-01T12:00:00"" to=""2016-04-01T15:00:00"">
            <symbol number=""803"" name=""broken clouds"" var=""04d""/>
            <precipitation/>
            <windDirection deg=""197.502"" code=""SSW"" name=""South-southwest""/>
            <windSpeed mps=""7.66"" name=""Moderate breeze""/>
            <temperature unit=""celsius"" value=""9.81"" min=""9.81"" max=""9.81""/>
            <pressure unit=""hPa"" value=""1021.69""/>
            <humidity value=""73"" unit=""%""/>
            <clouds value=""broken clouds"" all=""68"" unit=""%""/>
        </time>
    </forecast>
</weatherdata>";
    }
}
