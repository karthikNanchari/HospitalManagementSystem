(function () {
    var viewModel;
    angular.module('login').controller(
        'loginController',
        [
            '$scope',
            'loginService',
            'registerUser',
            'forgotPasswordEmail',

            function ($scope, loginService, registerUser, forgotPasswordEmail) {

                init();

                function init() {
                    viewModel = buildViewModel();
                    $scope.loginButtonClick = loginButtonClick;
                    $scope.registerButtonClick = registerButtonClick;
                    $scope.forgotPasswordEmailClick = forgotPasswordEmailClick;
                    $scope.viewModel = viewModel;
                }

                function buildViewModel() {
                    return {
                        'loginUserName' : "",
                        'loginPassword': "",
                        'displayTitle': true,
                        'title': "Welcome To Login Page",
                        'displayError': false
                    }
                }

                function loginButtonClick() {
                    var data = {
                        id: $scope.viewModel.loginUserName, //7006,
                        pwd: $scope.viewModel.loginPassword   //'harry123'
                    };

                    var UserID = $scope.viewModel.loginUserName;
                    loginService.validateUser(data)
                        .then(function (data) {

                            if (!isNaN(data)) {
                                if (data === -1) {
                                    $scope.viewModel.errorLabel = "Invalid UserName/Password";
                                    $scope.viewModel.displayTitle = false;
                                    $scope.viewModel.displayError = true;
                                }
                                if (data === 0) {

                                    $scope.viewModel.errorLabel = "Invalid UserName/Password";
                                    $scope.viewModel.displayError = true;
                                    $scope.viewModel.displayTitle = false;
                                }
                                if (data === 1) {

                                        var loginDetails =
                                     {
                                         "userRole": "nurse",
                                         "id": $scope.viewModel.loginUserName
                                     };
                                    $.ajax({
                                        url: '/Login/SetSessions',
                                        data: JSON.stringify(loginDetails),
                                        type: 'POST',
                                        contentType: 'application/json; charset=utf-8',
                                    });

                                    window.location.href = "http://localhost:39697/Nurse?id=" + UserID;
                                    $scope.viewModel.displayError = false;
                                    $scope.viewModel.displayTitle = true;
                                }
                                if (data === 2) {


                                    var loginDetails =
                                   {
                                       "userRole": "labIncharge",
                                       "id": $scope.viewModel.loginUserName
                                   };
                                    console.log("test");
                                    $.ajax({
                                        url: '/Login/SetSessions',
                                        data: JSON.stringify(loginDetails),
                                        type: 'POST',
                                        contentType: 'application/json; charset=utf-8',
                                    });

                                    window.location.href = "http://localhost:39697/LabIncharge?id=" + UserID;
                                    $scope.viewModel.displayError = false;
                                    $scope.viewModel.displayTitle = true;

                                }
                                if (data === 3) {

                                        var loginDetails =
                                     {
                                         "userRole": "doctor",
                                         "id": $scope.viewModel.loginUserName
                                     };
                                        console.log("test");
                                    $.ajax({
                                        url: '/Login/SetSessions',
                                        data: JSON.stringify(loginDetails),
                                        type: 'POST',
                                        contentType: 'application/json; charset=utf-8',
                                    });

                                    window.location.href = "http://localhost:39697/Doctor?id=" + UserID;
                                    $scope.viewModel.displayError = false;
                                    $scope.viewModel.displayTitle = true;


                                }
                                if (data === 4) {

                                    var loginDetails =
                                   {
                                       "userRole": "admin",
                                       "id": $scope.viewModel.loginUserName
                                   };
                                    console.log($scope.viewModel.loginUserName);
                                    $.ajax({
                                        url: '/Login/SetSessions',
                                        data: JSON.stringify(loginDetails),
                                        type: 'POST',
                                        contentType: 'application/json; charset=utf-8',
                                    });
                                    window.location.href = "http://localhost:39697/Administrator?id=" + UserID;
                                    $scope.viewModel.displayError = false;
                                    $scope.viewModel.displayTitle = true;

                                }
                                if (data === 5) {

                                    var loginDetails =
                                      {
                                          "userRole": "patient",
                                          "id": $scope.viewModel.loginUserName
                                      };
                                    console.log($scope.viewModel.loginUserName);
                                    $.ajax({
                                        url: '/Login/SetSessions',
                                        data: JSON.stringify(loginDetails),
                                        type: 'POST',
                                        contentType: 'application/json; charset=utf-8',
                                    });

                                    window.location.href = "http://localhost:39697/Patient?id=" + UserID;
                                    $scope.viewModel.displayError = false;
                                    $scope.viewModel.displayTitle = true;
                                }
                            }
                        });
                } //end of loginButtonClick
                
                function registerButtonClick() {
                    //console.log($scope.viewModel.roleType);
                    var date = $scope.viewModel.birthDate;
                    console.log($scope.viewModel.roleType);

                    var val = 0;
                    if ($scope.viewModel.roleType == 1) {
                        // 1 -patient
                        val = 1;


                        var peronDetails = $.param({

                            firstName: $scope.viewModel.firstName, //'John'
                            lastName: $scope.viewModel.lastName,//'kane',
                            emailID: $scope.viewModel.email, //'john.k@gmail.com',
                            pwd: $scope.viewModel.passwrd,//'John.Kane',
                            securityQn: $scope.viewModel.securityQuestion,//'What is your Favourite Movie Name' ,
                            securityAns: $scope.viewModel.securityAnswer,//'Avengers',
                            phone: $scope.viewModel.phoneNumber,//'(913)-123-1234' ,
                            address: $scope.viewModel.addressLine1,//'Exton,PA' ,
                            gender: $scope.viewModel.gender,//'Male' ,
                            dateOfBirth: $scope.viewModel.birthDate,
                            patientType: $scope.viewModel.patientType //'Inpatient'
                        })


                    }
                    else if ($scope.viewModel.roleType == 2) {
                        //2-doctor
                        val = 2;

                        var peronDetails = $.param({

                            firstName: $scope.viewModel.firstName, //'John'
                            lastName: $scope.viewModel.lastName,//'kane',
                            emailID: $scope.viewModel.email, //'john.k@gmail.com',
                            pwd: $scope.viewModel.passwrd,//'John.Kane',
                            dateOfBirth: $scope.viewModel.birthDate,
                            securityQn: $scope.viewModel.securityQuestion,//'What is your Favourite Movie Name' ,
                            securityAns: $scope.viewModel.securityAnswer,//'Avengers',
                            phone: $scope.viewModel.phoneNumber,//'(913)-123-1234' ,
                            address: $scope.viewModel.addressLine1,//'Exton,PA' ,
                            gender: $scope.viewModel.gender,//'Male' ,
                            drDesignation: $scope.viewModel.doctorDesignation,
                            drDepartment: $scope.viewModel.doctorDepartment
                        })
                    }
                    else if ($scope.viewModel.roleType == 3) {
                        //3- admin
                        val = 3;

                        var peronDetails = $.param({

                            firstName: $scope.viewModel.firstName, //'John'
                            lastName: $scope.viewModel.lastName,//'kane',
                            emailID: $scope.viewModel.email, //'john.k@gmail.com',
                            pwd: $scope.viewModel.passwrd,//'John.Kane',
                            dateOfBirth: $scope.viewModel.birthDate,
                            securityQn: $scope.viewModel.securityQuestion,//'What is your Favourite Movie Name' ,
                            securityAns: $scope.viewModel.securityAnswer,//'Avengers',
                            phone: $scope.viewModel.phoneNumber,//'(913)-123-1234' ,
                            address: $scope.viewModel.addressLine1,//'Exton,PA' ,
                            gender: $scope.viewModel.gender,//'Male' ,
                        })
                    }
                    else if ($scope.viewModel.roleType == 4) {
                        //4- nurse
                        val = 4;

                        var peronDetails = $.param({

                            firstName: $scope.viewModel.firstName, //'John'
                            lastName: $scope.viewModel.lastName,//'kane',
                            emailID: $scope.viewModel.email, //'john.k@gmail.com',
                            pwd: $scope.viewModel.passwrd,//'John.Kane',
                            dateOfBirth: $scope.viewModel.birthDate,
                            securityQn: $scope.viewModel.securityQuestion,//'What is your Favourite Movie Name' ,
                            securityAns: $scope.viewModel.securityAnswer,//'Avengers',
                            phone: $scope.viewModel.phoneNumber,//'(913)-123-1234' ,
                            address: $scope.viewModel.addressLine1,//'Exton,PA' ,
                            gender: $scope.viewModel.gender,//'Male' ,
                        })
                    }
                    else if ($scope.viewModel.roleType == 5) {
                        //5- labincharge
                        val = 5;

                        var peronDetails = $.param({

                            firstName: $scope.viewModel.firstName, //'John'
                            lastName: $scope.viewModel.lastName,//'kane',
                            emailID: $scope.viewModel.email, //'john.k@gmail.com',
                            pwd: $scope.viewModel.passwrd,//'John.Kane',
                            dateOfBirth: $scope.viewModel.birthDate,
                            securityQn: $scope.viewModel.securityQuestion,//'What is your Favourite Movie Name' ,
                            securityAns: $scope.viewModel.securityAnswer,//'Avengers',
                            phone: $scope.viewModel.phoneNumber,//'(913)-123-1234' ,
                            address: $scope.viewModel.addressLine1,//'Exton,PA' ,
                            gender: $scope.viewModel.gender,//'Male' ,
                        })
                    }
                    console.log(peronDetails);
                    registerUser.newUser(peronDetails, val)
                        .then(function (data) {
                            $scope.viewModel.newRegistrationNumber = data;
                            $scope.viewModel.displayRegistrationNumber = true;
                        });
                } //end of registerButtonClick

                function forgotPasswordEmailClick() {
                    console.log(viewModel.emailForgot);
                    var userDetails = $.param({
                        pid: viewModel.roleType,
                        emailID: viewModel.emailForgot //'john.k@gmail.com',
                       
                    })

                    forgotPasswordEmail.userForgotPass(userDetails)
                        .then(function (data) {
                            viewModel.checkLabel = data;
                            viewModel.displayCheck = true;
                        });
                }
            }
        ]
    );//end of login controller

})(); //end of function



