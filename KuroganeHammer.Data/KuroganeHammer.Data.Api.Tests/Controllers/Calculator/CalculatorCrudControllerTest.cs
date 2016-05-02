using System.Web.Http.Results;
using KuroganeHammer.Data.Api.Models;
using NUnit.Framework;

namespace KuroganeHammer.Data.Api.Tests.Controllers.Calculator
{
    [TestFixture]
    public class CalculatorCrudControllerTest : BaseControllerTest
    {
        [Test]
        public void ShouldReturnExpectedCalculation()
        {
            CalculatorRequestModel calcRequest = TestObjects.CalcRequest();

            var response = CalculatorController.PostCalculatorRequest(calcRequest) as OkNegotiatedContentResult<CalculatorResponseModel>;

            Assert.That(response?.Content, Is.Not.Null);

            var content = response.Content;
            Assert.That(content.TrainingModeKnockback, Is.EqualTo(53.333333333333336d));
        }
    }
}
