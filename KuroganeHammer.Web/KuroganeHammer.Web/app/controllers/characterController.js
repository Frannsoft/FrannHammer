'use strict';
app.controller('characterController', function ($scope, characterService, characterMovementService,
    characterMovesService, $routeParams, $location) {

    $scope.characterId = $routeParams.characterId;
    $scope.characterName;
    $scope.mainImageUrl;
    $scope.characterDescription;
    $scope.ownerId;
    $scope.characterStyle;

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

    $scope.go = function goToStat(item) {
        $location.path('/movement').search({ name: item.name });
        //why does this not work?
    }
});