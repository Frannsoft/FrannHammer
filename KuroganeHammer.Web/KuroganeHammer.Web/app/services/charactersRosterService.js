app.factory('charactersRosterService', function ($resource) {

    var requestUri = 'http://localhost:53410/api/characters';

    return $resource(requestUri);
});