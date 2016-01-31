/// <reference path="../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../scripts/typings/angularjs/angular-resource.d.ts" />

module app.common {

    interface IDataAccessService {
        getCharacterResource(characterId?: number): app.common.ICharacterResource;
        getMovementResource(movementId?: number): app.common.IMovementResource;
        getMoveResource(moveId?: number): app.common.IMoveResource;
        getMovementsOfName(name: string): app.common.IMovementResource;
        getMovesOfName(name: string): app.common.IMoveResource;
        getMovesOfCharacter(characterId: number): app.common.IMoveResource;
        getMovementsOfCharacter(characterId: number): app.common.IMovementResource;
    }

    export interface ICharacterResource extends ng.resource.IResourceClass<app.domain.ICharacter> {
        update(ICharacter): app.domain.ICharacter;
    }

    export interface IMovementResource extends ng.resource.IResourceClass<app.domain.IMovement> {
        update(IMovement): app.domain.IMovement;
    }

    export interface IMoveResource extends ng.resource.IResourceClass<app.domain.IMove> {
        update(IMove): app.domain.IMove;
    }

    export class DataAccessService implements IDataAccessService {

        updateAction: ng.resource.IActionDescriptor;

        static $inject = ["$resource"];
        constructor(private $resource: ng.resource.IResourceService) {
            this.updateAction = {
                method: 'PUT',
                isArray: false,
            };
        }


        getCharacterResource(characterId?: number): app.common.ICharacterResource {
            return <app.common.ICharacterResource> this.$resource("http://localhost/frannhammerAPI/characters/:characterId",
                { characterId: characterId },
                { update: this.updateAction });
        }

        getMovementResource(movementId?: number): app.common.IMovementResource {
            return <app.common.IMovementResource> this.$resource("http://localhost/frannhammerAPI/movement/:movementId",
                { movementId: movementId },
                { update: this.updateAction });
        }

        getMoveResource(moveId?: number): app.common.IMoveResource {
            return <app.common.IMoveResource> this.$resource("http://localhost/frannhammerAPI/moves/:moveId",
                { moveId: moveId },
                { update: this.updateAction });
        }

        getMovementsOfName(name: string): app.common.IMovementResource {
            return <app.common.IMovementResource> this.$resource("http://localhost/frannhammerAPI/movement/",
                { name: name },
                { update: this.updateAction });
        }

        getMovesOfName(name: string): app.common.IMoveResource {
            return <app.common.IMoveResource> this.$resource("http://localhost/frannhammerAPI/moves/",
                { name: name },
                { update: this.updateAction });
        }

        getMovesOfCharacter(characterId: number): app.common.IMoveResource {
            return <app.common.IMoveResource> this.$resource("http://localhost/frannhammerAPI/characters/:characterId/moves",
                { characterId: characterId },
                { update: this.updateAction });
        }

        getMovementsOfCharacter(characterId: number): app.common.IMovementResource {
            return <app.common.IMovementResource> this.$resource("http://localhost/frannhammerAPI/characters/:characterId/movement",
                { characterId: characterId },
                { update: this.updateAction });
        }
    }
    angular
        .module("common.services")
        .service("dataAccessService",
        DataAccessService);
}
