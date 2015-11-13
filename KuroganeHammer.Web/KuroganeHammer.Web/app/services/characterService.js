app.factory('characterService', function ($resource) {

    //var requestUri = 'http://fransm4shtest.azurewebsites.net/api/characters/:id';
    var requestUri = 'http://localhost:53410/api/characters/:id';

    return $resource(requestUri,
        {
            id: '@id'
        });
});