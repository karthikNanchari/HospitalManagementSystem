(function () {
    var viewModel;
    angular.module('login').controller(
        'loginController',
        [
            '$scope',
            'loginService',
            'registerUser',

            function ($scope, loginService, registerUser) {

                init();

                function init() {
                    viewModel = buildViewModel();
                    $scope.loginButtonClick = loginButtonClick;
                    $scope.registerButtonClick = registerButtonClick;
                    $scope.viewModel = viewModel;

                }

                function buildViewModel() {
                    return {
                        userName: "",
                        password: "",
                        displayTitle: true,
                        title: "Welcome To Login Page",
                        displayError: false
                    }
                }

                function loginButtonClick() {
                    var data = {
                        id: viewModel.userName, //7006,
                        pwd: viewModel.password   //'harry123'
                    };

                    loginService.validateUser(data)
                        .then(function (data) {
                            if (!isNaN(data)) {
                                if (data === -1) {
                                    viewModel.errorLabel = "Internal Error";
                                    displayTitle = false;
                                    viewModel.displayError = true;
                                }
                                if (data === 0) {
                                    viewModel.errorLabel = "Invalid UserName/Password";
                                    viewModel.displayError = true;
                                    viewModel.displayTitle = false;
                                }
                                if (data === 1) {
                                    window.location.replace("../Nurse/NurseHomePage.html");
                                }
                                if (data === 2) {
                                    window.location.replace("../Lab Incharge/LabInchargeHome.html");
                                }
                                if (data === 3) {
                                    window.location.replace("../Doctor/DoctorLoginPage.html");
                                }
                                if (data === 4) {
                                    window.location.replace("../Administrator/AdminHomePage.html");
                                }
                                if (data === 5) {
                                    window.location.replace("../Patient/patientHome.html");
                                }
                            }
                        });
                } //end of loginButtonClick
                
                function registerButtonClick() {
                    var patientDetails = $.param({
                        
                        lastName: 'kane',
                        emailID: 'john.k@gmail.com',
                        pwd: 'John.Kane',
                        dateOfBirth: viewModel.birthDate,
                        securityQn:'What is your Favourite Movie Name' ,
                        securityAns:'Avengers',
                        phone:'(913)-123-1234' ,
                        address:'Exton,PA' ,
                        gender:'Male' ,
                        patientType: 'InPatient',
                        firstName: viewModel.firstName //'John'
                    })

                    registerUser.newUser(patientDetails)
                        .then(function (data) {
                        });
                } //end of registerButtonClick

            }
        ]
    );//end of login controller

})(); //end of function



