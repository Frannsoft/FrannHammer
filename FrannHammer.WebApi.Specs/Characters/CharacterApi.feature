Feature: CharacterApi
	As an api consumer who needs character data
	I want to be given character metadata when requested

@GetAll
Scenario: Request all character metadata
	When I request all character metadata
	Then the result should be a list of all character metadata

@Get
Scenario: Request one single characters metadata
	When I request one specific character's metadata by id 5906b9c34696595e4c610707
	Then the result should be just that character metadata
