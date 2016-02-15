using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Kurogane.Data.RestApi.Infrastructure
{
    public static class ExtendedClaimsProvider
    {
        public static IEnumerable<Claim> GetClaims(ApplicationUser user)
        {

            List<Claim> claims = new List<Claim>();

            var daysInWork = (DateTime.Now.Date - user.JoinDate).TotalDays;

            if (daysInWork > 90)
            {
                claims.Add(CreateClaim("FTE", "1"));

            }
            else {
                claims.Add(CreateClaim("FTE", "0"));
            }

            return claims;
        }

        public static Claim CreateClaim(string type, string value)
        {
            return new Claim(type, value, ClaimValueTypes.String);
        }

    }
}