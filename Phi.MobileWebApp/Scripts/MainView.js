$(document).ready(function () {

    isWorking = false;

    $('.selectpicker').selectpicker();

    if ($('#CurrentLang').val() == 'en') {

        $('.sunset').addClass('time-en');
        $('.sunrise').addClass('time-en');
    }

    LoadCity();
    AssignCityControls();

    // Reload items in case of change of any filter.
    $('#actiontypes').change(function () {
        var savedData = sessionStorage.getItem('suggestedItems');

        if (savedData) {
            var data = jQuery.parseJSON(savedData);

            if (data) {
                ShowSuggestedItems(data, null, null);
            }
        }
    });

    $('#commonSuggestionTypes').change(function () {
        var savedData = sessionStorage.getItem('suggestedItems');

        if (savedData) {
            var data = jQuery.parseJSON(savedData);
            var specificType = $("#commonSuggestionTypes option:selected").attr('data');
            if (data && specificType) {
                ReloadCommonItemsByFilter(data, specificType);
            }
        }
    });

    // Hide or show bottom pager panel in dependence of opened Tab.
    $('.mainTab').bind('click',
        function (e) {
            if (e.target.hash == '#spring') {
                $('#summer').hide();
                $('#autumn').hide();
                $('#winter').hide();
                $('#spring').show();
                $('.suggestions-panel-conent > .tab-content > spring').addClass('active');
            }
            else if (e.target.hash == '#summer') {
                $('#spring').hide();
                $('#autumn').hide();
                $('#winter').hide();
                $('#summer').show();
                $('.suggestions-panel-conent > .tab-content > summer').addClass('active');
            }
            else if (e.target.hash == '#autumn') {
                $('#spring').hide();
                $('#summer').hide();
                $('#winter').hide();
                $('#autumn').show();
                $('.suggestions-panel-conent > .tab-content > autumn').addClass('active');
            }
            else if (e.target.hash == '#winter') {
                $('#spring').hide();
                $('#autumn').hide();
                $('#summer').hide();
                $('#winter').show();
                $('.suggestions-panel-conent > .tab-content > winter').addClass('active');
            }
        });
});

var isWorking = false;

// Loads city on document ready.
function LoadCity()
{
    var selectedCityCountry = $.cookie('SelectedCityCountry');
    var selectedWoeid = $.cookie('SelectedWoeid');

    if (selectedCityCountry && selectedWoeid) {

        try {
            var ccPairs = selectedCityCountry.replace(/;;/g, ';').split(';');
            var woeids = selectedWoeid.replace(/;;/g, ';').split(';');

            for (var i = 0; i < ccPairs.length && i < woeids.length; i++) {

                var isLast = i == ccPairs.length - 1;

                if (ccPairs[i] && woeids[i]) {
                    AddNewCityLocation(woeids[i], ccPairs[i], false, isLast);
                }
            }
        } catch (e) {

            LogError(e);
        }

    } else {

        // Try to find out user IP and get his location.
        $.getJSON('http://api.hostip.info/get_json.php') //'http://jsonip.appspot.com' -- very limited
        .done(function (data) {

            if (data && data.ip) {
                // Example: https://api.ipinfodb.com/v3/ip-city/?key=debfc7c448e8b9d818084949fa23db2382f2488fbfd52e805a3e059091c65d8b&ip=178.76.194.158&format=json
                $.ajax({
                    method: 'get',
                    url: 'http://api.ipinfodb.com/v3/ip-city/?key=debfc7c448e8b9d818084949fa23db2382f2488fbfd52e805a3e059091c65d8b&ip=' + data.ip + '&format=json',
                    contentType: "application/json; charset=utf-8",
                    dataType: "jsonp",
                    success: function (location) {
                        if (location && location.countryName && location.cityName) {
                            AddDefaultCityLocation(location.countryName, location.cityName);
                        } else {
                            AddDefaultCityLocation();
                        }
                    },
                    error: function (error) {
                        AddDefaultCityLocation();
                    }
                });
            } else {
                AddDefaultCityLocation();
            }
        })
        .fail(function () {
            AddDefaultCityLocation();
        });
    }
}

// Assign events handlers for city/location controls.
function AssignCityControls()
{
    $('#Location').autocomplete({
        source: function (request, response) {

            var cityName = request.term;
            if (cityName && cityName.length > 0) {

                $.getJSON($('#CurrentLang').val() + '/api/APIProfile/cities?cityName=' + cityName)
                    .done(function (data) {

                        var cities = [];
                        if (data) {

                            for (var index = 0; index < data.length; index++) {

                                var geoObject = data[index];
                                cities.push(geoObject);
                            }
                        }

                        response(cities);
                    });
            }
        },
        select: function (event, ui) {

            var geoObject = ui.item;

            AddNewCityLocation(geoObject.woeid, geoObject.town + ", " + geoObject.country, true, true);

            // Give a command to server: get all data about this city and save.
            $.ajax({
                method: 'get',
                url: $('#CurrentLang').val() + '/api/APIProfile/requestcity?woeid=' + geoObject.woeid,
                contentType: "application/json; charset=utf-8",
                success: function (data) {

                    var country = geoObject.country;

                    if (country.length > 11) {
                        country = country.substring(0, 8) + "...";
                    }

                    //AddNewCityLocation(geoObject.woeid, geoObject.town + ", " + geoObject.country, true, true);
                },
                error: function (data) {
                    ShowError(error);
                }
            });

            return false;
        }
    })
    .autocomplete('instance')._renderItem = function (ul, geoObject) {
        return $('<li>')
          .append('<a>' + geoObject.country + '&nbsp' + geoObject.town + (geoObject.admin ? ', ' + geoObject.admin : '') +
               (geoObject.admin1 ? ' ,' + geoObject.admin1 : '') + (geoObject.admin2 ? ' ,' + geoObject.admin2 : '') + '</a>')
          .appendTo(ul);
    };
}

// Add default city if no IP detection.
function AddDefaultCityLocation(ipcountry, ipcity)
{
    if (ipcountry && ipcity) {
        $.ajax({
            method: 'get',
            url: $('#CurrentLang').val() + '/api/APIProfile/city?cityName=' + ipcity + ', ' + ipcountry,
            contentType: "application/json; charset=utf-8",
            success: function (woeid) {

                if (ipcountry.length > 11) {
                    ipcountry = ipcountry.substring(0, 8) + "...";
                }

                AddNewCityLocation(woeid, ipcity + ', ' + ipcountry, true, true);
            },
            error: function (data) {
                if ($('#CurrentLang').val() == 'ru') {
                    AddNewCityLocation('484907', 'Taganrog, RU', false, true);
                } else {
                    AddNewCityLocation('5391959', 'San Francisco, US', false, true);
                }
            }
        });
    } else {
        if ($('#CurrentLang').val() == 'ru') {
            AddNewCityLocation('484907', 'Taganrog, RU', false, true);
        } else {
            AddNewCityLocation('5391959', 'San Francisco, US', false, true);
        }
    }
}

// Adds new location into list of locations.
function AddNewCityLocation(woeid, fullLocationName, save, selectActive)
{
    try {

        // Add new element into list.
        var numItems = $('.selectedContent').length;

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

        if (numItems >= 6) {

            ShowError(noMoreCities);
            return;
        }

        // Create Location element by copying from Template.
        var selectedLocationElem = document.getElementById('SelectedLocation');
        var newSelectedLocationElem = selectedLocationElem.cloneNode(true);
        newSelectedLocationElem.id = newSelectedLocationElem.id + guid();
        newSelectedLocationElem.style.display = "block";
        newSelectedLocationElem.setAttribute('woeid', woeid);
        $('.instaBlock').before(newSelectedLocationElem);

        var el = $("#" + newSelectedLocationElem.id + ' > .selectedContent');
        el.text(fullLocationName);

        $('#Location').val('');

        // Remember in cookies.
        var selectedCityCountry = $.cookie('SelectedCityCountry');
        var selectedWoeid = $.cookie('SelectedWoeid');

        if (selectedCityCountry && selectedWoeid)
        {
            selectedCityCountry = selectedCityCountry.replace(/;;/g, ';');
            selectedWoeid = selectedWoeid.replace(/;;/g, ';');

            if (save == true && fullLocationName && woeid && selectedCityCountry.indexOf(fullLocationName) < 0 && selectedWoeid.indexOf(woeid) < 0) {
                $.cookie('SelectedCityCountry', selectedCityCountry + ';' + fullLocationName, { path: '/' });
                $.cookie('SelectedWoeid', selectedWoeid + ';' + woeid, { path: '/' });
            }
        }
        else
        {
            if (save == true && fullLocationName && woeid) {
                $.cookie('SelectedCityCountry', fullLocationName, { path: '/' });
                $.cookie('SelectedWoeid', woeid, { path: '/' });
            }
        }

        if (selectActive == true)
        {
            SetActiveLocation(newSelectedLocationElem);
        }

    } catch (e) {

        LogError(e);
        isWorking = false;
    }
}

// Selects specified location to current one: loads all data for it by ajax.
function SetActiveLocation(newElement) {

    if (isWorking == true) {
        ShowError(pleaseWait);
        return;
    }

    try {
        isWorking = true;

        $('.selectedLocation').each(function () {

            $(this).removeClass('activeLocation');
        });

        newElement.className += ' activeLocation';

        var fullCityName = newElement.children[0].innerHTML;

        var woeid = newElement.children[0].getAttribute('woeid');
        var newWoeid = newElement.getAttribute('woeid');

        if (woeid && woeid != 'undefined') {
            GetSuggestionsForToday(woeid);
        }
        else if (newWoeid && newWoeid != 'undefined') {
            GetSuggestionsForToday(newWoeid);
        } else {
            ShowError('No woeid!');
        }
    } catch (e) {
        LogError(e);
        isWorking = false;
    }
}

// Gets forecasts by AJAX from server.
function GetSuggestionsForToday(woeid) {

    GetAllData(woeid, null, 0);
}

function GetSuggestionsForecast(element) {

    if (isWorking == true) {
        ShowError(pleaseWait);
        return;
    }

    element.className += 'suggestion-header-active';

    var date = element.getAttribute('date');
    var woeid = element.getAttribute('woeid');
    GetAllData(woeid, date, 0);
}

function GetInstagramPhotosByLocation(data)
{
    //try {
    //    var location = data.location.replace(' ', '').replace('-', '').replace('_', '');
    //    var feed = new Instafeed({
    //        get: 'tagged',
    //        limit: 8,
    //        tagName: location,
    //        template: "<a style='margin-bottom:5px;' href='{{link}}' target='_blank'><img src='{{image}}' /></a>",
    //        clientId: '08862dc6cee04f5184d800100bebaf58'
    //    });
    //    $('#instafeed > *').remove();
    //    feed.run();
    //    console.log('Get instagram photos for location: ' + data.location);
    //}
    //catch (e) {
    //}
}

function GetAllData(woeid, date, fresh)
{
    $('.weather-panel').css('visibility', 'hidden');
    $('.weather-panel').css('opacity', '0.0');

    $.getJSON($('#CurrentLang').val() + '/api/APISuggestions/weather?woeid=' + woeid + '&date=' + date)
         .done(function (data) {

             //GetInstagramPhotosByLocation(data);
             var activeTabName = $('.mainTab > li.active > a').attr('href');

             if (activeTabName == '#spring') {
                 $('#spring').show();
                 $('.suggestions-panel-conent > .tab-content > spring').addClass('active');
             } else if (activeTabName == '#summer') {
                 $('#summer').show();
                 $('.suggestions-panel-conent > .tab-content > summer').addClass('active');
             } else if (activeTabName == '#autumn') {
                 $('#autumn').show();
                 $('.suggestions-panel-conent > .tab-content > autumn').addClass('active');
             } else if (activeTabName == '#winter') {
                 $('#winter').show();
                 $('.suggestions-panel-conent > .tab-content > winter').addClass('active');
             }

             // Show weather data.
             $('.weatherConditionIcon').attr('src', '/img/wicons/' + data.conditionIcon);
             $('.weatherDescription').text(data.shortDescription + (data.windDirection ? ', ' + data.windDirection : ''));
             var temperature = '';
             if (data.effectiveTemperature == '?') {
                 temperature = data.temperature;
             } else {
                 temperature = data.effectiveTemperature;
             }
             $('.eftemperature').text(temperature);
             $('.system').text(data.temperatureUnits);
             $('.tempmax').text(data.temperatureMax + data.temperatureUnits);
             $('.tempmin').text(data.temperatureMin + data.temperatureUnits);
             $('.humidity > .subValue').text(data.athmosphereHumidity + data.humidityUnits);
             $('.pressure > .subValue').text(data.athmospherePressure);
             $('.pressure > .subDesc').text(data.pressureUnits);
             //$('.visibility > .subValue').text(data.athmosphereVisibility);
             //$('.visibility > .subDesc').text(data.distanceUnits);
             $('.windspeed > .subValue').text(data.windSpeed);
             $('.windspeed > .subDesc').text(data.speedUnits);
             $('.sunrise > .subValue').text(data.sunrise);
             $('.sunset > .subValue').text(data.sunset);

             if (data.iswindy == true) $('.windspeed').css("border-color", "red");
             if (data.athmosphereHumidity == '?') $('.humidity').hide();
             if (data.athmospherePressure == '?') $('.pressure').hide();
             //if (data.athmosphereVisibility == '?') $('.visibility').hide();
             if (data.windSpeed == '?') $('.windspeed').hide();
             if (data.sunrise == '?') $('.sunrise').hide();
             if (data.sunset == '?') $('.sunset').hide();

             $('.weather-panel').css('visibility', 'visible').animate({ opacity: 1.0 }, 1000);

             try {
                 // Create links for forecast on next dates.
                 if (data.forecasts) {

                     $('#SuggestionForecasts').empty();
                     var len = 0;
                     for (var forecast in data.forecasts) {
                         len++;
                     }
                     var empty = true;
                     for (var forecast in data.forecasts) {

                         empty = false;
                         // Create Forecast link element by copying from always existent element for Today.
                         var suggestionTodayElem = document.getElementById('SuggestionTodayTemplate');
                         var newSuggestionDateElem = suggestionTodayElem.cloneNode(true);
                         newSuggestionDateElem.id = '';
                         newSuggestionDateElem.style.display = "block";
                         var forecastDate = data.forecasts[forecast];
                         newSuggestionDateElem.childNodes[0].setAttribute('date', forecastDate);
                         newSuggestionDateElem.childNodes[0].setAttribute('woeid', woeid);
                         newSuggestionDateElem.childNodes[0].setAttribute('onclick', 'GetSuggestionsForecast(this);');
                         newSuggestionDateElem.childNodes[0].innerHTML = forecast.toUpperCase();
                         if (date)
                         {
                             if (date == forecastDate) {

                                 newSuggestionDateElem.className += ' suggestion-header-active';
                             }
                         }
                         else if (data.forecastDate.indexOf(forecastDate) > -1) {

                             newSuggestionDateElem.className += ' suggestion-header-active';
                         }

                         $('#SuggestionForecasts').append(newSuggestionDateElem);

                         // If not last.
                         if (len > 1) {
                             var splitterDiv = document.createElement('div');
                             splitterDiv.className = "suggestion-header-splitter";
                             splitterDiv.innerHTML = "&nbsp;";
                             $('#SuggestionForecasts').append(splitterDiv);
                         }

                         --len;
                     }

                     if (empty)
                     {
                         // Add fake object to keep markup.
                         var suggestionTodayElem = document.getElementById('SuggestionTodayTemplate');
                         var newSuggestionDateElem = suggestionTodayElem.cloneNode(true);
                         newSuggestionDateElem.id = '';
                         newSuggestionDateElem.style.display = "block";
                         $('#SuggestionForecasts').append(newSuggestionDateElem);
                         newSuggestionDateElem.childNodes[0].innerHTML = '&nbsp;';
                     }
                 }
             }
             catch (e) {
                 ShowError(jsForecastError);
                 LogError(e);
             }

             if (sessionStorage.getItem('accessToken'))
             {
                 $.ajaxSetup({
                     headers: {
                         'Authorization': 'Bearer ' + sessionStorage.getItem('accessToken')
                     }
                 });
             }

             // Add description to Share link
             var locationName = $('.activeLocation > .selectedContent').text();
             var shareDescription = 'Сегодня в ' + locationName + ' температура ' + temperature + ' ' + data.temperatureUnits + '. ';

             if ($('#CurrentLang').val() == 'en') {

                 shareDescription = 'Today in ' + locationName + ' temperature is ' + temperature + ' ' + data.temperatureUnits + '. ';
             }

             SearchClothes(woeid, date, fresh, shareDescription);
         })
         .fail(function (e) {
             LogError(e);
             isWorking = false;
         });
}

function SearchClothes(woeid, date, fresh, shareDescription)
{
    ShowProgressBar();

    // Get Suggestions.
    $.getJSON($('#CurrentLang').val() + '/api/APISuggestions/suggestion?woeid=' + woeid + '&fresh=' + fresh + '&date=' + date)
         .done(function (data) {
             try {
                 // Fill suggestions panel.
                 if (data) {
                     sessionStorage.setItem('suggestedItems', JSON.stringify(data));

                     if (data.forecastDescription) {
                         var specialNotesDiv = $('#SpecialNotes');
                         specialNotesDiv.html(data.forecastDescription);
                     }

                     ShowSuggestedItems(data, woeid, date);

                     $('.pluso').attr('data-description', shareDescription + data.forecastDescription);
                     $('.pluso').attr('data-title', shareDescription + data.forecastDescription);
                     $('meta[name=description]').attr('content', shareDescription + data.forecastDescription);
                     $('meta[property="og:description"]').attr('content', shareDescription + data.forecastDescription);
                     $('meta[property="og:title"]').attr('content', shareDescription + data.forecastDescription);
                 }

                 $('.suggestion-item').hover(
                   function () {
                       $(this).find('.suggestion-item-info').show();
                   }, function () {
                       $(this).find('.suggestion-item-info').hide();
                   });

                 HideProgressBar();
                 $('.clothes-panel').css('visibility', 'visible').animate({ opacity: 1.0 }, 1000);

                 if (data.commonSuggestedItems) {

                     var specificType = $("#commonSuggestionTypes option:selected").attr('data');
                     ReloadCommonItemsByFilter(data, specificType);
                 }

             } catch (e) {
                 LogError(e);
             }

             isWorking = false;
         })
         .fail(function (e) {
             LogError(e);
             isWorking = false;
         });
}

function ReloadCommonItemsByFilter(data, specificType) {

    if (data.commonSuggestedItems && specificType) {

        var dataArray = data.commonSuggestedItems[specificType];

        $('#FullSuggestionNotes > li').remove();
        var container = $('#FullSuggestionNotes');

        $.each(dataArray, function (i) {
            var item = dataArray[i];
            var li = $('<li/>')
                .addClass('phi-common-item')
                .text(item.name)
                .appendTo(container);
        });
    }
}

// Removes location from list of selected.
function CloseSelected(element) {

    if (isWorking == true) {
        ShowError(pleaseWait);
        return;
    }

    isWorking = true;

    var numItems = $('.selectedContent').length;

    if (numItems <= 2) {
        ShowError(dontDeleteCity);
        isWorking = false;
    } else {

        try {
            var elName = element.getAttribute('id');
            var id = 0;
            var counter = 0;
            $('#SelectedLocationContainer > .selectedLocation').each(function () {

                counter += 1;
                if ($(this).attr('id') == elName)
                {
                    id = counter;
                }
            });

            var selectedCityCountry = $.cookie('SelectedCityCountry');
            var selectedWoeid = $.cookie('SelectedWoeid');

            if (selectedCityCountry && selectedWoeid) {

                selectedCityCountry = selectedCityCountry.replace(/;;/g, ';');
                selectedWoeid = selectedWoeid.replace(/;;/g, ';');

                var ccPairs = selectedCityCountry.split(';');
                var woeids = selectedWoeid.split(';');
                selectedCityCountry = '';
                selectedWoeid = '';

                for (var i = 0; i < ccPairs.length && i < woeids.length; i++) {

                    if (id != i + 1 && ccPairs[i] && woeids[i]) {
                        selectedCityCountry += ccPairs[i] + ';';
                        selectedWoeid += woeids[i] + ';';
                    }
                }

                selectedCityCountry = selectedCityCountry.substring(0, selectedCityCountry.length - 1);
                selectedWoeid = selectedWoeid.substring(0, selectedCityCountry.length - 1);
                $.cookie('SelectedCityCountry', selectedCityCountry, { path: '/' });
                $.cookie('SelectedWoeid', selectedWoeid, { path: '/' });
            }

            var isActive = element.getAttribute('class').indexOf('activeLocation') >= 0;
            var container = document.getElementById('SelectedLocationContainer');
            container.removeChild(element);

            if (isActive)
            {
                // Set last location as Active.
                $('.selectedLocation').each(function () {

                    $(this).removeClass('activeLocation');
                });

                var last = $('.selectedLocation').last();

                if (last) {
                    last.addClass('activeLocation');
                    // Update suggestions and weather.
                    GetSuggestionsForToday(last.attr('woeid'));
                } else {
                    alert(jsError); // Nothing to select
                    isWorking = false;
                }
            }
            else
            {
                isWorking = false;
            }
        } catch (e) {

            LogError(e);
        }
    }

    window.event.cancelBubble = true;
    window.event.stopPropagation();
    return false;
}

function ShowSuggestedItems(data, woeid, date)
{
    var actionType = $("#actiontypes option:selected").attr('data');

    if (data.suggestedItems) {
        // Clear area.
        $('.listview-wrapper > div').remove();

        var itemsContainer = $('.listview-wrapper');
        var dataArray = data.suggestedItems[actionType];

        //var useGender = false;
        //var gender = false;
        //$("#divGender :button").each(function () {
        //    if ($(this).hasClass('active')) {
        //        useGender = true;
        //        if ($(this).children().hasClass('fa-female')) {
        //            gender = true;
        //        }
        //    }
        //});

        var numItems = 0;
        for (var dataItem in dataArray) {

            var item = dataArray[dataItem];

            // Clone template for clothing item.
            var suggestionItemElem = document.getElementById('SuggestionItemTemplate');
            var newSuggestionItemElem = suggestionItemElem.cloneNode(true);
            newSuggestionItemElem.id = newSuggestionItemElem.id + numItems;
            newSuggestionItemElem.style.display = "block";

            // Append cloned template into DIV elements
            var owlItemDiv = document.createElement('div');
            owlItemDiv.className = "item";
            owlItemDiv.appendChild(newSuggestionItemElem);

            itemsContainer.append(owlItemDiv);

            // Fill clothing items.
            newSuggestionItemElem.className += ' suggestion-item-active';
            var header = $("#" + newSuggestionItemElem.id + " > .suggestion-item-title");
            header.text(item.name);
            newSuggestionItemElem.title = item.description;

            var elImg = $("#" + newSuggestionItemElem.id + " > .suggestion-item-content > a > img");
            elImg.attr('src', item.imageUrl);

            var elHref = $("#" + newSuggestionItemElem.id + " > .suggestion-item-content > a");
            elHref.attr('href', item.referrerUrl);

            var elPrice = $("#" + newSuggestionItemElem.id + " > .suggestion-item-info > div > .suggestion-item-price");
            elPrice.text(item.price);

            var elVendor = $("#" + newSuggestionItemElem.id + " > .suggestion-item-info > div > .suggestion-item-vendor");
            elVendor.text(item.provideBy);

            ++numItems;
        }

        // More button
        if (woeid)
        {
            $(".moreButton").click(function () {
                SearchClothes(woeid, date, 1);
            });
        }
    }
}

function ShowProgressBar()
{
    $('.clothes-progress-bar').show();

    var opts = {
        lines: 13 // The number of lines to draw
        , length: 28 // The length of each line
        , width: 14 // The line thickness
        , radius: 42 // The radius of the inner circle
        , scale: 1 // Scales overall size of the spinner
        , corners: 1 // Corner roundness (0..1)
        , color: '#000' // #rgb or #rrggbb or array of colors
        , opacity: 0.25 // Opacity of the lines
        , rotate: 0 // The rotation offset
        , direction: 1 // 1: clockwise, -1: counterclockwise
        , speed: 1 // Rounds per second
        , trail: 60 // Afterglow percentage
        , fps: 20 // Frames per second when using setTimeout() as a fallback for CSS
        , zIndex: 2e9 // The z-index (defaults to 2000000000)
        , className: 'spinner' // The CSS class to assign to the spinner
        , top: '35%' // Top position relative to parent
        , left: '50%' // Left position relative to parent
        , shadow: false // Whether to render a shadow
        , hwaccel: false // Whether to use hardware acceleration
        , position: 'absolute' // Element positioning
    }
    var target = document.getElementById('progress-bar')
    var spinner = new Spinner(opts).spin(target);
}

function HideProgressBar() {
    $('.clothes-progress-bar').hide();

    $('#progress-bar > *').remove();
}