var app;
(function (app) {
    var domain;
    (function (domain) {
        var CharacterCtrl = (function () {
            function CharacterCtrl(dataAccessService, $routeParams, $location, character, predicate, reverse, movementData, moveData, twitterFullUrl) {
                var _this = this;
                this.dataAccessService = dataAccessService;
                this.$routeParams = $routeParams;
                this.$location = $location;
                this.character = character;
                this.predicate = predicate;
                this.reverse = reverse;
                this.movementData = movementData;
                this.moveData = moveData;
                this.twitterFullUrl = twitterFullUrl;
                var characterResource = dataAccessService.getCharacterResource($routeParams.characterId);
                characterResource.get(function (data) {
                    _this.character = data;
                    _this.getMovementData(_this.character.id);
                    _this.getMoveData(_this.character.id);
                });
            }
            CharacterCtrl.prototype.getMovementData = function (id) {
                var _this = this;
                var movementResource = this.dataAccessService.getMovementsOfCharacter(id);
                movementResource.query(function (data) {
                    _this.movementData = data;
                });
            };
            CharacterCtrl.prototype.getMoveData = function (id) {
                var _this = this;
                var moveResource = this.dataAccessService.getMovesOfCharacter(id);
                moveResource.query(function (data) {
                    _this.moveData = data;
                });
            };
            CharacterCtrl.prototype.setTwitterUrl = function () {
                this.twitterFullUrl = 'http://localhost:8080/%23/' + 'character/' + this.character.id;
            };
            CharacterCtrl.prototype.goToMovementRanking = function (rowItem) {
                this.$location.path('/movement').search({ name: rowItem.name });
            };
            CharacterCtrl.prototype.goToMoveRanking = function (rowItem) {
                this.$location.path('/moveRanking').search({ name: rowItem.name });
            };
            CharacterCtrl.prototype.order = function (predicate) {
                this.reverse = (this.predicate === predicate) ? !this.reverse : false;
                this.predicate = predicate;
            };
            CharacterCtrl.$inject = ["dataAccessService", "$routeParams", "$location"];
            return CharacterCtrl;
        })();
        angular
            .module("common.services")
            .controller("CharacterCtrl", CharacterCtrl);
    })(domain = app.domain || (app.domain = {}));
})(app || (app = {}));
//# sourceMappingURL=characterCtrl.js.map