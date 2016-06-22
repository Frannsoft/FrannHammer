app.factory('moveRankService', function ($resource, $rootScope) {

    var requestUri = $rootScope.APIROUTE + 'movesofname';

    return $resource(requestUri,
        {
            name: '$name'
        });
});