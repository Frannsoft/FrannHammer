Feature: CharactersApi
	As an api consumer who needs character data
	I want to be given character metadata when requested

Scenario: Request all character metadata
	Given The api route of api/characters
	When I request all data
	Then the result should be a list of all character metadata

#Scenario: Request one single characters metadata
#	Given The api route of api/characters/{id}
#	When I request one specific item by id 58
#	Then the result should be metadata for zero suit samus


Scenario Outline: Request one single characters metadata
	Given The api route of <apiRoute> 
	When I request one specific item by <routeParameter>
	Then the result should be just that characters metadata

Examples: 
	| apiRoute                   | routeParameter |
	| api/characters/name/{name} | name Bowser    |
	| api/characters/{id}        | id 58          |

Scenario: I want all of the throw data for a character
	Given The api route of api/characters/name/{name}/throws
	When I request one specific item by name Bowser
	Then the result should be a list containing just that characters throw data
	
Scenario: I want all of the throw data named fthrow for a specific character
	Given The api route of api/characters/name/{name}/moves?movetype=throw&movename=fthrow
	When I request one specific item by name Greninja
	Then the result should be a list containing just that characters throw data

Scenario: I want all of the move data for a character
	Given The api route of api/characters/name/{name}/moves
	When I request one specific item by name Ganondorf
	Then the result should be a list containing just that characters move data

Scenario: I want all of the movement data for a character
	Given The api route of api/characters/name/{name}/movements
	When I request one specific item by name Mario
	Then the result should be a list containing just that characters movement data

Scenario: I want all the metadata, movement data and attribute data for a specific character in one request
	Given The api route of api/characters/name/{name}/details
	When I request one specific item by name Mario
	Then the result should be a list containing the metadata, movement data and attribute data for a specific character

Scenario: I want to get a characters parsed move data
	Given The api route of api/characters/name/{name}/detailedmoves
	When I request one specific item by name Mario
	Then the result should be a list containing the parsed out move data for that character

Scenario: I want to get a characters attribute data
	Given The api route of api/characters/name/{name}/characterattributes
	When I request one specific item by name Mario
	Then the result should be a list containing rows of attribute data for that character
