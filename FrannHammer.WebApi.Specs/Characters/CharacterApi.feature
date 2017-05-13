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
	When I request one specific item by id 5913c30d4696591c50f28616
	Then the result should be just that characters metadata

@GetByName
Scenario: Request one single characters metadata by name
	Given The api route of api/characters/name/{name}
	When I request one specific item by name Bowser
	Then the result should be a list containing just that characters metadata
