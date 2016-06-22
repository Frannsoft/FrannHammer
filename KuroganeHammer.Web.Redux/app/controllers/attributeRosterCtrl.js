var app;
(function (app) {
    var domain;
    (function (domain) {
        var CharacterAttributeRosterCtrl = (function () {
            function CharacterAttributeRosterCtrl(dataAccessService, $location) {
                this.dataAccessService = dataAccessService;
                this.$location = $location;
            }
            CharacterAttributeRosterCtrl.prototype.goToAttribute = function (attributeType) {
                this.$location.search('attributeType', attributeType);
                this.$location.path('/attributeranking/' + attributeType);
            };
            CharacterAttributeRosterCtrl.$inject = ["dataAccessService", "$location"];
            return CharacterAttributeRosterCtrl;
        })();
        angular
            .module("common.services")
            .controller("CharacterAttributeRosterCtrl", CharacterAttributeRosterCtrl);
    })(domain = app.domain || (app.domain = {}));
})(app || (app = {}));
//# sourceMappingURL=attributeRosterCtrl.js.map