Feature: CharacterAttributesApi
	As an api consumer who needs character attribute data
	I want to be given character attribute data when requested

@GetAll
Scenario: Request All Character Attribute Rows
	Given The api route of api/characterattributes
	When I request all data
	Then The result should be a list of all character attribute row entries

Scenario: I want to get back the names of all available character attributes
	Given The api route of api/characterattributes/types
	When I request all data
	Then The result should be a list of all character attribute types