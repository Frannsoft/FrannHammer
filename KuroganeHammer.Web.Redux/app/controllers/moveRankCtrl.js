var app;
(function (app) {
    var domain;
    (function (domain) {
        var MoveRankCtrl = (function () {
            function MoveRankCtrl(dataAccessService, $location, $routeParams) {
                var _this = this;
                this.dataAccessService = dataAccessService;
                this.$location = $location;
                this.$routeParams = $routeParams;
                this.reverse = true;
                this.moveData = [];
                this.moveName = $routeParams.name;
                var moveResource = dataAccessService.getMovesOfName(this.moveName);
                moveResource.query(function (data) {
                    _this.moveData = data;
                });
            }
            MoveRankCtrl.prototype.order = function (predicate) {
                this.reverse = (this.predicate === predicate) ? !this.reverse : false;
                this.predicate = predicate;
            };
            MoveRankCtrl.prototype.goToCharacter = function (rowItem) {
                this.$location.search('name', null); //cleanup url
                this.$location.path('/character/' + rowItem.ownerId);
            };
            MoveRankCtrl.$inject = ["dataAccessService", "$location", "$routeParams"];
            return MoveRankCtrl;
        })();
        angular
            .module("common.services")
            .controller("MoveRankCtrl", MoveRankCtrl);
    })(domain = app.domain || (app.domain = {}));
})(app || (app = {}));
//# sourceMappingURL=moveRankCtrl.js.map