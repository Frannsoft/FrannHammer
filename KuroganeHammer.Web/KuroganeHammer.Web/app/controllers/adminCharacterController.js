'use strict';
app.controller('adminCharacterController', function ($scope, $rootScope, characterService, characterMovementService,
    characterMovesService, movementService, moveService, $routeParams, $location, $resource) {

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
        characterMovesService.query({ id: $scope.characterId }, function (result) {
            $scope.moveData = result;
        });
    }

    $scope.saveChanges = function saveAllChanges() {

        characterService.get({ id: $scope.characterId }, function (chara) {
            chara.name = $scope.characterName;
            characterService.update({ id: $scope.characterId }, chara);
        });
    }

    $scope.saveMovementRow = function saveMovementRow(item) {
        movementService.get({ id: item.id }, function (response) {
            response.name = item.name;
            response.value = item.value;
            movementService.update({ id: item.id }, response);
        });
    }

    $scope.saveMoveRow = function saveMoveRow(item) {
        moveService.get({ id: item.id }, function (response) {
            response.hitboxActive = item.hitboxActive;
            response.firstActionableFrame = item.firstActionableFrame;
            response.baseDamage = item.baseDamage;
            response.angle = item.angle;
            response.baseKnockBackSetKnockback = item.baseKnockBackSetKnockback;
            response.landingLag = item.landingLag;
            response.autoCancel = item.autoCancel;
            response.knockbackGrowth = item.knockbackGrowth;
            response.type = item.type;
            response.characterName = item.characterName;
            response.id = item.id;
            response.thumbnailUrl = item.thumbnailUrl;
            response.ownerId = item.ownerId;
            response.name = item.name;
            
            moveService.update({ id: item.id }, response);
        });
    }

    $scope.order = function (predicate) {
        $scope.reverse = ($scope.predicate === predicate) ? !$scope.reverse : false;
        $scope.predicate = predicate;
    };
});