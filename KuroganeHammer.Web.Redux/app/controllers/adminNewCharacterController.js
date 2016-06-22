'use strict';
app.controller('adminNewCharacterController', function ($scope, $rootScope, characterService,
    characterMovementService, characterMovesService, movementService, moveService,
    $routeParams,
    $resource) {

    $scope.characterName;
    $scope.mainImageUrl;
    $scope.characterDescription;
    $scope.characterStyle;
    $scope.movementData = [];
    $scope.moveData = [];
    $scope.tileImageUrl;

    $scope.showNewMovementRow = false;
    $scope.showNewMoveRow = false;

    $scope.newMovementStat;
    $scope.newMoveStat;
    $scope.tempMovementData = [];
    $scope.tempMoveData = [];

    //save character methods
    $scope.saveCharacter = function saveNewCharacter() {
        $scope.moveData = $scope.tempMoveData;
        $scope.movementData = $scope.tempMovementData;

        //TODO: need image paths here too? - maybe just image name - 
        //path will be decided server side and then stored in db.

        characterService.save({
            name: $scope.characterName,
            style: $scope.characterStyle,
            description: $scope.characterDescription,
            mainImageUrl: $scope.mainImageUrl,
            thumbnailUrl: $scope.tileImageUrl
        }, function (newChar) {

            //using the new char id save the moves and movement data
            $scope.createNewMovementStat(newChar);
            $scope.createNewMoveStat(newChar);
        });
    }


    $scope.createNewMovementStat = function createNewMovementStat(item) {
        $scope.tempMovementData.forEach(function (data) {
            movementService.save({
                name: data.name, value: data.value,
                ownerId: item.id
            });
        });
        clearTempMovementStat();
    }

    $scope.createNewMoveStat = function createNewMoveStat(item) {

        $scope.tempMoveData.forEach(function (data) {
            moveService.save({
                name: data.name,
                hitboxActive: data.hitboxActive,
                firstActionableFrame: data.firstActionableFrame,
                baseDamage: data.baseDamage,
                angle: data.angle,
                baseKnockBackSetKnockback: data.baseKnockBackSetKnockback,
                knockbackGrowth: data.knockbackGrowth,
                landingLag: data.landingLag,
                autoCancel: data.autoCancel,
                ownerId: item.id
            });
        });

        clearTempMoveStat();
    }
    //end save character methods

    //temp storage methods for new character data
    $scope.addTempMovement = function addTempMovement() {
        $scope.tempMovementData.push({
            name: $scope.newMovementStat.name,
            value: $scope.newMovementStat.value
        });

        $scope.newMovementStat.name = '';
        $scope.newMovementStat.value = '';

    }

    $scope.addTempMove = function addTempMove() {
        $scope.tempMoveData.push({
            name: $scope.newMoveStat.name,
            hitboxActive: $scope.newMoveStat.hitboxActive,
            firstActionableFrame: $scope.newMoveStat.firstActionableFrame,
            baseDamage: $scope.newMoveStat.baseDamage,
            angle: $scope.newMoveStat.angle,
            baseKnockBackSetKnockback: $scope.newMoveStat.baseKnockBackSetKnockback,
            knockbackGrowth: $scope.newMoveStat.knockbackGrowth,
            landingLag: $scope.newMoveStat.landingLag,
            autoCancel: $scope.newMoveStat.autoCancel,
        });

        clearTempMoveStat();
    }

    function clearTempMovementStat() {
        $scope.newMovementStat.name = '';
        $scope.newMovementStat.value = '';
    }

    function clearTempMoveStat() {
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

    $scope.showMovementRow = function showMovementRow() {
        $scope.showNewMovementRow = !$scope.showNewMovementRow;
    };

    $scope.showMoveRow = function showMoveRow() {
        $scope.showNewMoveRow = !$scope.showNewMoveRow;
    }

});