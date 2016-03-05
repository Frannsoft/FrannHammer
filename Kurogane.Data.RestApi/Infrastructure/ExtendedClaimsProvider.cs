using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Kurogane.Data.RestApi.Infrastructure
{
    public static class ExtendedClaimsProvider
    {
        public static IEnumerable<Claim> GetClaims(ApplicationUser user)
        {

            var claims = new List<Claim>();

            var daysInWork = (DateTime.Now.Date - user.JoinDate).TotalDays;

            claims.Add(daysInWork > 90 ? CreateClaim("FTE", "1") : CreateClaim("FTE", "0"));

            return claims;
        }

        public static Claim CreateClaim(string type, string value)
        {
            return new Claim(type, value, ClaimValueTypes.String);
        }

    }
}