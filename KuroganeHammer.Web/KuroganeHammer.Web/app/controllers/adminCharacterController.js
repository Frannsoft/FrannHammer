'use strict';
app.controller('adminCharacterController', function ($scope, $rootScope, characterService, characterMovementService,
    characterMovesService, movementService, moveService, $routeParams, $location, $resource) {

    $scope.characterId = $routeParams.characterId;
    $scope.characterName;
    $scope.mainImageUrl;
    $scope.characterStyle;
    $scope.characterDescription;
    $scope.ownerId;
    $scope.characterStyle;
    $scope.predicate = 'id';
    $scope.reverse = false;
    $scope.movementData = [];
    $scope.moveData = [];

    $scope.showNewMovementRow = false;
    $scope.newMovementStat;

    $scope.showNewMoveRow = false;
    $scope.newMoveStat;

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

    $scope.createNewMovementStat = function () {
        movementService.save({
            name: $scope.newMovementStat.name, value: $scope.newMovementStat.value,
            ownerId: $scope.ownerId
        });

        $scope.newMovementStat.name = '';
        $scope.newMovementStat.value = '';
        $scope.showMovementRow();
    }

    $scope.showMovementRow = function showMovementRow() {
        $scope.showNewMovementRow = !$scope.showNewMovementRow;
    };

    $scope.createNewMoveStat = function () {
        moveService.save({
            name: $scope.newMoveStat.name,
            hitboxActive: $scope.newMoveStat.hitboxActive,
            firstActionableFrame: $scope.newMoveStat.firstActionableFrame,
            baseDamage: $scope.newMoveStat.baseDamage,
            angle: $scope.newMoveStat.angle,
            baseKnockBackSetKnockback: $scope.newMoveStat.baseKnockBackSetKnockback,
            knockbackGrowth: $scope.newMoveStat.knockbackGrowth,
            landingLag: $scope.newMoveStat.landingLag,
            autoCancel: $scope.newMoveStat.autoCancel,
            ownerId: $scope.ownerId
        });

        $scope.newMoveStat.name = '';
        $scope.newMoveStat.hitboxActive = '';
        $scope.newMoveStat.firstActionableFrame = '';
        $scope.newMoveStat.baseDamage = '';
        $scope.newMoveStat.angle = '';
        $scope.newMoveStat.baseKnockBackSetKnockback = '';
        $scope.newMoveStat.knockbackGrowth = '';
        $scope.newMoveStat.landingLag = '';
        $scope.newMoveStat.autoCancel = '';

        $scope.showNewMoveRow();
    }

    $scope.showMoveRow = function showMoveRow() {
        $scope.showNewMoveRow = !$scope.showNewMoveRow;
    }

    $scope.deleteMovementRow = function (item) {
        movementService.get({ id: item.id }, function (response) {
            movementService.delete({ id: item.id });
            getCharacterMovementData(); //refresh
        });
    }

    $scope.deleteMoveRow = function (item) {
        moveService.get({ id: item.id }, function (response) {
            moveService.delete({ id: item.id });
            getCharacterMovesData(); //refresh
        });
    }

    $scope.order = function (predicate) {
        $scope.reverse = ($scope.predicate === predicate) ? !$scope.reverse : false;
        $scope.predicate = predicate;
    };
});