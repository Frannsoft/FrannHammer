module app.domain {

    interface IMoveModel {
        moveName: string;
        predicate: string;
        reverse: boolean;
        moveData: IMove[];
        order(predicate: string): void;
        goToCharacter(rowItem: IMove): void;
    }

    interface IMoveParams extends ng.route.IRouteParamsService {
        name: string;
    }

    class MoveRankCtrl implements IMoveModel {

        moveName: string;
        predicate: string;
        reverse: boolean;
        moveData: app.domain.IMove[];

        static $inject = ["dataAccessService", "$location", "$routeParams"];
        constructor(private dataAccessService: app.common.DataAccessService,
            private $location: ng.ILocationService,
            private $routeParams: IMoveParams) {

            this.reverse = true;
            this.moveData = [];
            this.moveName = $routeParams.name;

            var moveResource = dataAccessService.getMovesOfName(this.moveName);
            moveResource.query((data: app.domain.IMove[]) => {
                this.moveData = data;
            });
        }

        order(predicate: string): void {
            this.reverse = (this.predicate === predicate) ? !this.reverse : false;
            this.predicate = predicate;
        }

        goToCharacter(rowItem: IMove): void {
            this.$location.search('name', null); //cleanup url
            this.$location.path('/character/' + rowItem.ownerId);
        }
    }

    angular
        .module("common.services")
        .controller("MoveRankCtrl",
        MoveRankCtrl);
}