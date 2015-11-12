app.factory('movementRankService', function ($resource) {

    //var requestUri = 'http://fransm4shtest.azurewebsites.net/api/movement/:name';
    var requestUri = 'http://localhost:53410/api/movement/:name';

    return $resource(requestUri,
        {
            name: '@name',
            orderBy: '@orderBy'
        });
});