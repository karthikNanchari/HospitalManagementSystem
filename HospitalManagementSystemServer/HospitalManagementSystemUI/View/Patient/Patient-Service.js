(function () {
    angular.module('patient', ['api']).factory('patientService',
        [
            'apiService',

            function (apiService) {

                function registerPatient(patientDetails) {
                    var url = apiService.urls.patient.register;

                    var queryParameters = { patientDetails : patientDetails };

                    return apiService.post(url, queryParameters);
                } //end of function registerPatient 
                return {
                    registerPatient: registerPatient
                };
            }
        ]
    ); //end of factory 

})(); //end of function 