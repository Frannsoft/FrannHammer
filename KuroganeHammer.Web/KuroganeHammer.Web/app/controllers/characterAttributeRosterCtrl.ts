module app.domain {
    
    interface ICharacterAttributeRosterModel {
        goToAttribute(attributeType: string): void;
    }    


    class CharacterAttributeRosterCtrl implements ICharacterAttributeRosterModel {
        
        static $inject = ["dataAccessService", "$location"];
        constructor(private dataAccessService: common.DataAccessService,
            private $location: ng.ILocationService) {
        }

        goToAttribute(attributeType: string): void {
            this.$location.search('attributeType', attributeType);
            this.$location.path('/attributeranking');
        }
    }

    angular
        .module("common.services")
        .controller("CharacterAttributeRosterCtrl",
        CharacterAttributeRosterCtrl);
}