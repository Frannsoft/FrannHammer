module app.domain {

    export interface ISmashAttributeType extends ng.resource.IResource<ISmashAttributeType> {
        id: number;
        name: string;
    }
}