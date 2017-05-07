Feature: MovementsApi
	As an api consumer who needs movement data
	I want to be given character movement data when requested

@GetAll
Scenario: Request All Character Movement data
	Given The api route of api/movements
	When I request all data
	Then The result should be a list of all character movement data

@Get
Scenario: Request one single movements data
	Given The api route of api/movements/{id}
	When I request one specific item by id 590e8b844696594ed4968430
	Then The result should be just that movement data
