'use strict';
app.controller('characterController', function ($scope, characterService, characterMovementService,
    characterMovesService, $routeParams, $location) {

    $scope.characterId = $routeParams.characterId;
    $scope.characterName;
    $scope.mainImageUrl;
    $scope.characterDescription;
    $scope.ownerId;
    $scope.characterStyle;
    $scope.predicate = 'id';
    $scope.reverse = false;
    $scope.movementData = [];
    $scope.moveData = [];

    init();

    function init() {
        getCharacterData();
        getCharacterMovementData();
        getCharacterMovesData();
    }

    function getCharacterData() {
        characterService.get({ id: $scope.characterId }, function (characterDataResult) {
            $scope.characterName = characterDataResult.name;
            $scope.mainImageUrl = characterDataResult.mainImageUrl;
            $scope.characterDescription = characterDataResult.description;
            $scope.ownerId = characterDataResult.ownerId;
            $scope.characterStyle = characterDataResult.style;
        });
    }

    function getCharacterMovementData() {
        characterMovementService.query({ id: $scope.characterId }, function (result) {
            $scope.movementData = result;
        });
    }

    function getCharacterMovesData() {
        characterMovesService.query({ id: $scope.characterId }, function(result){
            $scope.moveData = result;
        });
    }

    $scope.goToMovementRanking = function goToMovementRanking(item) {
        $location.path('/movement').search({ name: item.name });
    }

    $scope.goToMoveRanking = function goToMoveRanking(item) {
        $location.path('/moveRanking').search({ name: item.name });
    }

    $scope.order = function (predicate) {
        $scope.reverse = ($scope.predicate === predicate) ? !$scope.reverse : false;
        $scope.predicate = predicate;
    };
});