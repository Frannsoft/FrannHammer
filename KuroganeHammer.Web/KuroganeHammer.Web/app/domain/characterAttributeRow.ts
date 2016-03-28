module app.domain {

    export interface ICharacterAttributeKeyPair extends ng.resource.IResource<ICharacterAttributeKeyPair> {
        name: string;
        value: string;
    }

    export interface ICharacterAttributeRow extends ng.resource.IResource<ICharacterAttributeRow> {
        rank: string;
        attributeType: string;
        ownerId: string;
        characterName: string;
        thumbnailUrl: string;
        headers: string[];
        values: string[];
    }
}