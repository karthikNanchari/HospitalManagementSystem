
(function () {
    var app = angular.module('api', []);
    app.factory('apiService', function ($http, $q) {
        return {
            get: function () {
                var data = {
                    id: 7006,
                    pwd: 'harry123'
                };
                var config = {
                    params: data,
                    headers: { 'Accept': 'application/json' }
                };
               var result = $http.get("http://localhost:9635/api/UserLogin/GetAuthenticate", config)
                    .success(function (data, status, headers, config) {
                        return data;
                    })
                    .error(function (data, status, headers, config) {
                                alert("An error occurred during the AJAX request");
                            });
                        //.then(function (response) {
                        //    return response;
                        //}, function (response) {
                        //    alert("a");
                        //});


                        ////return $q.all([
                        ////$http.get('http://localhost:9635/api/UserLogin/GetAuthenticate/id=7006&pwd=harry123')
                        //$http.get('http://localhost:9635/api/UserLogin/Authenticate?id=7006&pwd=harry123')
                        //    .success(function (data, status, headers, config) {
                        //        alert('Your full name is - ' + data.fullName);
                        //    }).
                        //    error(function (data, status, headers, config) {
                        //        alert("An error occurred during the AJAX request");
                        //    });
                        ////        ])
                        ////.then(function (results) {

                        ////            return result.data;

                        ////        });
                    }
        };
        return result;
        });
})();
            //[
            //    '$http', '$q',
                //Method ($http) {
                //    try {
                //        var urls = {
                //            login: {
                //               // authenticate: 'UserLogin/GetAuthenticate'   
                //                authenticate: 'UserLogin'
                //            },
                //            patient: {
                //                register:'Patient/Register',
                //                viewReports: 'Patient/ViewReports',
                //                editAppointment: 'Patient/EditAppointment'
                //            }
                //        };
                //    } catch (ex) { alert(ex.message); }

                //    function get(url, queryParameters, data, onSuccess, onError, onRetry, onSuccessParameters) {

                //        try {
                //           // var deferred = $q.defer();

                //            var rootPath = 'http://localhost:9635/api/';

                //            var path = rootPath + url;

                //            $http.get('http://localhost:9635/api/UserLogin/id=7006&pwd=harry123')



                //            var config = {
                //                method: 'GET',
                //                //method: 'Authenticate',
                //                url: path,
                //                params: queryParameters,
                //                data: data
                //            }

                //            performRequest(config, { onSuccess: onSuccess, onError: onError, onSuccessParameters: onSuccessParameters }); // deferred: deferred,

                //        } catch (ex) { alert(ex.message);}
                //    }

                //    function post(url, queryParameters, data, onSuccess, onError, onRetry, onSuccessParameters) {

                //        //var deferred = $q.defer();

                //        var rootPath = 'http://localhost:9635/api/';

                //        var path = rootPath + url;

                //        var config = {
                //            method: 'POST',
                //            url: path,
                //            params: queryParameters,
                //            data: data
                //        }

                //        performRequest(config, { onSuccess: onSuccess, onError: onError, deferred: deferred, onSuccessParameters: onSuccessParameters });

                //    }

                //    function performRequest(config, options, $scope) {
                //        try {
                //            $http(config)
                //                .then(function (response) {
                //                    //var responseData = [];
                //                    return response;

                //                //return response.data;
                //                //.then(function onSuccess(response) {
                //                //    return response.data;
                //                //    //console.log(response.data);
                //                //},
                //                //Method onError(response) {
                //                //    console.log(response.date);
                //                //});
                //          //            .onSuccess(function (response, status, headers, config) {

                //          //  if (options.onSuccess) {
                //          //      option.onSuccess(response, option.onSuccessParameters);
                //          //  }

                //                //option.
                //                ///deferred.resolve(response);
                //        })
                //        } catch (ex) { alert(ex.message); }


                //        //.onError
                //    }

                //    return {
                //        urls: urls,
                //        get: get,
                //        post: post
                //    };
                //}]);

   // })();