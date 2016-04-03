module app.domain {
    
    interface ICharacterAttributeModel {
        attributes: domain.ICharacterAttributeRow[];
        headers: string[];
        attributeType: number;
        attributeTypeName: string;
        predicate: string;
        reverse: boolean;
        order(predicate: string): void;
        goToCharacter(rowItem: ICharacterAttribute): void;
    }    

    interface ICharacterAttributeParams extends ng.route.IRouteParamsService {
        attributeType: number;
    }

    class CharacterAttributeCtrl implements ICharacterAttributeModel {
        
        attributeType: number;
        attributeTypeName: string;
        headers: string[];
        attributes: domain.ICharacterAttributeRow[];
        predicate: string = 'rank';
        reverse: boolean = true;

        static $inject = ["dataAccessService", "$location", "$routeParams"];
        constructor(private dataAccessService: common.DataAccessService,
            private $location: ng.ILocationService,
            private $routeParams: ICharacterAttributeParams) {

            this.attributes = [];
            this.attributeType = $routeParams.attributeType;

            var attributesResource = dataAccessService.getCharacterAttributesOfSmashAttributeType(this.attributeType);
            attributesResource.query((data: domain.ICharacterAttributeRow[]) => {
                this.attributes = data;
                this.headers = this.attributes[0].rawHeaders;
            });

            var attributeTypesResource = dataAccessService.getSmash4AttributeTypes(this.attributeType);
            attributeTypesResource.get((data: domain.ISmashAttributeType) => {
                this.attributeTypeName = data.name;
            });
        }

        order(predicate: string): void {
            this.reverse = (predicate === predicate) ? !this.reverse : false;
            this.predicate = predicate;
        }

        goToCharacter(rowItem: ICharacterAttribute): void {
            this.$location.search('attributeType', null); //cleanup url
            this.$location.path('/character/' + rowItem.ownerId);
        }
    }

    angular
        .module("common.services")
        .controller("CharacterAttributeCtrl",
            CharacterAttributeCtrl);
}