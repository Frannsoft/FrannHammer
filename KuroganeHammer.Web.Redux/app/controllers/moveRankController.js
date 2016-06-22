'use strict';
app.controller('moveRankController', function ($scope, moveRankService, 
    $routeParams, $location) {

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

    $scope.goToCharacter = function (item) {
        var characterId = item.ownerId;
        $location.search('name', null);
        $location.path('/character/' + characterId);
    };
});