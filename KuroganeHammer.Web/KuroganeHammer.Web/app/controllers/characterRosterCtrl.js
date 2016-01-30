var app;
(function (app) {
    var domain;
    (function (domain) {
        var CharacterRosterCtrl = (function () {
            function CharacterRosterCtrl(dataAccessService, $location) {
                var _this = this;
                this.dataAccessService = dataAccessService;
                this.$location = $location;
                this.characters = [];
                var characterResource = dataAccessService.getCharacterResource();
                characterResource.query(function (data) {
                    _this.characters = data;
                });
            }
            CharacterRosterCtrl.prototype.goToCharacter = function (character) {
                this.$location.path('/character/' + character.id);
            };
            CharacterRosterCtrl.$inject = ["dataAccessService", "$location"];
            return CharacterRosterCtrl;
        })();
        angular
            .module("common.services")
            .controller("CharacterRosterCtrl", CharacterRosterCtrl);
    })(domain = app.domain || (app.domain = {}));
})(app || (app = {}));
//# sourceMappingURL=characterRosterCtrl.js.map