﻿module app.domain {
    export interface ICharacterAttribute extends ng.resource.IResource<ICharacterAttribute> {
        rank: string;
        ownerId: string;
        name: string;
        value: string;
        attributeType: string;    
        thumbnailUrl: string;
        characterName: string;
    }
}