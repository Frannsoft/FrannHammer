'use strict';
app.controller('moveRankController', function ($scope, moveRankService, $routeParams) {

    $scope.moveName = $routeParams.name;
    $scope.moveOrder = $routeParams.orderBy;
    $scope.predicate = 'firstActionableFrame';
    $scope.reverse = true;

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

    $scope.order = function (predicate) {
        $scope.reverse = ($scope.predicate === predicate) ? !$scope.reverse : false;
        $scope.predicate = predicate;
    };

});