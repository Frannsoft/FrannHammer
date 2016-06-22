app.factory('characterMovesService', function ($resource, $rootScope) {

    var requestUri = $rootScope.APIROUTE + 'characters/:id/moves';

    return $resource(requestUri,
        {
            id: '@id'
        });
});