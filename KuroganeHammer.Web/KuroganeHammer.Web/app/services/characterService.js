app.factory('characterService', function ($resource, $rootScope) {

    var requestUri = $rootScope.ROUTES + 'characters/:id';

    return $resource(requestUri,
        {
            id: '@id'
        });
});