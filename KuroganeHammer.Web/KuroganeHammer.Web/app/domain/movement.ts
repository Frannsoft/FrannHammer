module app.domain {
    export interface IMovement extends ng.resource.IResource<IMovement>{
        ownerId: number;
        id: number;
        name: string;
        value: number;
    }
}