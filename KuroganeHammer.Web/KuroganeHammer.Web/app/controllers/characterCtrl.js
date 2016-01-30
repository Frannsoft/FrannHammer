var app;
(function (app) {
    var domain;
    (function (domain) {
        var CharacterCtrl = (function () {
            function CharacterCtrl(dataAccessService, $routeParams, $location, characterName, mainImageUrl, characterDescription, characterStyle, characterId, predicate, reverse, movementData, moveData, twitterFullUrl) {
                var _this = this;
                this.dataAccessService = dataAccessService;
                this.$routeParams = $routeParams;
                this.$location = $location;
                this.characterName = characterName;
                this.mainImageUrl = mainImageUrl;
                this.characterDescription = characterDescription;
                this.characterStyle = characterStyle;
                this.characterId = characterId;
                this.predicate = predicate;
                this.reverse = reverse;
                this.movementData = movementData;
                this.moveData = moveData;
                this.twitterFullUrl = twitterFullUrl;
                this.characterId = $routeParams.characterId;
                var characterResource = dataAccessService.getCharacterById(this.characterId);
                characterResource.get(function (data) {
                    _this.characterName = data.characterName;
                    _this.mainImageUrl = data.mainImageUrl;
                    _this.characterDescription = data.characterDescription;
                    _this.characterStyle = data.characterStyle;
                });
                var movementResource = dataAccessService.getMovementsById(this.characterId);
                movementResource.query(function (data) {
                    _this.movementData = data;
                });
                var moveResource = dataAccessService.getMovesById(this.characterId);
                moveResource.query(function (data) {
                    _this.moveData = data;
                });
            }
            CharacterCtrl.prototype.setTwitterUrl = function () {
                this.twitterFullUrl = 'http://localhost:8080/%23/' + 'character/' + this.characterId;
            };
            CharacterCtrl.prototype.goToMovementRanking = function (rowItem) {
                this.$location.path('/movement').search({ name: rowItem.movementName });
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