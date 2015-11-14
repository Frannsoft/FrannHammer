app.factory('charactersRosterService', function ($resource, $rootScope) {

    var requestUri = $rootScope.ROUTES + 'characters';

    return $resource(requestUri);
});