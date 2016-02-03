module app.domain {

    interface ICharacterRosterModel {
        characters: app.domain.ICharacter[];
        goToCharacter(character: ICharacterMetadata): void;
    }

    class CharacterRosterCtrl implements ICharacterRosterModel {

        characters: app.domain.ICharacter[];

        static $inject = ["dataAccessService", "$location"];
        constructor(private dataAccessService: app.common.DataAccessService,
        private $location: ng.ILocationService) {
            this.characters = [];

            var characterResource = dataAccessService.getCharacterResource();
            characterResource.query((data: app.domain.ICharacter[]) => {
                this.characters = data;
            });
        }

        goToCharacter(character: ICharacterMetadata): void {
            this.$location.path('/character/' + character.id);
        }
    }

    angular
        .module("common.services")
        .controller("CharacterRosterCtrl",
        CharacterRosterCtrl);
}