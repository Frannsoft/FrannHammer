module app.domain {

    interface IMovementModel {

        name: string;
        movementData: app.domain.IMovement[];
        predicate: string;
        reverse: boolean;
        order(predicate: string): void;
        goToCharacter(rowItem: ICharacter): void;
    }

    interface IMovementParams extends ng.route.IRouteParamsService {
        name: string;
    }

    class MovementRankCtrl implements IMovementModel {

        name: string;
        movementData: app.domain.IMovement[];
        predicate: string = 'value';
        reverse: boolean = true;

        static $inject = ["dataAccessService", "$location", "$routeParams"];
        constructor(private dataAccessService: app.common.DataAccessService,
            private $location: ng.ILocationService,
            private $routeParams: IMovementParams) {

            this.movementData = [];
            this.name = $routeParams.name;

            var movementResource = dataAccessService.getMovementsOfName(this.name);
            movementResource.query((data: app.domain.IMovement[]) => {
                this.movementData = data;
            });
        }

        order(predicate: string): void {
            this.reverse = (predicate === predicate) ? !this.reverse : false;
            this.predicate = predicate;
        }

        goToCharacter(rowItem: ICharacter): void {
            this.$location.search('name', null); //cleanup url
            this.$location.path('/character/' + rowItem.ownerId);
        }
    }

    angular
        .module("common.services")
        .controller("MovementRankCtrl",
        MovementRankCtrl);

}