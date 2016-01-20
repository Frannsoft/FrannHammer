app.factory('characterService', function ($resource, $rootScope) {

    var requestUri = $rootScope.APIROUTE + 'characters/:id';

    return $resource(requestUri,
        {
            id: '@id'
        });
});

app.factory('characterService', function ($resource, $rootScope) {

    return $resource($rootScope.APIROUTE + 'characters/:id', null,
        {
            'update': { method: 'PUT' }
        });
});