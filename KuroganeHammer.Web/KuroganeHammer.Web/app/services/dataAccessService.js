/// <reference path="../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../scripts/typings/angularjs/angular-resource.d.ts" />
var app;
(function (app) {
    var common;
    (function (common) {
        var DataAccessService = (function () {
            function DataAccessService($resource) {
                this.$resource = $resource;
                this.updateAction = {
                    method: 'PUT',
                    isArray: false,
                };
            }
            DataAccessService.prototype.getCharacterResource = function (characterId) {
                return this.$resource("http://localhost/frannhammerAPI/characters/:characterId", { characterId: characterId }, { update: this.updateAction });
            };
            DataAccessService.prototype.getMovementResource = function (movementId) {
                return this.$resource("http://localhost/frannhammerAPI/movement/:movementId", { movementId: movementId }, { update: this.updateAction });
            };
            DataAccessService.prototype.getMoveResource = function (moveId) {
                return this.$resource("http://localhost/frannhammerAPI/moves/:moveId", { moveId: moveId }, { update: this.updateAction });
            };
            DataAccessService.prototype.getMovementsOfName = function (name) {
                return this.$resource("http://localhost/frannhammerAPI/movement/", { name: name }, { update: this.updateAction });
            };
            DataAccessService.prototype.getMovesOfName = function (name) {
                return this.$resource("http://localhost/frannhammerAPI/moves/", { name: name }, { update: this.updateAction });
            };
            DataAccessService.prototype.getMovesOfCharacter = function (characterId) {
                return this.$resource("http://localhost/frannhammerAPI/characters/:characterId/moves", { characterId: characterId }, { update: this.updateAction });
            };
            DataAccessService.prototype.getMovementsOfCharacter = function (characterId) {
                return this.$resource("http://localhost/frannhammerAPI/characters/:characterId/movement", { characterId: characterId }, { update: this.updateAction });
            };
            DataAccessService.$inject = ["$resource"];
            return DataAccessService;
        })();
        common.DataAccessService = DataAccessService;
        angular
            .module("common.services")
            .service("dataAccessService", DataAccessService);
    })(common = app.common || (app.common = {}));
})(app || (app = {}));
//# sourceMappingURL=dataAccessService.js.map