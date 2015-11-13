app.factory('moveRankService', function ($resource) {
    //var requestUri = 'http://fransm4shtest.azurewebsites.net/api/moves/:name;
    var requestUri = 'http://localhost:53410/api/moves/:name';

    return $resource(requestUri,
        {
            name: '$name',
            orderBy: '@orderBy'
        });
});