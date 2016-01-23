'use strict';
app.controller('adminNewCharacterController', function ($scope, $rootScope, characterService,
    characterMovementService, characterMovesService, movementService, moveService, $routeParams,
    $resource) {


    $scope.characterName;
    $scope.mainImageUrl;
    $scope.characterDescription;
    $scope.characterStyle;
    $scope.movementData = [];
    $scope.moveData = [];
    $scope.tileImageUrl;

    $scope.newMovementStat;
    $scope.newMoveStat;
    $scope.tempMovementData = [];
    $scope.tempMoveData = [];

    $scope.saveCharacter = function saveNewCharacter() {

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

    $scope.createNewMovementStat = function createNewMovementStat() {
        movementService.save({
            name: $scope.newMovementStat.name, value: $scope.newMovementStat.value,
            ownerId: $scope.ownerId
        });

        $scope.newMovementStat.name = '';
        $scope.newMovementStat.value = '';
    }

    $scope.createNewMoveStat = function createNewMoveStat() {
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

    }

    $scope.createNewMovementRow = function createNewMovementRow() {
        $scope.tempMovementData.push({ name: '', value: '' });
    };

    $scope.createNewMoveRow = function createNewMoveRow() {
        $scope.tempMoveData.push({
            name: '',
            hitboxActive: '',
            firstActionableFrame: '',
            baseDamage: '',
            angle: '',
            backKnockBackSetKnockback: '',
            knockbackGrowth: '',
            landingLag: '',
            autoCancel: ''
        })
    };

    //$scope.showMovementRow = function showMovementRow() {
    //    $scope.showNewMovementRow = !$scope.showNewMovementRow;
    //};

    //$scope.showMoveRow = function showMoveRow() {
    //    $scope.showNewMoveRow = !$scope.showNewMoveRow;
    //}

});