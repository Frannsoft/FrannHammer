module app.domain {

    interface IAdminCharacterModel {

        movementData: domain.IMovement[];
        moveData: domain.IMove[];
        character: domain.ICharacterMetadata;
        showNewMovementRow: boolean;
        showNewMoveRow: boolean;
        newMovementStat: domain.IMovement;
        newMoveStat: domain.IMove;
        saveCharacterDetailsChanges(): void;
        saveMovementRow(rowItem: IMovement): void;
        saveMoveRow(rowItem: IMove): void;
        createNewMovementStat(): void;
        createNewMoveStat(): void;
        deleteMovementRow(rowItem: IMovement): void;
        deleteMoveRow(rowItem: IMove): void;
        clearMoveStat(): void;
        clearMovementStat(): void;
        showMovementRow(): void;
        showMoveRow(): void;
    }

    export interface IAdminCharacterParams extends ng.route.IRouteParamsService {
        characterId: number;
    }

    class AdminCharacterCtrl implements IAdminCharacterModel {

        static $inject = ["dataAccessService", "$routeParams"];
        constructor(public dataAccessService: common.DataAccessService,
            private $routeParams: IAdminCharacterParams,
            public character: domain.ICharacterMetadata,
            public movementData: domain.IMovement[],
            public moveData: domain.IMove[],
            public showNewMovementRow: boolean,
            public showNewMoveRow: boolean,
            public newMovementStat: domain.IMovement,
            public newMoveStat: domain.IMove) {

            var charId = $routeParams.characterId;

            var characterResource = dataAccessService.getCharacterResource(charId);
            characterResource.get((data: domain.ICharacterMetadata) => {
                this.character = data;
                this.getCharacterMovementData();
                this.getCharacterMoveData();
            });
        }

        getCharacterMovementData(): void {
            var movementResource = this.dataAccessService.getMovementsOfCharacter(this.character.ownerId);
            movementResource.query((data: domain.IMovement[]) => {
                this.movementData = data;
            });
        }

        getCharacterMoveData(): void {
            var moveResource = this.dataAccessService.getMovesOfCharacter(this.character.ownerId);
            moveResource.query((data: domain.IMove[]) => {
                this.moveData = data;
            });
        }

        saveCharacterDetailsChanges(): void {
            var characterResource = this.dataAccessService.getCharacterResource(this.character.id);
            characterResource.update(this.character);
        }

        saveMovementRow(rowItem: domain.IMovement): void {
            var movementResource = this.dataAccessService.getMovementResource(this.character.id);
            movementResource.save({ id: rowItem.id }, rowItem);
        }

        saveMoveRow(rowItem: domain.IMove): void {
            var moveResource = this.dataAccessService.getMoveResource(this.character.id);
            moveResource.save({ id: rowItem.id }, rowItem);
        }

        createNewMovementStat(): void {
            this.newMovementStat.ownerId = this.character.ownerId;
            var movementResource = this.dataAccessService.getMovementResource();
            movementResource.save(this.newMovementStat);
        }

        createNewMoveStat(): void {
            this.newMoveStat.ownerId = this.character.ownerId;
            var moveResource = this.dataAccessService.getMoveResource();
            moveResource.save(this.newMoveStat);
        }

        deleteMovementRow(rowItem: domain.IMovement): void {
            var movementResource = this.dataAccessService.getMovementResource(rowItem.id);
            movementResource.delete(() => this.getCharacterMovementData());
        }

        deleteMoveRow(rowItem: domain.IMove): void {
            var moveResource = this.dataAccessService.getMoveResource(rowItem.id);
            moveResource.delete(() => this.getCharacterMoveData());
        }

        clearMoveStat(): void {
            this.newMoveStat.name = '';
            this.newMoveStat.hitboxActive = '';
            this.newMoveStat.firstActionableFrame = 0;
            this.newMoveStat.baseDamage = 0;
            this.newMoveStat.angle = 0;
            this.newMoveStat.baseKnockBackSetKnockback = '';
            this.newMoveStat.knockbackGrowth = '';
            this.newMoveStat.landingLag = '';
            this.newMoveStat.autoCancel = '';

            this.showMoveRow();
        }

        clearMovementStat(): void {
            this.newMovementStat.name = '';
            this.newMovementStat.value = 0;
            this.showMovementRow();
        }

        showMoveRow(): void {
            this.showNewMoveRow = !this.showNewMoveRow;
        }

        showMovementRow(): void {
            this.showNewMovementRow = !this.showNewMovementRow;
        }
    }

    angular
        .module("common.services")
        .controller("AdminCharacterCtrl",
        AdminCharacterCtrl);
}