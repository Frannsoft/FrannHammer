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
        moveRankService.query({ name: $scope.moveName, orderBy: $scope.moveOrder }, function (result) {
            $scope.moveData = result;
        })
    }

});