app.factory('moveRankService', function ($resource, $rootScope) {

    var requestUri = $rootScope.ROUTES + 'movesofname';

    return $resource(requestUri,
        {
            name: '$name'
        });
});