app.factory('characterService', function ($resource, $rootScope) {

    var requestUri = $rootScope.APIROUTE + 'characters/:id';

    return $resource(requestUri,
        {
            id: '@id'
        });
});