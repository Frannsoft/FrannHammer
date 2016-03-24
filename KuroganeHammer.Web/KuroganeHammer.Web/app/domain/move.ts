module app.domain {

    export interface IMove extends ng.resource.IResource<IMove>, Move {

    }

    export class Move {

        constructor(
            public name: string,
            public hitboxActive: string,
            public firstActionableFrame: number,
            public baseDamage: number,
            public angle: number,
            public baseKnockBackSetKnockback: string,
            public knockbackGrowth: string,
            public type: string,
            public landingLag: string,
            public autoCancel: string,
            public id?: number,
            public ownerId?: number,
            public characterName?: string) {
        }
    }
}