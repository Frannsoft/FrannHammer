module app.domain {
    export interface ICharacter extends ng.resource.IResource<ICharacter>{
        
        metaData: app.domain.ICharacterMetadata;
        movementData: app.domain.IMovement[];
        moveData: app.domain.IMove[];
    }
}