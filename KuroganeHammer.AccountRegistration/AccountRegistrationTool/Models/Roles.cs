using System.Collections.Generic;
using System.Linq;

namespace AccountRegistrationTool.Models
{
    public class Roles
    {
        internal List<string> UserRoles { get; }

        public Roles(string[] roles)
        {
            UserRoles = roles.ToList();
        }

        public Roles(Dictionary<string, bool> roleValues)
        {
            UserRoles = new List<string>();

            foreach (var key in from key in roleValues.Keys
                                let value = roleValues[key]
                                where value
                                select key)
            {
                UserRoles.Add(key);
            }
        }
    }
}