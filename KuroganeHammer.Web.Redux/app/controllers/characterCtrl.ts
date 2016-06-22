module app.domain {

    interface ICharacterModel {

        character: domain.ICharacterMetadata;
        predicate: string;
        reverse: boolean;
        movementData: domain.IMovement[];
        moveData: domain.IMove[];
        twitterFullUrl: string;
        setTwitterUrl(): void;
        goToMovementRanking(rowItem: IMovement): void;
        goToMoveRanking(rowItem: IMove): void;
        order(predicate: string): void;
    }

    interface ICharacterParams extends ng.route.IRouteParamsService {
        characterId: number;
    }

    class CharacterCtrl implements ICharacterModel {

        static $inject = ["dataAccessService", "$routeParams", "$location"];
        constructor(public dataAccessService: common.DataAccessService,
            private $routeParams: ICharacterParams, 
            public $location: ng.ILocationService,
            public character: ICharacterMetadata,
            public predicate: string,
            public reverse: boolean,
            public movementData: domain.IMovement[],
            public moveData: domain.IMove[],
            public twitterFullUrl: string) {
            
            var characterResource = dataAccessService.getCharacterResource($routeParams.characterId);
            characterResource.get((data: domain.ICharacterMetadata) => {
                this.character = data;
                this.getMovementData(this.character.id);
                this.getMoveData(this.character.id);
            });
            
        }

        getMovementData(id: number): void {
            var movementResource = this.dataAccessService.getMovementsOfCharacter(id);
            movementResource.query((data: domain.IMovement[]) => {
                this.movementData = data;
            });
        }

        getMoveData(id: number): void {
            var moveResource = this.dataAccessService.getMovesOfCharacter(id);
            moveResource.query((data: domain.IMove[]) => {
                this.moveData = data;
            });
        }

        setTwitterUrl(): void {
            this.twitterFullUrl = 'http://localhost:8080/%23/' + 'character/' + this.character.id;
        }

        goToMovementRanking(rowItem: IMovement): void {
            this.$location.path('/movement').search({ name: rowItem.name });
        }

        goToMoveRanking(rowItem: IMove): void {
            this.$location.path('/moveranking').search({ name: rowItem.name });
        }

        order(predicate: string): void {
            this.reverse = (this.predicate === predicate) ? !this.reverse : false;
            this.predicate = predicate;
        }
    }

    angular
        .module("common.services")
        .controller("CharacterCtrl",
        CharacterCtrl);
}