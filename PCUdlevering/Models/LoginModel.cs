using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.Protocols;
using System.Linq;
using System.Web;

namespace PCUdlevering.Models
{
    public class LoginModel
    {
        public class Dal
        {
            string username, password;
            public bool ValidateUser(string username, string password)
            {
                bool _valid = false;

                LdapDirectoryIdentifier identifier = new LdapDirectoryIdentifier("10.255.1.1", 389);
                LdapConnection connection = new LdapConnection(identifier)
                {
                    Credential = new System.Net.NetworkCredential(username, password)
                };

                try
                {
                    connection.Bind();
                    PrincipalContext context = new PrincipalContext(ContextType.Domain, "10.255.1.1", username, password);
                    UserPrincipal principal = UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName, username);
                    _valid = true;
                }

                catch
                {

                }

                return _valid;
            }
            public string GetInformation(string username)
            {
                string _fullName = null;
                // set up domain context using the default domain you're currently logged in 
                using (PrincipalContext context = new PrincipalContext(ContextType.Domain))
                {
                    // find a user
                    UserPrincipal user = UserPrincipal.FindByIdentity(context, username);

                    /* or if you're interested in the *currently logged in* user,
                       then you could also use:
                    UserPrincipal user = UserPrincipal.Current;
                    */

                    if (user != null)
                    {
                        // get the "DisplayName" property ("Fullname" is WinNT specific)
                        _fullName = user.DisplayName;

                        // do something here....        
                    }
                }

                return _fullName;
            }
        }
    }
}