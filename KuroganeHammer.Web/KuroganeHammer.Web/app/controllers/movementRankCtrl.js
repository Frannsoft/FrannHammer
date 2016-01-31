var app;
(function (app) {
    var domain;
    (function (domain) {
        var MovementRankCtrl = (function () {
            function MovementRankCtrl(dataAccessService, $location, $routeParams) {
                var _this = this;
                this.dataAccessService = dataAccessService;
                this.$location = $location;
                this.$routeParams = $routeParams;
                this.predicate = 'value';
                this.reverse = true;
                this.movementData = [];
                this.name = $routeParams.name;
                var movementResource = dataAccessService.getMovementsOfName(this.name);
                movementResource.query(function (data) {
                    _this.movementData = data;
                });
            }
            MovementRankCtrl.prototype.order = function (predicate) {
                this.reverse = (predicate === predicate) ? !this.reverse : false;
                this.predicate = predicate;
            };
            MovementRankCtrl.prototype.goToCharacter = function (rowItem) {
                this.$location.search('name', null); //cleanup url
                this.$location.path('/character/' + rowItem.metaData.ownerId);
            };
            MovementRankCtrl.$inject = ["dataAccessService", "$location", "$routeParams"];
            return MovementRankCtrl;
        })();
        angular
            .module("common.services")
            .controller("MovementRankCtrl", MovementRankCtrl);
    })(domain = app.domain || (app.domain = {}));
})(app || (app = {}));
//# sourceMappingURL=movementRankCtrl.js.map