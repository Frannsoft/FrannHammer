app.factory('characterMovementService', function ($resource, $rootScope) {

    var requestUri = $rootScope.ROUTES + 'characters/:id/movement';

    return $resource(requestUri,
        {
            id: '@id'
        });
});