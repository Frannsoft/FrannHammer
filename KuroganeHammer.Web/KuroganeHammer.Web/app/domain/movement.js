var app;
(function (app) {
    var domain;
    (function (domain) {
        var Movement = (function () {
            function Movement(name, value) {
                this.name = name;
                this.value = value;
            }
            return Movement;
        })();
        domain.Movement = Movement;
    })(domain = app.domain || (app.domain = {}));
})(app || (app = {}));
//# sourceMappingURL=movement.js.map