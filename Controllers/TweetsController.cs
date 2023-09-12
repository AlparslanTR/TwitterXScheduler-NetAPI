using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Tweetinvi;
using Tweetinvi.Models;
using TwitterXScheduler.Models;

namespace TwitterXScheduler.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TweetsController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> PostTweet(PostTweetRequestDto postTweet)
        {
            var client = new TwitterClient("yRx3RV2I5wGQKr7KADZnUOVWX", "fRX8y3WA0pFG57w25YcMt3ORYFTadONOjYOT3bERWIG8eC5ym4", "852529751210250240-DxUt3XeEn7Tlp7887odqZJCL0P1FZpz", "UZWcd33lyRjZW0WSu1h2N4kKKxjxfnTBz7aFNhJG5kuJW");
            var result = await client.Execute.AdvanceRequestAsync(BuildTwitterRequest(postTweet, client));

            return Ok(result.Content);

        }

        private static Action<ITwitterRequest> BuildTwitterRequest(PostTweetRequestDto postTweet, TwitterClient client)
        {
            return (ITwitterRequest request) =>
            {
                var jsonBody = client.Json.Serialize(postTweet);
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                request.Query.Url = "https://api.twitter.com/2/tweets";
                request.Query.HttpMethod = Tweetinvi.Models.HttpMethod.POST;
                request.Query.HttpContent = content;
            };
        }
    }
}
