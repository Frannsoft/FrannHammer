app.factory('characterMovementService', function ($resource) {

    //var requestUri = 'http://fransm4shtest.azurewebsites.net/api/characters/:id/movement';
    var requestUri = 'http://localhost:53410/api/characters/:id/movement';

    return $resource(requestUri,
        {
            id: '@id'
        });
});