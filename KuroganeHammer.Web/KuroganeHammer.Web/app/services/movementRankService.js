app.factory('movementRankService', function ($resource, $rootScope) {

    var requestUri = $rootScope.APIROUTE + 'movement';

    return $resource(requestUri,
        {
            name: '@name',
        });
});