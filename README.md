# FrannHammer

Develop: [![Build status](https://ci.appveyor.com/api/projects/status/ruxmxhiutxe9q8v9/branch/develop?svg=true)](https://ci.appveyor.com/project/Frannsoft/frannhammer-0uqqq/branch/develop)
Master: [![Build status](https://ci.appveyor.com/api/projects/status/y2hs2u8ux4ceaq2k/branch/master?svg=true)](https://ci.appveyor.com/project/Frannsoft/frannhammer/branch/master)


Full stack web site, rest api and database layer for Sm4sh frame data as told by KuroganeHammer.

This is being worked on with KuroganeHammer's permission.  All credit for the actual data that is stored and retrieved goes to him and 
any other people that are involved in that process.

Allows consumers to pull back the data displayed on KuroganeHammer's site as JSON.  The data is stored in a database and is not 
scraped from the site on-call.

See the [Wiki](https://github.com/Frannsoft/FrannHammer/wiki/Character-Data) for more information.

## Getting Started

Live API documentation can be found via [Swagger docs](http://api.kuroganehammer.com/swagger)  
### Features

This allows consumers to pull back the following types of data from the existing KuroganeHammer site:

- Character details such as image links, color theme from KuroganeHammer.com, descriptions, etc.
- Movement details such as Weight, Fall Speed, Walk Speed, etc.
- Move details such as active Hitboxes, First Actionable frames, Knockback Growth, etc.
- Calculate Versus mode Knockback, Rage, Shield stun.
   - More options exist in the [Data.Core project](https://github.com/Frannsoft/FrannHammer/tree/master/KuroganeHammer.Data.Core/Calculations), I just haven't made public API calls for them yet.
   
To see more details on these features, check out these wiki pages:

- [Character data](https://github.com/Frannsoft/FrannHammer/wiki/Character-Data)
- [Move Data](https://github.com/Frannsoft/FrannHammer/wiki/Move-Data)
- [Movement Data](https://github.com/Frannsoft/FrannHammer/wiki/Movement-data)
- [Character Attribute Data](https://github.com/Frannsoft/FrannHammer/wiki/Character-Attribute-Data)
- [Calculator Functionality](https://github.com/Frannsoft/FrannHammer/wiki/Calculator-Functionality)

#### Documentation

[Web API docs here](https://github.com/Frannsoft/FrannHammer/wiki/Web-API-Documentation) - These are different from the Swagger docs above.  Rather than demoing calls and results, this is the server-side code documentation.

[Data.Core docs can be found here](https://github.com/Frannsoft/FrannHammer/wiki/Core-API-Documentation).  This is the guts of the project.  You can use these locally, not everything has web api calls however so you might be responsible for some making any necessary calls yourself right now.  Currently, you need to build these in order to use them.  Depending on demand nuget packages might be made available.  These libs are all created using C# 6.



The live docs (above) are a great place to see the full capability and how one can use it.

##### NOTE: The API is in beta and is subject to rapid change.

##### Issues and Feedback
Changes are being made that could affect public calls.  I'll be sure to post those types of changes in this repo.  If you have an issue, feel free to open one up on the [Issues](https://github.com/Frannsoft/FrannHammer/issues) page or email me at the address in [my Profile](https://github.com/Frannsoft) or twitter at franndotexe.


Thanks for taking a look!
