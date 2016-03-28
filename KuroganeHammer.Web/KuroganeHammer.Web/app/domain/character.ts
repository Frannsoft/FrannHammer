module app.domain {
    export interface ICharacter extends ng.resource.IResource<ICharacter>{
        metaData: domain.ICharacterMetadata;
        movementData: domain.IMovement[];
        moveData: domain.IMove[];
    }
}