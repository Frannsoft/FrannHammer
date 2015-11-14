var app = angular.module('KuroganeHammerApp', ['ngRoute', 'ngResource', 'ui.bootstrap', 'ngAnimate'])
.run(function ($rootScope) {
    $rootScope.ROUTES = 'http://fransm4shtest.azurewebsites.net/api/'
    //$rootScope.ROUTES = 'http://localhost:53410/api/'
});

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

    $routeProvider.when('/moveRanking', {
        controller: "moveRankController",
        templateUrl: "/app/views/moveRanks.html"
    });

    $routeProvider.when("/contactus", {
        controller: "",
        templateUrl: "app/views/contactus.html"
    });

    $routeProvider.when("/aboutus", {
        controller: "",
        templateUrl: "app/views/aboutus.html"
    });

    $routeProvider.when("/glossary", {
        controller: "",
        templateUrl: "app/views/glossary.html"
    });
    
    $routeProvider.otherwise({ redirectTo: "/" });
});