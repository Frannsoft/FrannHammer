'use strict';
app.controller('moveRankController', function ($scope, moveRankService, $routeParams) {

    $scope.moveName = $routeParams.name;
    $scope.moveOrder = $routeParams.orderBy;

    $scope.moveData = [];

    init();

    function init() {
        getRankData();
    }

    function getRankData() {
        moveRankService.query({ name: $scope.moveName }, function (result) {
            $scope.moveData = result;
        })
    }

});