module app.domain {

    interface IAdminCharacterModel {

        movementData: app.domain.IMovement[];
        moveData: app.domain.IMove[];
        character: app.domain.ICharacterMetadata;
        showNewMovementRow: boolean;
        showNewMoveRow: boolean;
        newMovementStat: app.domain.IMovement;
        newMoveStat: app.domain.IMove;
        saveCharacterChanges(): void;
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

    interface IAdminCharacterParams extends ng.route.IRouteParamsService {
        characterId: number;
    }

    class AdminCharacterCtrl implements IAdminCharacterModel {

        static $inject = ["dataAccessService", "$routeParams"];
        constructor(public dataAccessService: app.common.DataAccessService,
            private $routeParams: IAdminCharacterParams,
            public character: app.domain.ICharacterMetadata,
            public movementData: app.domain.IMovement[],
            public moveData: app.domain.IMove[],
            public showNewMovementRow: boolean,
            public showNewMoveRow: boolean,
            public newMovementStat: app.domain.IMovement,
            public newMoveStat: app.domain.IMove) {

            var charId = $routeParams.characterId;

            var characterResource = dataAccessService.getCharacterResource(charId);
            characterResource.get((data: app.domain.ICharacterMetadata) => {
                this.character = data;
                this.getCharacterMovementData();
                this.getCharacterMoveData();
            });
        }

        getCharacterMovementData(): void {
            var movementResource = this.dataAccessService.getMovementsOfCharacter(this.character.id);
            movementResource.query((data: app.domain.IMovement[]) => {
                this.movementData = data;
            });
        }

        getCharacterMoveData(): void {
            var moveResource = this.dataAccessService.getMovesOfCharacter(this.character.id);
            moveResource.query((data: app.domain.IMove[]) => {
                this.moveData = data;
            });
        }

        saveCharacterChanges(): void {
            var characterResource = this.dataAccessService.getCharacterResource(this.character.id);
            characterResource.update(this.character);
        }

        saveMovementRow(rowItem: app.domain.IMovement): void {
            var movementResource = this.dataAccessService.getMovementResource(this.character.id);
            movementResource.save({ id: rowItem.id }, rowItem);
        }

        saveMoveRow(rowItem: app.domain.IMove): void {
            var moveResource = this.dataAccessService.getMoveResource(this.character.id);
            moveResource.save({ id: rowItem.id }, rowItem);
        }

        createNewMovementStat(): void {
            this.newMovementStat.ownerId = this.character.id;
            var movementResource = this.dataAccessService.getMovementResource();
            movementResource.save(this.newMovementStat);
        }

        createNewMoveStat(): void {
            this.newMoveStat.ownerId = this.character.id;
            var moveResource = this.dataAccessService.getMoveResource();
            moveResource.save(this.newMoveStat);
        }

        deleteMovementRow(rowItem: app.domain.IMovement): void {
            var movementResource = this.dataAccessService.getMovementResource(rowItem.id);
            movementResource.delete(() => this.getCharacterMovementData());
        }

        deleteMoveRow(rowItem: app.domain.IMove): void {
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