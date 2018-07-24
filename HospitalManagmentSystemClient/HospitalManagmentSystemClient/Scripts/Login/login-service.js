(function () {

    angular.module('login').factory ('loginService',

        function ($http) {

            var loginService = {

                validateUser: function (data) {

                    var config = {
                        params: data,
                        headers: { 'Accept': 'application/json' }
                       
                    };

                    var baseURL = "http://localhost:9635/api";

                    var promise = $http.get(baseURL + "/UserLogin/GetAuthenticate", config)
                        .then(function (response) {
                            return response.data;
                        });

                    return promise;
                }// end of validateUser
            };
            return loginService;
        }),
    

            angular.module('login').factory ('registerUser',

                 function($http) {

                     var registerUser = {

                         newUser: function (data, val) {
                             console.log(val);
                             var registerURL = "";
                             if (val == 1) { registerURL = "/Patient/RegisterNewUser"; }
                             if (val == 2) { registerURL = "/Doctor/RegisterNewDoctor"; }
                             if (val == 3) { registerURL = "/Administrator/RegisterNewAdmin"; }
                             if (val == 4) { registerURL = "/Nurse/RegisterNewNurse"; }
                             if (val == 5) { registerURL = "/LabIncharge/RegisterNewIncharge"; }


                             var config = {
                                 //data:  data 
                                 headers: {
                                     'Accept': 'application/json',
                                     'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8'  }
                             };

                             var baseURL = "http://localhost:9635/api";
                             console.log(registerURL);
                             var promise = $http.post(baseURL + registerURL, data, config )
                                 .then(function (response) {
                                     return response.data;
                                 });
                             return promise;
                         } //end of newUser
                     }; //end of register User

                     return registerUser;

                 }),
            

            angular.module('login').factory('forgotPasswordEmail',

                function ($http) {
                    var forgotPasswordEmail ={
                        userForgotPass: function(data){
                            var config = {
                                headers: {
                                    'Accept': 'application/json',
                                    'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8'
                                }
                            };

                            var baseURL = "http://localhost:9635/api";

                            var promise = $http.post(baseURL + "/UserLogin/UserForgotPassword",data, config)
                                .then(function (response) {
                                    return response.data;
                                });

                            return promise;
                        }
                    };
                    return forgotPasswordEmail;
                }
               
            ) })(); //end of function
           

 


