//history.pushState(null, null, location.href);
//window.onpopstate = function () {
//    history.go(1);
//};  // to stop going forward once went back.

////Method warnUser(){
////    return "User will be logged out";
////};
////to warn user when back button is clicked



//var urls = [
//    "Html/Nurse/NurseHomePage.html",
//    "Html/Lab Incharge/LabInchargeHomePage.html",
//    "Html/Doctor/DoctorHomePage.html",
//    "Html/Administrator/AdminHomePage.html",
//    "Html/Patient/PatientHomePage.html"
//]; //urls to redirect respective user





//myapp.controller('UserLoginController', function ($scope, $http) {

//    $scope.ValidateUser = function () {

//        var parameters = {
//            id: $scope.UserIdInput,
//            pwd: $scope.UserPwd
//        };
//        var config = {
//            params: parameters
//        };
//        $http.get('http://localhost:9635/api/UserLogin/Get', config)
//            .then(function (response, status, header, config) {
//                if (response.data[0] !== "False") {

//                    window.location.href = urls[response.data[1]];
//                    //$scope.result = response.data;    
//                } else if (response.data[0] === "False") {
//                    $scope.result = "Invalid credentials or User Is InAcitve";
//                } else { $scope.result = "Internal error please try again later"; }

               


//            })
//            //.error(function (response, status, header, config) {
//            //    $scope.errorLabel = "Data: " + response +
//            //        "<hr /> status: " + status +
//            //        "<hr />header: " + header +
//            //        "<hr />config: " + config;
//            //});
//    }; //end fucntion 
   
//}); //end controller

//window.onbeforeunload = function () { window.history.forward(1); };
