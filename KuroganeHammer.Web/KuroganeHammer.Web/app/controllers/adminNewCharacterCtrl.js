var app;
(function (app) {
    var domain;
    (function (domain) {
        var AdminNewCharacterCtrl = (function () {
            function AdminNewCharacterCtrl(dataAccessService, $routeParams, character, movementData, moveData, showNewMovementRow, showNewMoveRow, newMovementStat, newMoveStat, tempMovementData, tempMoveData) {
                this.dataAccessService = dataAccessService;
                this.$routeParams = $routeParams;
                this.character = character;
                this.movementData = movementData;
                this.moveData = moveData;
                this.showNewMovementRow = showNewMovementRow;
                this.showNewMoveRow = showNewMoveRow;
                this.newMovementStat = newMovementStat;
                this.newMoveStat = newMoveStat;
                this.tempMovementData = tempMovementData;
                this.tempMoveData = tempMoveData;
                var charId = $routeParams.characterId;
                this.movementData = [];
                this.moveData = [];
                this.tempMovementData = [];
                this.tempMoveData = [];
            }
            AdminNewCharacterCtrl.prototype.saveCharacter = function () {
                var _this = this;
                this.moveData = this.tempMoveData;
                this.movementData = this.tempMovementData;
                //TODO: need image paths here too? - maybe just image name - 
                //path will be decided server side and then stored in db.
                var characterResource = this.dataAccessService.getCharacterResource();
                characterResource.save(this.character, function (newChar) {
                    _this.createNewMovementStats(newChar.id, _this.movementData);
                    _this.createNewMoveStats(newChar.id, _this.moveData);
                });
            };
            AdminNewCharacterCtrl.prototype.createNewMoveStats = function (ownerId, moveData) {
                var moveResource = this.dataAccessService.getMoveResource();
                moveData.forEach(function (move) {
                    move.ownerId = ownerId;
                    moveResource.save(move);
                });
                this.clearTempMoveStat();
            };
            AdminNewCharacterCtrl.prototype.createNewMovementStats = function (ownerId, movementData) {
                var movementResource = this.dataAccessService.getMovementResource();
                movementData.forEach(function (movement) {
                    movement.ownerId = ownerId;
                    movementResource.save(movement);
                });
                this.clearTempMovementStat();
            };
            AdminNewCharacterCtrl.prototype.clearTempMoveStat = function () {
                this.newMoveStat.name = '';
                this.newMoveStat.hitboxActive = '';
                this.newMoveStat.firstActionableFrame = 0;
                this.newMoveStat.baseDamage = 0;
                this.newMoveStat.angle = 0;
                this.newMoveStat.baseKnockBackSetKnockback = '';
                this.newMoveStat.knockbackGrowth = '';
                this.newMoveStat.landingLag = '';
                this.newMoveStat.autoCancel = '';
            };
            AdminNewCharacterCtrl.prototype.clearTempMovementStat = function () {
                this.newMovementStat.id = 0;
                this.newMovementStat.name = '';
                this.newMovementStat.value = 0;
            };
            AdminNewCharacterCtrl.prototype.showMoveRow = function () {
                this.showNewMoveRow = !this.showNewMoveRow;
            };
            AdminNewCharacterCtrl.prototype.showMovementRow = function () {
                this.showNewMovementRow = !this.showNewMovementRow;
            };
            AdminNewCharacterCtrl.prototype.addTempMovement = function () {
                var movement = new domain.Movement(this.newMovementStat.name, this.newMovementStat.value);
                this.tempMovementData.push(movement);
                this.clearTempMovementStat();
            };
            AdminNewCharacterCtrl.prototype.addTempMove = function () {
                var move = new domain.Move(this.newMoveStat.name, this.newMoveStat.hitboxActive, this.newMoveStat.firstActionableFrame, this.newMoveStat.baseDamage, this.newMoveStat.angle, this.newMoveStat.baseKnockBackSetKnockback, this.newMoveStat.knockbackGrowth, this.newMoveStat.landingLag, this.newMoveStat.autoCancel);
                this.tempMoveData.push(move);
                this.clearTempMoveStat();
            };
            AdminNewCharacterCtrl.$inject = ["dataAccessService", "$routeParams"];
            return AdminNewCharacterCtrl;
        })();
        angular
            .module("common.services")
            .controller("AdminNewCharacterCtrl", AdminNewCharacterCtrl);
    })(domain = app.domain || (app.domain = {}));
})(app || (app = {}));
//# sourceMappingURL=adminNewCharacterCtrl.js.map