﻿'use strict';
app.controller('movementRankController', function ($scope, movementRankService, $location,
    $routeParams) {

    $scope.movementName = $routeParams.name;
    $scope.predicate = 'value';
    $scope.reverse = true;
    

    $scope.movementData = [];

    init();

    function init() {
        getRankData();
    }

    function getRankData() {
        movementRankService.query({ name: $scope.movementName }, function (result) {
            $scope.movementData = result;
        });
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