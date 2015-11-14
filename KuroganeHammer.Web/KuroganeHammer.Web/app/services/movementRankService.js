app.factory('movementRankService', function ($resource, $rootScope) {

    var requestUri = $rootScope.ROUTES + 'movement';

    return $resource(requestUri,
        {
            name: '@name',
        });
});