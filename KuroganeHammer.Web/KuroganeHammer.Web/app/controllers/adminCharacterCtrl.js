var app;
(function (app) {
    var domain;
    (function (domain) {
        var AdminCharacterCtrl = (function () {
            function AdminCharacterCtrl(dataAccessService, $routeParams, character, movementData, moveData, showNewMovementRow, showNewMoveRow, newMovementStat, newMoveStat) {
                var _this = this;
                this.dataAccessService = dataAccessService;
                this.$routeParams = $routeParams;
                this.character = character;
                this.movementData = movementData;
                this.moveData = moveData;
                this.showNewMovementRow = showNewMovementRow;
                this.showNewMoveRow = showNewMoveRow;
                this.newMovementStat = newMovementStat;
                this.newMoveStat = newMoveStat;
                var charId = $routeParams.characterId;
                var characterResource = dataAccessService.getCharacterResource(charId);
                characterResource.get(function (data) {
                    _this.character = data;
                    _this.getCharacterMovementData();
                    _this.getCharacterMoveData();
                });
            }
            AdminCharacterCtrl.prototype.getCharacterMovementData = function () {
                var _this = this;
                var movementResource = this.dataAccessService.getMovementsOfCharacter(this.character.id);
                movementResource.query(function (data) {
                    _this.movementData = data;
                });
            };
            AdminCharacterCtrl.prototype.getCharacterMoveData = function () {
                var _this = this;
                var moveResource = this.dataAccessService.getMovesOfCharacter(this.character.id);
                moveResource.query(function (data) {
                    _this.moveData = data;
                });
            };
            AdminCharacterCtrl.prototype.saveCharacterChanges = function () {
                var characterResource = this.dataAccessService.getCharacterResource(this.character.id);
                characterResource.update(this.character);
            };
            AdminCharacterCtrl.prototype.saveMovementRow = function (rowItem) {
                var movementResource = this.dataAccessService.getMovementResource(this.character.id);
                movementResource.save({ id: rowItem.id }, rowItem);
            };
            AdminCharacterCtrl.prototype.saveMoveRow = function (rowItem) {
                var moveResource = this.dataAccessService.getMoveResource(this.character.id);
                moveResource.save({ id: rowItem.id }, rowItem);
            };
            AdminCharacterCtrl.prototype.createNewMovementStat = function () {
                this.newMovementStat.ownerId = this.character.id;
                var movementResource = this.dataAccessService.getMovementResource();
                movementResource.save(this.newMovementStat);
            };
            AdminCharacterCtrl.prototype.createNewMoveStat = function () {
                this.newMoveStat.ownerId = this.character.id;
                var moveResource = this.dataAccessService.getMoveResource();
                moveResource.save(this.newMoveStat);
            };
            AdminCharacterCtrl.prototype.deleteMovementRow = function (rowItem) {
                var _this = this;
                var movementResource = this.dataAccessService.getMovementResource(rowItem.id);
                movementResource.delete(function () { return _this.getCharacterMovementData(); });
            };
            AdminCharacterCtrl.prototype.deleteMoveRow = function (rowItem) {
                var _this = this;
                var moveResource = this.dataAccessService.getMoveResource(rowItem.id);
                moveResource.delete(function () { return _this.getCharacterMoveData(); });
            };
            AdminCharacterCtrl.prototype.clearMoveStat = function () {
                this.newMoveStat.name = '';
                this.newMoveStat.hitboxActive = '';
                this.newMoveStat.firstActionableFrame = 0;
                this.newMoveStat.baseDamage = 0;
                this.newMoveStat.angle = 0;
                this.newMoveStat.baseKnockBackSetKnockback = '';
                this.newMoveStat.knockbackGrowth = '';
                this.newMoveStat.landingLag = '';
                this.newMoveStat.autoCancel = '';
                this.showMoveRow();
            };
            AdminCharacterCtrl.prototype.clearMovementStat = function () {
                this.newMovementStat.name = '';
                this.newMovementStat.value = 0;
                this.showMovementRow();
            };
            AdminCharacterCtrl.prototype.showMoveRow = function () {
                this.showNewMoveRow = !this.showNewMoveRow;
            };
            AdminCharacterCtrl.prototype.showMovementRow = function () {
                this.showNewMovementRow = !this.showNewMovementRow;
            };
            AdminCharacterCtrl.$inject = ["dataAccessService", "$routeParams"];
            return AdminCharacterCtrl;
        })();
        angular
            .module("common.services")
            .controller("AdminCharacterCtrl", AdminCharacterCtrl);
    })(domain = app.domain || (app.domain = {}));
})(app || (app = {}));
//# sourceMappingURL=adminCharacterCtrl.js.map