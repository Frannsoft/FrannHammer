'use strict';
app.controller('homeController', ['$scope', '$location', 'authService', function ($scope, $location, authService) {

    $scope.logOut = function () {
        authService.logOut();
        $location.path('/');
        //$route.reload();
        $window.location.reload();
    }

    $scope.authentication = authService.authentication;
}]);