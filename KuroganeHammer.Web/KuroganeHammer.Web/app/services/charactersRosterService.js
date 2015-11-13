app.factory('charactersRosterService', function ($resource) {

    //var requestUri = 'http://fransm4shtest.azurewebsites.net/api/characters';
    var requestUri = 'http://localhost:53410/api/characters';

    return $resource(requestUri);
});