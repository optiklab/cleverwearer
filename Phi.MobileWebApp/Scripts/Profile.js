$(document).ready(function () {
    $('#uploadButton').click(OnUpload);
    //$('#avatar').on('drop', drop);

    $('#Location').autocomplete({
        source: function (request, response) {

            $('#IsCheckedLocation').val('false');
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
        //focus: function (event, ui) {
        //    $('#Location').val(geoObject.town);
        //    return false;
        //},
        select: function (event, ui) {

            var geoObject = ui.item;
            $('#Location').val(geoObject.country + ', ' + geoObject.town + (geoObject.admin ? ', ' + geoObject.admin : '') + (geoObject.admin1 ? ', ' + geoObject.admin1 : '') + (geoObject.admin2 ? ', ' + geoObject.admin2 : ''));
            $('#IsCheckedLocation').val('true');

            // Give a command to server: get all data about this city and save.
            $.ajax({
                method: 'get',
                url: location.protocol + '//' + location.host + '/' + $('#CurrentLang').val() + '/api/APIProfile/requestcity?woeid=' + geoObject.woeid,
                contentType: 'application/json; charset=utf-8',
                //headers: {
                //    'Authorization': 'Bearer ' + sessionStorage.getItem('accessToken')
                //},
                success: function (data) {
                }
            });

            return false;
        }
    }).autocomplete('instance')._renderItem = function (ul, geoObject) {
        return $('<li>')
          .append('<a><b>' + geoObject.country + '</b>, ' + geoObject.town + (geoObject.admin ? ', ' + geoObject.admin : '') + (geoObject.admin1 ? ', ' + geoObject.admin1 : '') + (geoObject.admin2 ? ', ' + geoObject.admin2 : '') + '</a>')
          .appendTo(ul);
    };
});

//////////////////////////////////////////////////////////////////////////////////////////////////

function OnUpload(evt) {
    var files = $('#avatar').get(0).files;
    if (files.length > 0) {
        if (window.FormData !== undefined) {
            var data = new FormData();
            for (i = 0; i < files.length && i < 1; i++) {
                data.append('file' + i, files[i]);
            }
            $.ajax({
                type: 'POST',
                url: location.protocol + '//' + location.host + '/' + $('#CurrentLang').val() + '/api/APIProfile/PostAvatar',
                contentType: false,
                processData: false,
                headers: {
                    'Authorization': 'Bearer ' + sessionStorage.getItem('accessToken')
                },
                data: data,
                success: function (filesInfoArray) {

                    if (filesInfoArray) {
                        // Remove all existent avatars.
                        $('.avatar-ico').remove();

                        // Add new one.
                        $('.avatar').append("<img class='avatar-ico' src='" + filesInfoArray[0].path + "' width='100' height='100' />");
                    }
                },
                error: function (error) {

                    alert(error.responseText);
                }
            });
        } else {
            alert('This browser doesnt support HTML5 multiple file uploads!');
        }
    }
}

function drop(evt) {
    evt.stopPropagation();
    evt.preventDefault();
    $(evt.target).removeClass('over');

    var files = evt.originalEvent.dataTransfer.files;

    if (files.length > 0) {
        if (window.FormData !== undefined) {
            var data = new FormData();
            // Accept only 1 first file.
            for (i = 0; i < files.length && i < 1; i++) {
                data.append("file" + i, files[i]);
            }

            $.ajax({
                type: 'POST',
                url: location.protocol + '//' + location.host + '/' + $('#CurrentLang').val() + '/api/APIProfile/PostAvatar',
                headers: {
                    'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
                },
                contentType: false,
                processData: false,
                data: data,
                success: function (filesInfoArray) {

                    if (filesInfoArray)
                    {
                        // Remove all existent avatars.
                        $('.avatar-ico').remove();

                        // Add new one.
                        $('.avatar').append("<img class='avatar-ico' src='" + filesInfoArray[0].path + "' width='100' height='100' />");
                    }
                }
            });
        } else {
            alert('your browser sucks!');
        }
    }
}