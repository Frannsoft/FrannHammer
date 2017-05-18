﻿Feature: MovesApi
	As an api consumer who needs move data
	I want to be given move data when requested

@GetAll
Scenario: Request all move data
	Given The api route of api/moves
	When I request all data
	Then The result should be a list of all move data

@Get
Scenario: Request one single moves data
	Given The api route of api/moves/{id}
	When I request one specific item by id 5913c30e4696591c50f28629
	Then The result should be just that moves data

@GetAllWithName
Scenario: Request all moves by name
	Given The api route of api/moves/name/{name}
	When I request one specific item by name Jab 1
	Then The result should be all moves that match that name

@GetAllNonHitboxDataForMovesByName
Scenario Outline: Requesting specific property of moves matching the given name returns parsed data for that property of those moves
	Given The api route of api/moves/name/{name}/{property}
	When I request all of the <property> property data for a move by name Nair
	Then The result should be a list of <moveproperties> for the specific property in the moves that match that name

	Examples: 
	| property             | moveproperties                          |
	| autoCancel           | cancel1;cancel2;rawvalue;movename		 |
	| firstActionableFrame | frame;rawvalue;movename                 |
	| landingLag           | frames;rawvalue;movename                |

Scenario Outline: Request hitbox-based property of moves by name
	Given The api route of api/moves/name/{name}/{property}
	When I request all of the <property> property data for a move by name Jab 1
	Then The result should be a list of hitbox1;hitbox2;hitbox3;hitbox4;hitbox4;rawvalue;movename for the specific property in the moves that match that name

	Examples: 
	| property        | 
	| baseDamage      |
	| baseKnockback   |
	| hitboxActive    |
	| angle           |
	| setKnockback    |
	| knockbackGrowth |
