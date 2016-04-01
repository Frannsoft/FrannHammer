module app.domain {

    interface IAdminAttributeModel {

        attributes: domain.ICharacterAttributeRow[];
        headers: string[];
        attributeType: number;
        saveAttribute(attribute: ICharacterAttribute): void;

    }

    export interface IAdminAttributeParams extends ng.route.IRouteParamsService {
        attributeType: number;
    }

    class AdminAttributeCtrl implements IAdminAttributeModel {

        attributes: domain.ICharacterAttributeRow[];
        headers: string[];
        attributeType: number;

        static $inject = ["dataAccessService", "$location", "$routeParams"];
        constructor(private dataAccessService: common.DataAccessService,
            private $location: ng.ILocationService,
            private $routeParams: IAdminAttributeParams) {

            this.attributes = [];
            this.attributeType = $routeParams.attributeType;

            var attributesResource = dataAccessService.getAttributesOfType(this.attributeType);
            attributesResource.query((data: domain.ICharacterAttributeRow[]) => {
                this.attributes = data;
                this.headers = this.attributes[0].rawHeaders;
            });
        }

        saveAttribute(attribute: ICharacterAttribute): void {
            var attributesResource = this.dataAccessService.getAttributesOfType(this.attributeType);
            attributesResource.save({ id: attribute.id }, attribute);
        }


    }

    angular
        .module("common.services")
        .controller("AdminAttributeCtrl",
        AdminAttributeCtrl);
}