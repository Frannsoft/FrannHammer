var app;
(function (app) {
    var domain;
    (function (domain) {
        var CharacterAttributeCtrl = (function () {
            function CharacterAttributeCtrl(dataAccessService, $location, $routeParams) {
                var _this = this;
                this.dataAccessService = dataAccessService;
                this.$location = $location;
                this.$routeParams = $routeParams;
                this.predicate = 'rank';
                this.reverse = true;
                this.attributes = [];
                this.attributeType = $routeParams.attributeType;
                var attributesResource = dataAccessService.getCharacterAttributesOfSmashAttributeType(this.attributeType);
                attributesResource.query(function (data) {
                    _this.attributes = data;
                    _this.headers = _this.attributes[0].rawHeaders;
                });
                var attributeTypesResource = dataAccessService.getSmash4AttributeTypes(this.attributeType);
                attributeTypesResource.get(function (data) {
                    _this.attributeTypeName = data.name;
                });
            }
            CharacterAttributeCtrl.prototype.order = function (predicate) {
                this.reverse = (predicate === predicate) ? !this.reverse : false;
                this.predicate = predicate;
            };
            CharacterAttributeCtrl.prototype.goToCharacter = function (rowItem) {
                this.$location.search('attributeType', null); //cleanup url
                this.$location.path('/character/' + rowItem.ownerId);
            };
            CharacterAttributeCtrl.$inject = ["dataAccessService", "$location", "$routeParams"];
            return CharacterAttributeCtrl;
        })();
        angular
            .module("common.services")
            .controller("CharacterAttributeCtrl", CharacterAttributeCtrl);
    })(domain = app.domain || (app.domain = {}));
})(app || (app = {}));
//# sourceMappingURL=characterAttributeCtrl.js.map