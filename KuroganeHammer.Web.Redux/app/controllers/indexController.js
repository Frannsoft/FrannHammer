'use strict';
app.controller('indexController', ['$scope', '$location', '$window', 'authService', function ($scope, $location, $window, authService) {

    $scope.logOut = function () {
        authService.logOut();
        $location.path('/');
        //$route.reload();
        $window.location.reload();
    }

    $scope.authentication = authService.authentication;

}]);