using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace DiscBot.Modules
{
    public class MovieApiCalls
    {
        
        public async void MovieCreate(string apiKey, string movieRequested)
        {
            var client = new HttpClient();

            // testing
            var uriMovie = Uri.EscapeDataString(movieRequested);
            // get
            var uriGet = $"http://localhost:7878/api/v3/movie/lookup?term={uriMovie}&apiKey={apiKey}";
            using var httpResponse = await client.GetAsync(uriGet, HttpCompletionOption.ResponseHeadersRead);
            var responseJson = httpResponse.Content.ReadAsStringAsync();
            var movieJsonObject = Newtonsoft.Json.JsonConvert.DeserializeObject<List<dynamic>>(responseJson.Result);
            if(movieJsonObject == null || !movieJsonObject.Any())
            {
                return;
            }
            var actualJson = movieJsonObject[0];
            actualJson["id"] = 0;
            actualJson["addOptions"] = JObject.Parse(
                @"{""monitor"":""movieOnly"",
                   ""searchForMovie"":true}");
            actualJson["rootFolderPath"] = "D:\\Movies 4TB";
            actualJson["monitored"] = true;
            actualJson["qualityProfileId"] = 1;
            actualJson["minimumAvailability"] = "announced";


            // post
            var uriPost = $"http://localhost:7878/api/v3/movie?apikey={apiKey}";
            using var request = new HttpRequestMessage(HttpMethod.Post, uriPost);

            var stringPayload = JsonConvert.SerializeObject(actualJson);
            request.Content = new StringContent(stringPayload, Encoding.UTF8, "application/json");
            var response = await client.SendAsync(request);

            // logging
/*            var movie = System.Text.Json.JsonSerializer.Deserialize<Movie>(actualJson);
            if (movie == null)
            {
                return;
            }

            Console.WriteLine($"title: {movie.title}");
            Console.WriteLine($"originalTitle: {movie.originalTitle}");
            Console.WriteLine($"folder: {movie.rootFolderPath}");*/

        }
        // Uri.EscapeUriString to encode spaces (%20)

    }
}
