module app.domain {

    interface ICharacterModel {

        characterId: number;
        characterName: string;
        mainImageUrl: string;
        characterDescription: string;
        characterStyle: string;
        predicate: string;
        reverse: boolean;
        movementData: app.domain.IMovement[];
        moveData: app.domain.IMove[];
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
        constructor(public dataAccessService: app.common.DataAccessService,
            private $routeParams: ICharacterParams,
            public $location: ng.ILocationService,
            public characterName: string,
            public mainImageUrl: string,
            public characterDescription: string,
            public characterStyle: string,
            public characterId: number,
            public predicate: string,
            public reverse: boolean,
            public movementData: app.domain.IMovement[],
            public moveData: app.domain.IMove[],
            public twitterFullUrl: string) {
            
            this.characterId = $routeParams.characterId;

            var characterResource = dataAccessService.getCharacterResource(this.characterId);
            characterResource.get((data: app.domain.ICharacter) => {
                this.characterName = data.metaData.name;
                this.mainImageUrl = data.metaData.mainImageUrl;
                this.characterDescription = data.metaData.description;
                this.characterStyle = data.metaData.style;
            });

            var movementResource = dataAccessService.getMovementResource(this.characterId);
            movementResource.query((data: app.domain.IMovement[]) => {
                this.movementData = data;
            });

            var moveResource = dataAccessService.getMoveResource(this.characterId);
            moveResource.query((data: app.domain.IMove[]) => {
                this.moveData = data;
            });
        }

        setTwitterUrl(): void {
            this.twitterFullUrl = 'http://localhost:8080/%23/' + 'character/' + this.characterId;
        }

        goToMovementRanking(rowItem: IMovement): void {
            this.$location.path('/movement').search({ name: rowItem.name });
        }

        goToMoveRanking(rowItem: IMove): void {
            this.$location.path('/moveRanking').search({ name: rowItem.name });
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