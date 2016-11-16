using System;

namespace Phi.ImportersTest
{
    public class YahooProviderTest
    {
        private const string LOCATION_REQUEST = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<places xmlns=""http://where.yahooapis.com/v1/schema.rng"" xmlns:yahoo=""http://www.yahooapis.com/v1/base.rng"" yahoo:start=""0"" yahoo:count=""1"" yahoo:total=""3"">
	<place yahoo:uri=""http://where.yahooapis.com/v1/place/2123805"" xml:lang=""ru-RU"">
		<woeid>2123805</woeid>
		<placeTypeName code=""7"">Город</placeTypeName>
		<name>Таганрог</name>
		<country type=""Страна"" code=""RU"" woeid=""23424936"">Россия</country>
		<admin1 type=""Провинция"" code="""" woeid=""2346921"">Ростовская Область</admin1>
		<admin2 type=""Дистрикт"" code="""" woeid=""90564944"">Taganrog</admin2>
		<admin3/>
		<locality1 type=""Город"" woeid=""2123805"">Таганрог</locality1>
		<locality2/>
		<postal/>
		<centroid>
			<latitude>47.236691</latitude>
			<longitude>38.881050</longitude>
		</centroid>
		<boundingBox>
			<southWest>
				<latitude>47.191460</latitude>
				<longitude>38.792641</longitude>
			</southWest>
			<northEast>
				<latitude>47.284950</latitude>
				<longitude>38.963890</longitude>
			</northEast>
		</boundingBox>
		<areaRank>4</areaRank>
		<popRank>11</popRank>
		<timezone type=""Часовой Пояс"" woeid=""56043614"">Europe/Moscow</timezone>
	</place>
</places>";
    }
}
