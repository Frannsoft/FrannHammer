/// <reference path="../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../scripts/typings/angularjs/angular-resource.d.ts" />
var app;
(function (app) {
    var common;
    (function (common) {
        var DataAccessService = (function () {
            function DataAccessService($resource) {
                this.$resource = $resource;
                //baseUrl = "http://localhost/KHapi";
                this.baseUrl = "http://localhost:62072/api";
                this.updateAction = {
                    method: 'PUT',
                    isArray: false,
                };
            }
            DataAccessService.prototype.getCharacterResource = function (characterId) {
                return this.$resource(this.baseUrl + "/CharacterModels/:characterId", { characterId: characterId }, { update: this.updateAction });
            };
            DataAccessService.prototype.getMovementResource = function (movementId) {
                return this.$resource(this.baseUrl + "/movements/:movementId", { movementId: movementId }, { update: this.updateAction });
            };
            DataAccessService.prototype.getMoveResource = function (moveId) {
                return this.$resource(this.baseUrl + "/moves/:moveId", { moveId: moveId }, { update: this.updateAction });
            };
            DataAccessService.prototype.getMovementsOfName = function (name) {
                return this.$resource(this.baseUrl + "/movements/", { name: name }, { update: this.updateAction });
            };
            DataAccessService.prototype.getMovesOfName = function (name) {
                return this.$resource(this.baseUrl + "/moves/", { name: name }, { update: this.updateAction });
            };
            DataAccessService.prototype.getMovesOfCharacter = function (characterId) {
                return this.$resource(this.baseUrl + "/CharacterModels/:characterId/moves", { characterId: characterId }, { update: this.updateAction });
            };
            DataAccessService.prototype.getMovementsOfCharacter = function (characterId) {
                return this.$resource(this.baseUrl + "/CharacterModels/:characterId/movement", { characterId: characterId }, { update: this.updateAction });
            };
            DataAccessService.prototype.getAttributesOfType = function (id) {
                return this.$resource(this.baseUrl + "/CharacterAttributeModels/:id", { id: id }, { update: this.updateAction });
            };
            DataAccessService.prototype.getSmash4AttributeTypes = function (id) {
                return this.$resource(this.baseUrl + "/SmashAttributeTypes/:id", { id: id }, { update: this.updateAction });
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