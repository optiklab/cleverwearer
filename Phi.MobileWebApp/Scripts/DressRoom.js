$(document).ready(function () {
    $('#uploadButton').click(OnUpload);
    //$('#avatar').on('drop', drop);
});

//////////////////////////////////////////////////////////////////////////////////////////////////

function OnUpload(evt) {
    var files = $('#dress').get(0).files;
    if (files.length > 0) {
        if (window.FormData !== undefined) {
            var data = new FormData();
            for (i = 0; i < files.length && i < 1; i++) {
                data.append('file' + i, files[i]);
            }
            $.ajax({
                type: 'POST',
                url: location.protocol + '//' + location.host + '/' + $('#CurrentLang').val() + '/api/APIDressRoom/PostImage',
                contentType: false,
                processData: false,
                headers: {
                    'Authorization': 'Bearer ' + sessionStorage.getItem('accessToken')
                },
                data: data,
                success: function (filesInfoArray) {

                    if (filesInfoArray) {
                        // Remove all existent avatars.
                        $('.dress-ico').remove();

                        // Add new one.
                        $('.dress').append("<img class='dress-ico' src='" + filesInfoArray[0].path + "' width='100' height='100' />");

                        $('#ImageUrl').val(filesInfoArray[0].path);
                    }
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
                url: location.protocol + '//' + location.host + '/' + $('#CurrentLang').val() + '/api/APIDressRoom/PostImage',
                headers: {
                    'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
                },
                contentType: false,
                processData: false,
                data: data,
                success: function (filesInfoArray) {

                    if (filesInfoArray) {
                        // Remove all existent avatars.
                        $('.dress-ico').remove();

                        // Add new one.
                        $('.dress').append("<img class='dress-ico' src='" + filesInfoArray[0].path + "' width='100' height='100' />");

                        $('#ImageUrl').val(filesInfoArray[0].path);
                    }
                }
            });
        } else {
            alert('your browser sucks!');
        }
    }
}