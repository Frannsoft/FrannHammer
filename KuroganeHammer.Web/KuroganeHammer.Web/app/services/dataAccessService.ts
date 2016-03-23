/// <reference path="../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../scripts/typings/angularjs/angular-resource.d.ts" />

module app.common {

    export interface IDataAccessService {
        getCharacterResource(characterId?: number): ICharacterResource;
        getMovementResource(movementId?: number): IMovementResource;
        getMoveResource(moveId?: number): IMoveResource;
        getMovementsOfName(name: string): IMovementResource;
        getMovesOfName(name: string): IMoveResource;
        getMovesOfCharacter(characterId: number): IMoveResource;
        getMovementsOfCharacter(characterId: number): IMovementResource;
    }

    export interface ICharacterResource extends ng.resource.IResourceClass<domain.ICharacter> {
        update(ICharacter): domain.ICharacter;
    }

    export interface IMovementResource extends ng.resource.IResourceClass<domain.IMovement> {
        update(IMovement): domain.IMovement;
    }

    export interface IMoveResource extends ng.resource.IResourceClass<domain.IMove> {
        update(IMove): domain.IMove;
    }

    export class DataAccessService implements IDataAccessService {

        baseUrl = "http://localhost:53410/api";
        updateAction: ng.resource.IActionDescriptor;

        static $inject = ["$resource"];
        constructor(private $resource: ng.resource.IResourceService) {
            this.updateAction = {
                method: 'PUT',
                isArray: false,
            };
        }


        getCharacterResource(characterId?: number): ICharacterResource {
            return <ICharacterResource>this.$resource(this.baseUrl + "/characters/:characterId",
                { characterId: characterId },
                { update: this.updateAction });
        }

        getMovementResource(movementId?: number): IMovementResource {
            return <IMovementResource> this.$resource(this.baseUrl + "/movement/:movementId",
                { movementId: movementId },
                { update: this.updateAction });
        }

        getMoveResource(moveId?: number): IMoveResource {
            return <IMoveResource> this.$resource(this.baseUrl + "/moves/:moveId",
                { moveId: moveId },
                { update: this.updateAction });
        }

        getMovementsOfName(name: string): IMovementResource {
            return <IMovementResource> this.$resource(this.baseUrl + "/movement/",
                { name: name },
                { update: this.updateAction });
        }

        getMovesOfName(name: string): IMoveResource {
            return <IMoveResource> this.$resource(this.baseUrl + "/moves/",
                { name: name },
                { update: this.updateAction });
        }

        getMovesOfCharacter(characterId: number): IMoveResource {
            return <IMoveResource> this.$resource(this.baseUrl + "/characters/:characterId/moves",
                { characterId: characterId },
                { update: this.updateAction });
        }

        getMovementsOfCharacter(characterId: number): IMovementResource {
            return <IMovementResource> this.$resource(this.baseUrl + "/characters/:characterId/movement",
                { characterId: characterId },
                { update: this.updateAction });
        }
    }
    angular
        .module("common.services")
        .service("dataAccessService",
        DataAccessService);
}
