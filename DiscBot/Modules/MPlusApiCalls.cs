using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace DiscBot.Modules
{
    public class MPlusApiCalls
    {

        public string MplusRequest(string apiKey, string mplusRequested)
        {
            var client = new HttpClient();

            // get
            var uriGet = $"https://raider.io/api/characters/us/illidan/{mplusRequested}?season=season-df-1&tier=29";
            var httpResponse = client.GetAsync(uriGet).Result;
            var responseJson = httpResponse.Content.ReadAsStringAsync().Result;
            var obj = JsonConvert.DeserializeObject<dynamic>(responseJson);
            if (obj == null)
                return "0";

            var mplusScore = obj.characterDetails;

            return mplusScore["mythicPlusScores"]["all"]["score"];

            
            
        }

    }
}
