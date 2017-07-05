Feature: CharactersApi
	As an api consumer who needs character data
	I want to be given character metadata when requested

Scenario: Request all character metadata
	Given The api route of api/characters
	When I request all data
	Then the result should be a list of all character metadata


Scenario Outline: Request one single characters metadata
	Given The api route of <apiRoute> 
	When I request one specific item by <routeParameter>
	Then the result should be just that characters metadata

Examples: 
	| apiRoute                   | routeParameter |
	| api/characters/name/{name} | name Bowser    |
	| api/characters/{id}        | id 58          |

Scenario Outline: I want all of the throw data for a character
	Given The api route of <apiRoute>
	When I request one specific item by <routeParameter>
	Then the result should be a list containing just that characters throw data

Examples: 
	| apiRoute                          | routeParameter |
	| api/characters/name/{name}/throws | name Bowser    |
	| api/characters/{id}/throws        | id 58          |
	
Scenario Outline: I want all of the throw data named fthrow for a specific character
	Given The api route of <apiRoute>
	When I request one specific item by <routeParameter>
	Then the result should be a list containing just that characters throw data

Examples: 
	| apiRoute														         | routeParameter |
	| api/characters/name/{name}/moves/search?movetype=throw&movename=fthrow | name Bowser    |
	| api/characters/{id}/moves/search?movetype=throw&movename=fthrow        | id 58          |

Scenario Outline: I want all of the move data for a character
	Given The api route of <apiRoute>
	When I request one specific item by <routeParameter>
	Then the result should be a list containing just that characters move data

Examples: 
	| apiRoute                          | routeParameter |
	| api/characters/name/{name}/moves  | name Bowser    |
	| api/characters/{id}/moves         | id 58          |

Scenario Outline: I want all of the movement data for a character
	Given The api route of <apiRoute>
	When I request one specific item by <routeParameter>
	Then the result should be a list containing just that characters movement data

Examples: 
	| apiRoute                             | routeParameter |
	| api/characters/name/{name}/movements | name Bowser    |
	| api/characters/{id}/movements        | id 58          |

Scenario Outline: I want all of the gravity movement data for a character
	Given The api route of <apiRoute>
	When I request one specific item by <routeParameter>
	Then the result should be a list containing just that characters gravity movement data

Examples: 
	| apiRoute                                                          | routeParameter |
	| api/characters/name/{name}/movements/search?movementname=gravity  | name Bowser    |
	| api/characters/{id}/movements/search?movementname=gravity         | id 58          |

Scenario Outline: I want all the metadata, movement data and attribute data for a specific character in one request
	Given The api route of <apiRoute>
	When I request one specific item by <routeParameter>
	Then the result should be a list containing the metadata, movement data and attribute data for a specific character

Examples: 
	| apiRoute                             | routeParameter |
	| api/characters/name/{name}/details   | name Bowser    |
	| api/characters/{id}/details          | id 58          |

Scenario Outline: I want to get a characters parsed move data
	Given The api route of <apiRoute>
	When I request one specific item by <routeParameter>
	Then the result should be a list containing the parsed out move data for that character

Examples: 
	| apiRoute                                   | routeParameter |
	| api/characters/name/{name}/detailedmoves   | name Bowser    |
	| api/characters/{id}/detailedmoves          | id 58          |

Scenario Outline: I want to get a characters attribute data
	Given The api route of <apiRoute>
	When I request one specific item by <routeParameter>
	Then the result should be a list containing rows of attribute data for that character

Examples: 
	| apiRoute                                         | routeParameter |
	| api/characters/name/{name}/characterattributes   | name Bowser    |
	| api/characters/{id}/characterattributes          | id 58          |

Scenario Outline: I want to get a characters unique properties
	Given The api route of <apiRoute>
	When I request one specific item by <routeParameter>
	Then the result should be a dictionary containing unique data for that character

Examples:
	| apiRoute                                    | routeParameter |
	| api/characters/name/{name}/uniqueproperties | name Cloud     |
	| api/characters/{id}/uniqueproperties        | id 6           |
