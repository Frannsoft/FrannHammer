﻿using NUnit.Framework;

namespace FrannHammer.Api.Tests.Smoke
{
    public class LoginBaseSmokeTest : BaseSmokeTest
    {
        [Test]
        [Ignore("Still working to setup Owin self hosting")]
        public void ShouldBeAbleToLoginWithBasicUser()
        {
            Assert.IsTrue(LoggedInBasicClient != null);
            Assert.IsTrue(LoggedInBasicClient.DefaultRequestHeaders.Authorization.Parameter.Length > 0);
            Assert.That(LoggedInBasicClient.DefaultRequestHeaders.Authorization.Scheme.Length > 0);
        }

        [Test]
        [Ignore("Still working to setup Owin self hosting")]
        public void ShouldBeAbleToLoginWithAdminUser()
        {
            Assert.That(LoggedInAdminClient != null);
            Assert.IsTrue(LoggedInAdminClient.DefaultRequestHeaders.Authorization.Parameter.Length > 0);
            Assert.That(LoggedInAdminClient.DefaultRequestHeaders.Authorization.Scheme.Length > 0);
        }
    }
}
