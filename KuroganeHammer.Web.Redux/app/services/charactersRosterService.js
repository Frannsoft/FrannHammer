app.factory('charactersRosterService', function ($resource, $rootScope) {

    var requestUri = $rootScope.APIROUTE + 'characters';

    return $resource(requestUri);
});