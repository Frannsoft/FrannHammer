module app.domain {

    export interface IMove extends ng.resource.IResource<IMove>{

        name: string;
        id: number;
        ownerId: number;
        characterName: string;
        hitboxActive: string;
        firstActionableFrame: number;
        baseDamage: number;
        angle: number;
        baseKnockBackSetKnockback: string;
        knockbackGrowth: string;
        landingLag: string;
        autoCancel: string;
    }
}