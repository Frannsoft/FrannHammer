'use strict';
app.controller('charactersRosterController', function ($scope, charactersRosterService, $location) {

    $scope.characters = [];
    init();

    function init() {
        getCharacters();
    }

    function getCharacters() {
        charactersRosterService.query({}, function (charactersResult) {
                $scope.characters = charactersResult;
        });
    };

    $scope.goToCharacter = function goToCharacter(item) {
        $location.path('/character/' + item.id);
    }
});