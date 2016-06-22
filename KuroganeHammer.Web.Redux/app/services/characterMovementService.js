app.factory('characterMovementService', function ($resource, $rootScope) {

    var requestUri = $rootScope.APIROUTE + 'characters/:id/movement';

    return $resource(requestUri,
        {
            id: '@id'
        });
});