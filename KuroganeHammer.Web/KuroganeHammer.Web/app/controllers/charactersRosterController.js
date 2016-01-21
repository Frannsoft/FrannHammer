'use strict';
app.controller('charactersRosterController', function ($scope, charactersRosterService, $filter, $location) {

    $scope.characters = [];
    $scope.filteredPlacesCount = 0;

    //paging
    $scope.totalRecordsCount = 0;

    init();

    function init() {
        getCharacters();
    }

    function getCharacters() {
        charactersRosterService.query({}, function (charactersResult) {
                $scope.characters = charactersResult;
                $scope.totalRecordsCount = charactersResult.length;
        });
    };

    $scope.goToCharacter = function goToCharacter(item) {
        $location.path('/character/' + item.id);//.search({ characterId: item.id });
    }
});