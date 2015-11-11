var app = angular.module('KuroganeHammerApp', ['ngRoute', 'ngResource', 'ui.bootstrap']);
app.config(function ($routeProvider) {

    $routeProvider.when("/", {
        controller: "charactersRosterController",
        templateUrl: "/app/views/charactersRoster.html"
    });
    
    $routeProvider.otherwise({ redirectTo: "/index" });
});