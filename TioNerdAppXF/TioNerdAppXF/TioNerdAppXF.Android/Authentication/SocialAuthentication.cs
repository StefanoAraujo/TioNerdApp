using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using TioNerdAppXF.Authentication;
using TioNerdAppXF.Droid.Authentication;
using Xamarin.Forms;

[assembly: Dependency(typeof(SocialAuthentication))]
namespace TioNerdAppXF.Droid.Authentication
{
    public class SocialAuthentication : IAuthentication
    {

        public async Task<MobileServiceUser> LoginAsync(MobileServiceClient client, MobileServiceAuthenticationProvider provider,
            IDictionary<string, string> paremeters = null)
        {
            try
            {
                var user = await client.LoginAsync(Forms.Context, provider);
                
                return user;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}