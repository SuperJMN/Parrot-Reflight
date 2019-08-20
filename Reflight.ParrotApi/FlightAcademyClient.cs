using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Refit;

namespace Reflight.ParrotApi
{
    public static class FlightAcademyClient
    {
        public static async Task<IFlightAcademyClient> Create(string username, string password, Uri uri = null)
        {
            if (uri == null)
            {
                uri = new Uri("http://academy.ardrone.com/api3");
            }

            return new FlightAcademyWrapper(await CreateInner(username, password, uri));
        }

        private static async Task<IFlightAcademyClient> CreateInner(string identifier, string password, Uri uri)
        {
            var httpClientHandler = new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                Credentials = new NetworkCredential(identifier, password)
            };

            var httpClient = new HttpClient(httpClientHandler)
            {
                BaseAddress = uri
            };

            var credential = await GetCredentialCore(httpClient, identifier, password);

            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", Convert.ToBase64String(FormatCredential(credential)));

            return RestService.For<IFlightAcademyClient>(httpClient, new RefitSettings
            {
                JsonSerializerSettings = new JsonSerializerSettings
                {
                    Converters =
                    {
                        new TimeSpanJsonConverter(),
                        new JsonDateTimeOffsetConverter()
                    }
                }
            });
        }

        private static async Task<NetworkCredential> GetCredentialCore(HttpClient httpClient, string identifier, string password)
        {
            if (identifier.Contains("@"))
            {
                return await GetCredentialsFromEmailLogin(httpClient, identifier, password);
            }

            return new NetworkCredential(identifier, password);
        }

        private static async Task<NetworkCredential> GetCredentialsFromEmailLogin(HttpClient httpClient, string email, string password)
        {
            var names = new[]
            {
                new KeyValuePair<string, string>("login", email),
                new KeyValuePair<string, string>("password", password)
            };
            var formUrlEncodedContent = new FormUrlEncodedContent(names);
            var rp = await httpClient.PostAsync(new Uri("https://accounts.parrot.com/V3/logform"), formUrlEncodedContent);
            var uri = rp.RequestMessage.RequestUri;
            var parsed = ParseQueryString(uri.ToString());
            var credentials = parsed["ca"];
            var des = JObject.Parse(credentials);

            var username = (string)des["user"];
            var retrievedPassword = (string)des["pwd"];

            return new NetworkCredential(username, retrievedPassword);
        }

        private static byte[] FormatCredential(NetworkCredential credential)
        {
            return Encoding.ASCII.GetBytes($"{credential.UserName}:{credential.Password}");
        }

        private static Dictionary<string, string> ParseQueryString(string requestQueryString)
        {
            Dictionary<string, string> rc = new Dictionary<string, string>();
            string[] part = requestQueryString.Split('&', '?');
            foreach (var row in part)
            {
                if (string.IsNullOrEmpty(row)) continue;
                int index = row.IndexOf('=');
                if (index < 0) continue;
                {
                    rc[Uri.UnescapeDataString(row.Substring(0, index))] = Uri.UnescapeDataString(row.Substring(index + 1)); // use Unescape only parts          
                }
            }
            return rc;
        }
    }
}