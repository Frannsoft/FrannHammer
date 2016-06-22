app.factory('moveService', function ($resource, $rootScope) {

    var requestUri = $rootScope.APIROUTE + 'moves/:id';

    return $resource(requestUri,
        {
            id: '@id'
        });
});

app.factory('moveService', function ($resource, $rootScope) {

    return $resource($rootScope.APIROUTE + 'moves/:id', null,
        {
            'update': { method: 'PUT' }
        });
});