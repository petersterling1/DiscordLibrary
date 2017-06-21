using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordLibrary
{
    public class ClassMain
    {
        private String discordAPI = "https://discordapp.com/api/";

        private Boolean usingOAuth = false;
        private String discordGateway = null;

        public void GetDiscordGateway()
        {
            HTTPRequest request = new HTTPRequest();
            String response = request.GetRequest(discordAPI + "gateway");

            Console.WriteLine(response);
        }

        /// <summary>
        /// Whether or not to use OAuth when authorizing requests to Discord. Default is FALSE. If set to false then bot authorization will be used instead.
        /// </summary>
        /// <param name="oAuth">Whether to use OAuth or not</param>
        public void SetOAuth(Boolean oAuth)
        {
            usingOAuth = oAuth;
        }

    }
}
