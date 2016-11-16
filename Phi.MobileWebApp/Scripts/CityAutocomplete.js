$(document).ready(function () {
    var cities = [];
    var timer;
    $('#Location').keyup(function (event) {
        clearInterval(timer);
        timer = setInterval(findCity, 500);
    });

    var findCity = function () {

        console.log('findCity');
        clearInterval(timer);
        var cityName = $("#Location").val();
        if (cityName && cityName.length > 0) {

            $.getJSON($('#CurrentLang').val() + '/api/APIProfile/cities?cityName=' + cityName)
                .done(function (data) {

                    cities.splice(0, cities.length);

                    if (data) {

                        for (var index = 0; index < data.length; index++) {

                            var geoObject = data[index];
                            cities.push(geoObject);
                            //cities.push(geoObject.town + ", " + (geoObject.admin ? geoObject.admin + ", " : "") + (geoObject.admin1 ? geoObject.admin1 + ", " : "") + (geoObject.admin2 ? geoObject.admin2 + ", " : "") + geoObject.country + ", " + geoObject.woeid)
                        }
                    }
            })
                .fail(function () {
                    //ShowError('City not found!');
            });

            //$.ajax({
            //    type: "GET",
            //    url: "https://geocode-maps.yandex.ru/1.x/",
            //    // TODO Select right response language: http://api.yandex.com/maps/doc/geocoder/desc/concepts/input_params.xml
            //    data: { lang: "en-US", format: "json", geocode: cityName, kind: "locality" /*, results: 50*/ },
            //    dataType: "json",
            //    success: function (data) {

            //        var cities = [];
            //        if (data && data.response.GeoObjectCollection.featureMember) {

            //            for (var index = 0; index < data.response.GeoObjectCollection.featureMember.length; index++) {

            //                var geoObject = data.response.GeoObjectCollection.featureMember[index];
            //                if (geoObject && geoObject.GeoObject.metaDataProperty.GeocoderMetaData.text)
            //                    cities.push(geoObject.GeoObject.metaDataProperty.GeocoderMetaData.text); // Use full address line
            //                else if (geoObject && geoObject.GeoObject.name)
            //                    cities.push(geoObject.name); // Use just a name
            //            }
            //        }

            //        $("#Location").autocomplete({
            //            source: cities
            //        });
            //    },
            //    error: function (info) {
            //        var i = info;
            //    }
            //});
        }

        return false;
    };
});