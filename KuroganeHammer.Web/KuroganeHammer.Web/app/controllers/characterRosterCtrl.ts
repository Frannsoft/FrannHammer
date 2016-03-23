module app.domain {

    interface ICharacterRosterModel {
        characters: domain.ICharacter[];
        goToCharacter(character: ICharacterMetadata): void;
    }

    class CharacterRosterCtrl implements ICharacterRosterModel {

        characters: domain.ICharacter[];

        static $inject = ["dataAccessService", "$location"];
        constructor(private dataAccessService: common.DataAccessService,
        private $location: ng.ILocationService) {
            this.characters = [];

            var characterResource = dataAccessService.getCharacterResource();
            characterResource.query((data: domain.ICharacter[]) => {
                this.characters = data;
            });
        }

        goToCharacter(character: ICharacterMetadata): void {
            this.$location.path('/character/' + character.ownerId);
        }
    }

    angular
        .module("common.services")
        .controller("CharacterRosterCtrl",
        CharacterRosterCtrl);
}