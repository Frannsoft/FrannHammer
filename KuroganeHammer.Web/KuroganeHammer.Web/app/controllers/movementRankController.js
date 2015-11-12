'use strict';
app.controller('movementRankController', function ($scope, movementRankService, $routeParams) {

    $scope.movementName = $routeParams.name;

    $scope.movementData = [];

    init();

    function init() {
        getRankData();
    }

    function getRankData() {
        movementRankService.get({ name: $scope.movementName }, function (result) {
            $scope.movementData = result;
        });
    }

});