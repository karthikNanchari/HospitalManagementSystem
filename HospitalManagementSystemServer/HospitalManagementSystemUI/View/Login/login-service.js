(function () {

    angular.module('login').factory('loginService',

        function ($http) {

            var loginService = {

                validateUser: function (data) {

                    var config = {
                        params: data,
                        headers: { 'Accept': 'application/json' }
                       
                    };

                    var baseURL = "http://localhost:9635/api";

                    var promise = $http.get(baseURL + "/UserLogin/Authenticate", config)
                        .then(function (response) {
                            return response.data;
                        });

                    return promise;
                }// end of validateUser
            };
            return loginService;
        },
    );

    angular.module('login').factory('registerUser',

         function($http) {

                var registerUser = {

                    newUser: function (data) {

                        var config = {
                            //data:  data 
                            headers: {
                                'Accept': 'application/json',
                                'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8'
                            }
                        };

                        var baseURL = "http://localhost:9635/api";

                        var promise = $http.post(baseURL + "/Patient/RegisterNewUser", data, config
                            //{
                        //    headers: {
                        //        'Accept': 'application/json',
                        //        'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8'
                            //         } }
                        )
                            .then(function (response) {
                                return response.data;
                            });
                        
                        return promise;
                    } //end of newUser
                }; //end of register User

                return registerUser;

            } // end of function
       );  //end of app factory
})(); //end of function
           

 


