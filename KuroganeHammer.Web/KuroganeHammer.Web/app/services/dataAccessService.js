/// <reference path="../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../scripts/typings/angularjs/angular-resource.d.ts" />
var app;
(function (app) {
    var common;
    (function (common) {
        var DataAccessService = (function () {
            function DataAccessService($resource) {
                this.$resource = $resource;
            }
            DataAccessService.prototype.getCharacterById = function (characterId) {
                return this.$resource("http://localhost/frannhammerAPI/characters/:characterId", {
                    characterId: characterId
                });
            };
            DataAccessService.prototype.getCharacterResource = function () {
                return this.$resource("http://localhost/frannhammerAPI/characters/:characterId");
            };
            DataAccessService.prototype.getMovementsOfName = function (name) {
                return this.$resource("http://localhost/frannhammerAPI/movement/", { name: name });
            };
            DataAccessService.prototype.getMovesOfName = function (name) {
                return this.$resource("http://localhost/frannhammerAPI/moves/", { name: name });
            };
            DataAccessService.prototype.getMovesById = function (id) {
                return this.$resource("http://localhost/frannhammerAPI/characters/:id/moves", { id: id });
            };
            DataAccessService.prototype.getMovementsById = function (id) {
                return this.$resource("http://localhost/frannHammerAPI/characters/:id/movement", { id: id });
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