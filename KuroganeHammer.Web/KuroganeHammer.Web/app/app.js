var app = angular.module('KuroganeHammerApp', ['ngRoute', 'ngResource', 'ui.bootstrap']);
app.config(function ($routeProvider) {

    $routeProvider.when("/", {
        controller: "charactersRosterController",
        templateUrl: "/app/views/charactersRoster.html"
    });

    $routeProvider.when("/character/:characterId", {
        controller: "characterController",
        templateUrl: "/app/views/character.html"
    });

    $routeProvider.when("/movement", {
        controller: "movementRankController",
        templateUrl: "/app/views/movementRanks.html"
    });
    
    $routeProvider.otherwise({ redirectTo: "/" });
});