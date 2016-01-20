app.factory('movementService', function ($resource, $rootScope) {

    return $resource($rootScope.APIROUTE + 'movement/:id', null,
        {
            'update': { method: 'PUT' }
        });
});