var app;
(function (app) {
    var domain;
    (function (domain) {
        var Move = (function () {
            function Move(name, hitboxActive, firstActionableFrame, baseDamage, angle, baseKnockBackSetKnockback, knockbackGrowth, landingLag, autoCancel, id, ownerId, characterName) {
                this.name = name;
                this.hitboxActive = hitboxActive;
                this.firstActionableFrame = firstActionableFrame;
                this.baseDamage = baseDamage;
                this.angle = angle;
                this.baseKnockBackSetKnockback = baseKnockBackSetKnockback;
                this.knockbackGrowth = knockbackGrowth;
                this.landingLag = landingLag;
                this.autoCancel = autoCancel;
                this.id = id;
                this.ownerId = ownerId;
                this.characterName = characterName;
            }
            return Move;
        })();
        domain.Move = Move;
    })(domain = app.domain || (app.domain = {}));
})(app || (app = {}));
//# sourceMappingURL=move.js.map