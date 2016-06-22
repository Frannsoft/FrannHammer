var app;
(function (app) {
    var domain;
    (function (domain) {
        var CharacterRosterCtrl = (function () {
            function CharacterRosterCtrl(dataAccessService) {
                var _this = this;
                this.dataAccessService = dataAccessService;
                this.characters = [];
                var characterResource = dataAccessService.getCharacterResource();
                characterResource.query(function (data) {
                    _this.characters = data;
                });
            }
            CharacterRosterCtrl.$inject = ["dataAccessService"];
            return CharacterRosterCtrl;
        })();
        angular
            .module("KuroganeHammerApp")
            .controller("CharacterRosterCtrl", CharacterRosterCtrl);
    })(domain = app.domain || (app.domain = {}));
})(app || (app = {}));
//# sourceMappingURL=charactersRosterCtrl.js.map