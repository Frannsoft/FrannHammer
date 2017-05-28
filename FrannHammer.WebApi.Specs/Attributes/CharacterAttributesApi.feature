Feature: CharacterAttributesApi
	As an api consumer who needs character attribute data
	I want to be given character attribute data when requested

@GetAll
Scenario: Request All Character Attribute Rows
	Given The api route of api/characterattributes
	When I request all data
	Then The result should be a list of all character attribute row entries

#@Get
#Scenario: Request one single Character Attribute Row
#	Given The api route of api/characterattributes/{id}
#	When I request one specific item by id 5913c30e4696591c50f28673
#	Then The result should be just that character attribute row
