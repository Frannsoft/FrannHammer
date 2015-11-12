app.factory('charactersRosterService', function ($resource) {

    var requestUri = 'http://fransm4shtest.azurewebsites.net/api/characters';

    return $resource(requestUri);
});