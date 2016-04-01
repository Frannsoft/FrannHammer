var app;
(function (app) {
    var domain;
    (function (domain) {
        var CharacterAttributeRosterCtrl = (function () {
            function CharacterAttributeRosterCtrl(dataAccessService, $location) {
                var _this = this;
                this.dataAccessService = dataAccessService;
                this.$location = $location;
                this.attributeTypes = [];
                var attributeTypesResource = dataAccessService.getSmash4AttributeTypes();
                attributeTypesResource.query(function (data) {
                    _this.attributeTypes = data;
                });
            }
            CharacterAttributeRosterCtrl.prototype.goToAttribute = function (id) {
                //this.$location.search('attributeType', attributeType);
                this.$location.path('/attributes/' + id + "/ranking");
            };
            CharacterAttributeRosterCtrl.$inject = ["dataAccessService", "$location"];
            return CharacterAttributeRosterCtrl;
        })();
        angular
            .module("common.services")
            .controller("CharacterAttributeRosterCtrl", CharacterAttributeRosterCtrl);
    })(domain = app.domain || (app.domain = {}));
})(app || (app = {}));
//# sourceMappingURL=characterAttributeRosterCtrl.js.map