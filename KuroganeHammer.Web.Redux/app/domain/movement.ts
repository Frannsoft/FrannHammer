module app.domain {
    export interface IMovement extends ng.resource.IResource<IMovement>, Movement{
        
    }

    export class Movement {

        public id: number;
        public ownerId: number;

        constructor(
            public name: string,
            public value: number) {
        }
    }
}