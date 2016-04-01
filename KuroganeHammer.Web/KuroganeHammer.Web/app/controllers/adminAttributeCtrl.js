var app;
(function (app) {
    var domain;
    (function (domain) {
        var AdminAttributeCtrl = (function () {
            function AdminAttributeCtrl(dataAccessService, $location, $routeParams) {
                var _this = this;
                this.dataAccessService = dataAccessService;
                this.$location = $location;
                this.$routeParams = $routeParams;
                this.attributes = [];
                this.attributeType = $routeParams.attributeType;
                var attributesResource = dataAccessService.getAttributesOfType(this.attributeType);
                attributesResource.query(function (data) {
                    _this.attributes = data;
                    _this.headers = _this.attributes[0].rawHeaders;
                });
            }
            AdminAttributeCtrl.prototype.saveAttribute = function (attribute) {
                var attributesResource = this.dataAccessService.getAttributesOfType(this.attributeType);
                attributesResource.save({ id: attribute.id }, attribute);
            };
            AdminAttributeCtrl.$inject = ["dataAccessService", "$location", "$routeParams"];
            return AdminAttributeCtrl;
        })();
        angular
            .module("common.services")
            .controller("AdminAttributeCtrl", AdminAttributeCtrl);
    })(domain = app.domain || (app.domain = {}));
})(app || (app = {}));
//# sourceMappingURL=adminAttributeCtrl.js.map