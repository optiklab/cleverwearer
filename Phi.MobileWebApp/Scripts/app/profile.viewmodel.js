function ProfileViewModel(app, dataModel) {
    var self = this;

//    self.myHometown = ko.observable("");

//    Sammy(function () {
//        this.postavatar('#apiprofile', function () {
//            // Make a call to the protected Web API by passing in a Bearer Authorization Header
//            $.ajax({
//                method: 'get',
//                url: app.dataModel.userInfoUrl,
//                contentType: "application/json; charset=utf-8",
//                headers: {
//                    'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
//                },
//                success: function (data) {
//                    self.myHometown('Your Hometown is : ' + data.hometown);
//                }
//            });
//        });
//        //this.get('/', function () { this.app.runRoute('get', '#profile') });
//    });

    return self;
}

app.addViewModel({
    name: "APIProfile",
    bindingMemberName: "apiprofile",
    factory: ProfileViewModel
});
