'use strict';
app.controller('movementRankController', function ($scope, movementRankService, $routeParams) {

    $scope.movementName = $routeParams.name;
    $scope.movementOrder = $routeParams.orderBy;

    $scope.movementData = [];

    init();

    function init() {
        getRankData();
    }

    function getRankData() {
        movementRankService.query({ name: $scope.movementName, orderBy: $scope.movementOrder }, function (result) {
            $scope.movementData = result;
        });
    }

});