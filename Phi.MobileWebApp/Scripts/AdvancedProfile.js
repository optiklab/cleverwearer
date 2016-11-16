$(document).ready(function () {

    $('#Location').autocomplete({
        source: function (request, response) {

            var cityName = request.term;
            if (cityName && cityName.length > 0) {

                $.getJSON(location.protocol + '//' + location.host + '/' + $('#CurrentLang').val() + '/api/APIProfile/cities?cityName=' + cityName)
                    .done(function (data) {

                        var cities = [];
                        if (data) {

                            for (var index = 0; index < data.length; index++) {

                                var geoObject = data[index];
                                cities.push(geoObject);
                            }
                        }

                        response(cities);
                    })
                    .fail(function () {
                        alert('Cities not found because some error happened: please, contact our support!');
                    });
            }
        },
        select: function (event, ui) {

            var geoObject = ui.item;

            // Give a command to server: get all data about this city and save.
            $.ajax({
                method: 'get',
                url: location.protocol + '//' + location.host + '/' + $('#CurrentLang').val() + '/api/APIProfile/addbranchcity?woeid=' + geoObject.woeid,
                contentType: 'application/json; charset=utf-8',
                headers: {
                    'Authorization': 'Bearer ' + sessionStorage.getItem('accessToken')
                },
                success: function (data) {

                    // Add to control.
                    AddNewCityLocation(geoObject.woeid, geoObject.town + ", " + geoObject.country);
                },
                fail: function (data) {
                    ShowError('Cannot add city, please, contact our support!');
                }
            });

            return false;
        }
    }).autocomplete('instance')._renderItem = function (ul, geoObject) {
        return $('<li>')
            .append('<a><b>' + geoObject.country + '</b>, ' + geoObject.town + (geoObject.admin ? ', ' + geoObject.admin : '') + (geoObject.admin1 ? ', ' + geoObject.admin1 : '') + (geoObject.admin2 ? ', ' + geoObject.admin2 : '') + '</a>')
            .appendTo(ul);
    };

    $.ajax({
        method: 'get',
        url: location.protocol + '//' + location.host + '/' + $('#CurrentLang').val() + '/api/APIProfile/branchcities',
        contentType: 'application/json; charset=utf-8',
        headers: {
            'Authorization': 'Bearer ' + sessionStorage.getItem('accessToken')
        },
        success: function (data) {

            if (data) {

                for (var index = 0; index < data.length; index++) {

                    var geoObject = data[index];
                    AddNewCityLocation(geoObject.woeid, geoObject.town + ", " + geoObject.country);
                }
            }
        },
        fail: function (data) {
            ShowError('Error during getting the list of cities, please, contact our support!');
        }
    });
});

// Adds new location into list of locations.
function AddNewCityLocation(woeid, fullLocationName) {
    try {

        // Check for duplicates in the list.
        var isDuplicate = 0;
        $('.selectedContent').each(function () {

            if ($(this).parent().attr('id') !== 'SelectedLocation') {

                var text = $(this).text();

                if (text == location) {

                    isDuplicate = 1;
                    return;
                }
            }
        });

        if (isDuplicate || location == '' || location == null) {

            ShowError(pleaseTypeUniqueLocation);
            return;
        }

        // Create Location element by copying from Template.
        var selectedLocationElem = document.getElementById('SelectedLocation');
        var newSelectedLocationElem = selectedLocationElem.cloneNode(true);
        newSelectedLocationElem.id = newSelectedLocationElem.id + guid();
        newSelectedLocationElem.style.display = "block";
        newSelectedLocationElem.setAttribute('woeid', woeid);

        $('#SelectedLocationContainer').append(newSelectedLocationElem);

        var el = $("#" + newSelectedLocationElem.id + ' > .selectedContent');
        el.text(fullLocationName);

        $('#Location').val('');
    } catch (e) {

        LogError(e);
        isWorking = false;
    }
}

// Removes location from list of selected.
function CloseSelected(element) {

    try {
        var woeid = element.getAttribute('woeid');

        $.ajax({
            method: 'get',
            url: location.protocol + '//' + location.host + '/' + $('#CurrentLang').val() + '/api/APIProfile/removebranchcity?woeid=' + woeid,
            contentType: 'application/json; charset=utf-8',
            headers: {
                'Authorization': 'Bearer ' + sessionStorage.getItem('accessToken')
            },
            success: function (data) {

                var container = document.getElementById('SelectedLocationContainer');
                container.removeChild(element);
            },
            fail: function (data) {
                ShowError('Error during removing element from server, please, contact our support!');
            }
        });

    } catch (e) {

        LogError(e);
    }

    window.event.stopPropagation();
    return false;
}
