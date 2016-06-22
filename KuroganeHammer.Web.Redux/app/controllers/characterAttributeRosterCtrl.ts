module app.domain {
    
    interface ICharacterAttributeRosterModel {
        goToAttribute(id: number): void;
        attributeTypes: ISmashAttributeType[];
    }    

    class CharacterAttributeRosterCtrl implements ICharacterAttributeRosterModel {

        attributeTypes: ISmashAttributeType[];

        static $inject = ["dataAccessService", "$location"];
        constructor(private dataAccessService: common.DataAccessService,
            private $location: ng.ILocationService) {

            this.attributeTypes = [];

            var attributeTypesResource = dataAccessService.getSmash4AttributeTypes();
            attributeTypesResource.query((data: domain.ISmashAttributeType[]) => {
                this.attributeTypes = data;
            });
        }

        goToAttribute(id: number): void {
            //this.$location.search('attributeType', attributeType);
            this.$location.path('/attributes/' + id + "/ranking");
        }
    }

    angular
        .module("common.services")
        .controller("CharacterAttributeRosterCtrl",
        CharacterAttributeRosterCtrl);
}