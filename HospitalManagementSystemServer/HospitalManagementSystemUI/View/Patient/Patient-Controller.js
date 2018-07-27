(function () {
    var patientModel;
    angular.module('patient').controller(
        'patientController',
        [
            '$scope',
            'patientService',
            function ($scope, patientService) {

                init();
                function init() {

                    patientModel = buildPatientModel();
                    $scope.saveButtonClick = saveButtonClick;
                    $scope.clearButtonClick = clearButtonClick;
                    $scope.patientModel = patientModel;
                } //end of function init

                function buildPatientModel() {
                    return {
                        firstName: "",
                        lastName: "",
                        dateOfBirth:"",
                        gender:"select",
                        address1:"",
                        address2:"",
                        phoneNumber:"",
                        email:"",
                        patientType:"select",
                        password:"",
                        reEnterPassword:"",
                        securityQn:"",
                        securityAns:"",

                    }
                } //end of buildpatientModel

                function clearButtonClick() {
                    buildPatientModel();
                } //end of clear button click

                function saveButtonClick() {

                    var patientDetails = {
                        firstName: patientModel.firstName, lastName: patientModel.lastName, emailID: patientModel.emailID,
                        pwd: patientModel.password, dateOfBirth: patientModel.dateOfBirth, securityQn: patientModel.securityQn,
                        securityAns: patientModel.securityAns, phone: patientModel.phone, address: patientModel.address, gender: patientModel.gender,
                        patientType: patientModel.patientType
                    };

                    var save = patientService.registerPatient(patientDetails);

                }
            } //end of save New Patient 
        ]
    )

})(); //end of function