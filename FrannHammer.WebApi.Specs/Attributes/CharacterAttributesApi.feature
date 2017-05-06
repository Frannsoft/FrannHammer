Feature: CharacterAttributesApi
	As an api consumer who needs character attribute data
	I want to be given character attribute data when requested

@GetAll
Scenario: Request All Character Attribute Rows
	When I request all character attribute rows
	Then The result should be a list of all character attribute row entries

@Get
Scenario: Request one single Character Attribute Row
	When I request one specific character attribute row by id 58ff7e0306eafb0d9cf025f4
	Then The result should be just that character attribute row
