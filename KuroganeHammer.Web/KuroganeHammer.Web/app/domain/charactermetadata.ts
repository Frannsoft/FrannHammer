module app.domain {

    export interface ICharacterMetadata extends ng.resource.IResource<ICharacterMetadata> {

        id: number;
        name: string;
        mainImageUrl: string;
        thumbnailUrl: string;
        colortheme: string;
        description: string;
        ownerId: number;
        style: string;
    }
}