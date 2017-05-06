Feature: CharactersApi
	As an api consumer who needs character data
	I want to be given character metadata when requested

@GetAll
Scenario: Request all character metadata
	Given The api route of api/characters
	When I request all data
	Then the result should be a list of all character metadata

@Get
Scenario: Request one single characters metadata
	Given The api route of api/characters/{id}
	When I request one specific item by id 5906b9c34696595e4c610707
	Then the result should be just that characters metadata
