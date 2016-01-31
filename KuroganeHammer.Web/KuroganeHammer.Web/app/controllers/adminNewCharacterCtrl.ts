module app.domain {

    interface IAdminNewCharacterModel {

        character: app.domain.ICharacterMetadata;
        movementData: app.domain.Movement[];
        moveData: app.domain.Move[];
        showNewMovementRow: boolean;
        showNewMoveRow: boolean;
        newMovementStat: app.domain.IMovement;
        newMoveStat: app.domain.IMove;
        tempMovementData: app.domain.Movement[];
        tempMoveData: app.domain.Move[];
    }

    class AdminNewCharacterCtrl implements IAdminNewCharacterModel {

        static $inject = ["dataAccessService", "$routeParams"];
        constructor(
            public dataAccessService: app.common.DataAccessService,
            private $routeParams: app.domain.IAdminCharacterParams,
            public character: app.domain.ICharacterMetadata,
            public movementData: app.domain.Movement[],
            public moveData: app.domain.Move[],
            public showNewMovementRow: boolean,
            public showNewMoveRow: boolean,
            public newMovementStat: app.domain.IMovement,
            public newMoveStat: app.domain.IMove,
            public tempMovementData: app.domain.Movement[],
            public tempMoveData: app.domain.Move[]) {

            var charId = $routeParams.characterId;
            this.movementData = [];
            this.moveData = [];
            this.tempMovementData = [];
            this.tempMoveData = [];
        }

        saveCharacter(): void {
            this.moveData = this.tempMoveData;
            this.movementData = this.tempMovementData;

            //TODO: need image paths here too? - maybe just image name - 
            //path will be decided server side and then stored in db.

            var characterResource = this.dataAccessService.getCharacterResource();
            characterResource.save(this.character, (newChar: app.domain.ICharacterMetadata) => {
                this.createNewMovementStats(newChar.id, this.movementData);
                this.createNewMoveStats(newChar.id, this.moveData);
            });
        }

        createNewMoveStats(ownerId: number, moveData: app.domain.Move[]): void {

            var moveResource = this.dataAccessService.getMoveResource();

            moveData.forEach((move) => {
                move.ownerId = ownerId;
                moveResource.save(move);
            });

            this.clearTempMoveStat();
        }

        createNewMovementStats(ownerId: number, movementData: app.domain.Movement[]): void {

            var movementResource = this.dataAccessService.getMovementResource();

            movementData.forEach((movement) => {
                movement.ownerId = ownerId;
                movementResource.save(movement);
            });

            this.clearTempMovementStat();
        }

        clearTempMoveStat() {
            this.newMoveStat.name = '';
            this.newMoveStat.hitboxActive = '';
            this.newMoveStat.firstActionableFrame = 0;
            this.newMoveStat.baseDamage = 0;
            this.newMoveStat.angle = 0;
            this.newMoveStat.baseKnockBackSetKnockback = '';
            this.newMoveStat.knockbackGrowth = '';
            this.newMoveStat.landingLag = '';
            this.newMoveStat.autoCancel = '';
        }

        clearTempMovementStat() {
            this.newMovementStat.id = 0;
            this.newMovementStat.name = '';
            this.newMovementStat.value = 0;
        }

        showMoveRow(): void {
            this.showNewMoveRow = !this.showNewMoveRow;
        }

        showMovementRow(): void {
            this.showNewMovementRow = !this.showNewMovementRow;
        }

        addTempMovement(): void {

            var movement: Movement = new Movement(this.newMovementStat.name,
                this.newMovementStat.value);

            this.tempMovementData.push(movement);

            this.clearTempMovementStat();
        }

        addTempMove(): void {
            var move: Move = new Move(
                this.newMoveStat.name,
                this.newMoveStat.hitboxActive,
                this.newMoveStat.firstActionableFrame,
                this.newMoveStat.baseDamage,
                this.newMoveStat.angle,
                this.newMoveStat.baseKnockBackSetKnockback,
                this.newMoveStat.knockbackGrowth,
                this.newMoveStat.landingLag,
                this.newMoveStat.autoCancel);

            this.tempMoveData.push(move);

            this.clearTempMoveStat();
        }
    }

    angular
        .module("common.services")
        .controller("AdminNewCharacterCtrl",
        AdminNewCharacterCtrl);
}