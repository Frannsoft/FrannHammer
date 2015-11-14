app.factory('characterMovesService', function ($resource, $rootScope) {

    var requestUri = $rootScope.ROUTES + 'characters/:id/moves';

    return $resource(requestUri,
        {
            id: '@id'
        });
});