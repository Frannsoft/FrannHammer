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
            DataAccessService.prototype.getCharacterResource = function () {
                return this.$resource("http://localhost/frannhammerAPI/characters/:characterId");
            };
            DataAccessService.$inject = ["$resource"];
            return DataAccessService;
        })();
        common.DataAccessService = DataAccessService;
        angular
            .module("KuroganeHammerApp")
            .service("dataAccessService", DataAccessService);
    })(common = app.common || (app.common = {}));
})(app || (app = {}));
//# sourceMappingURL=dataAccessService.js.map