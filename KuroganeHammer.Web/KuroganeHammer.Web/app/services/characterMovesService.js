app.factory('characterMovesService', function ($resource) {

    var requestUri = 'http://fransm4shtest.azurewebsites.net/api/characters/:id/moves';

    return $resource(requestUri,
        {
            id: '@id'
        });
});