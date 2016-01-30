 /// <reference path="../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../scripts/typings/angularjs/angular-resource.d.ts" />

module app.common {
    interface IDataAccessService {
        getCharacterResource(): ng.resource.IResourceClass<ICharacterResource>;
        getMovementsOfName(name: string): ng.resource.IResourceClass<IMovementResource>;
        getMovesOfName(name: string): ng.resource.IResourceClass<IMoveResource>;
        getCharacterById(id: number): ng.resource.IResourceClass<ICharacterResource>;
        getMovesById(id: number): ng.resource.IResourceClass<IMoveResource>;
        getMovementsById(id: number): ng.resource.IResourceClass<IMovementResource>;
    }

    interface ICharacterResource extends ng.resource.IResource<app.domain.ICharacter> {

    }

    interface IMovementResource extends ng.resource.IResource<app.domain.IMovement> {

    }

    interface IMoveResource extends ng.resource.IResource<app.domain.IMove> {

    }

    export class DataAccessService implements IDataAccessService {

        static $inject = ["$resource"];
        constructor(private $resource: ng.resource.IResourceService) {

        }

        getCharacterById(characterId: number): ng.resource.IResourceClass<ICharacterResource> {
            return this.$resource("http://localhost/frannhammerAPI/characters/:characterId",
                {
                    characterId: characterId
                });
        }

        getCharacterResource(): ng.resource.IResourceClass<ICharacterResource> {
            return this.$resource("http://localhost/frannhammerAPI/characters/:characterId");
        }

        getMovementsOfName(name: string): ng.resource.IResourceClass<IMovementResource> {
            return this.$resource("http://localhost/frannhammerAPI/movement/", { name: name });
        }

        getMovesOfName(name: string): ng.resource.IResourceClass<IMoveResource> {
            return this.$resource("http://localhost/frannhammerAPI/moves/", { name: name });
        }

        getMovesById(id: number): ng.resource.IResourceClass<IMoveResource> {
            return this.$resource("http://localhost/frannhammerAPI/characters/:id/moves", { id: id }); 
        }

        getMovementsById(id: number): ng.resource.IResourceClass<IMovementResource> {
            return this.$resource("http://localhost/frannHammerAPI/characters/:id/movement", { id: id });
        }
    }
    angular
        .module("common.services")
        .service("dataAccessService",
        DataAccessService);
}
