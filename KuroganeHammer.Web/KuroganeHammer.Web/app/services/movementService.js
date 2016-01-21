app.factory('movementService', function ($resource, $rootScope) {

    var requestUri = $rootScope.APIROUTE + 'movement/:id';

    return $resource(requestUri,
        {
            id: '@id'
        });
});

app.factory('movementService', function ($resource, $rootScope) {

    return $resource($rootScope.APIROUTE + 'movement/:id', null,
        {
            'update': { method: 'PUT' }
        });
});