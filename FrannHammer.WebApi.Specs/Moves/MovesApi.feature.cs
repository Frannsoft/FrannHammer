﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:2.1.0.0
//      SpecFlow Generator Version:2.0.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace FrannHammer.WebApi.Specs.Moves
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.1.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("MovesApi")]
    public partial class MovesApiFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "MovesApi.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "MovesApi", "\tAs an api consumer who needs move data\r\n\tI want to be given move data when reque" +
                    "sted", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.TestFixtureTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [NUnit.Framework.SetUpAttribute()]
        public virtual void TestInitialize()
        {
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Request all move data")]
        [NUnit.Framework.CategoryAttribute("GetAll")]
        public virtual void RequestAllMoveData()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Request all move data", new string[] {
                        "GetAll"});
#line 6
this.ScenarioSetup(scenarioInfo);
#line 7
 testRunner.Given("The api route of api/moves", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 8
 testRunner.When("I request all data", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 9
 testRunner.Then("The result should be a list of all move data", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Request all moves by name")]
        [NUnit.Framework.CategoryAttribute("GetAllWithName")]
        public virtual void RequestAllMovesByName()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Request all moves by name", new string[] {
                        "GetAllWithName"});
#line 12
this.ScenarioSetup(scenarioInfo);
#line 13
 testRunner.Given("The api route of api/moves/name/{name}", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 14
 testRunner.When("I request one specific item by name Nair", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 15
 testRunner.Then("The result should be all moves that match that name", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Requesting specific property of moves matching the given name returns parsed data" +
            " for that property of those moves")]
        [NUnit.Framework.CategoryAttribute("GetAllNonHitboxDataForMovesByName")]
        [NUnit.Framework.TestCaseAttribute("autoCancel", "cancel1;cancel2;rawvalue;movename", new string[0])]
        [NUnit.Framework.TestCaseAttribute("firstActionableFrame", "frame;rawvalue;movename", new string[0])]
        [NUnit.Framework.TestCaseAttribute("landingLag", "frames;rawvalue;movename", new string[0])]
        public virtual void RequestingSpecificPropertyOfMovesMatchingTheGivenNameReturnsParsedDataForThatPropertyOfThoseMoves(string property, string moveproperties, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "GetAllNonHitboxDataForMovesByName"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Requesting specific property of moves matching the given name returns parsed data" +
                    " for that property of those moves", @__tags);
#line 18
this.ScenarioSetup(scenarioInfo);
#line 19
 testRunner.Given("The api route of api/moves/name/{name}/{property}", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 20
 testRunner.When(string.Format("I request all of the {0} property data for a move by Nair", property), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 21
 testRunner.Then(string.Format("The result should be a list of {0} for the specific property in the moves that ma" +
                        "tch that name", moveproperties), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Request hitbox-based property of moves by name")]
        [NUnit.Framework.TestCaseAttribute("baseDamage", new string[0])]
        [NUnit.Framework.TestCaseAttribute("baseKnockback", new string[0])]
        [NUnit.Framework.TestCaseAttribute("hitboxActive", new string[0])]
        [NUnit.Framework.TestCaseAttribute("angle", new string[0])]
        [NUnit.Framework.TestCaseAttribute("setKnockback", new string[0])]
        [NUnit.Framework.TestCaseAttribute("knockbackGrowth", new string[0])]
        public virtual void RequestHitbox_BasedPropertyOfMovesByName(string property, string[] exampleTags)
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Request hitbox-based property of moves by name", exampleTags);
#line 29
this.ScenarioSetup(scenarioInfo);
#line 30
 testRunner.Given("The api route of api/moves/name/{name}/{property}", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 31
 testRunner.When(string.Format("I request all of the {0} property data for a move by Jab 1", property), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 32
 testRunner.Then("The result should be a list of hitbox1;hitbox2;hitbox3;hitbox4;hitbox4;rawvalue;m" +
                    "ovename for the specific property in the moves that match that name", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
