module app.domain {
    export interface ICharacter {
        id: number;
        characterName: string;
        mainImageUrl: string;
        characterDescription: string;
        ownerId: number;
        characterStyle: string;
        movementData: any[];
        moveData: any[];
    }
}