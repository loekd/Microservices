using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Publisher.Repositories;
using PubSub;

namespace Publisher.Controllers
{
    [Route("api/[controller]")]
    public class PubSubController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ISubscriptionRepository _subscriptionRepository;

        public PubSubController(ISubscriptionRepository subscriptionRepository, HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _subscriptionRepository = subscriptionRepository ?? throw new ArgumentNullException(nameof(subscriptionRepository));
        }

        [HttpPost]
        public IActionResult Post([FromBody]Subscription subscription)
        {
            if (subscription == null)
                return BadRequest();
            
            _subscriptionRepository.Add(subscription);
            return Ok(subscription);
        }
        

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _subscriptionRepository.Delete(id);
            return NoContent();
        }

        [HttpPost("~/api/[controller]/publish/{eventType}")]
        public async Task<IActionResult> Publish(string eventType)
        {
            if (eventType == null) return BadRequest();

            var requestBody = HttpContext.Request.Body;
            requestBody.Position = 0;
            string json;
            using (var reader = new StreamReader(requestBody, true))
            {
                json = await reader.ReadToEndAsync();
            }

            HttpContent content = new StringContent(json);
            content.Headers.ContentType.MediaType = "application/json";

            var subscriptions = _subscriptionRepository.GetUrlsForEventType(eventType);
            foreach (var subscription in subscriptions)
            {
                await _httpClient.PostAsync($"{subscription}/{eventType}", content).ConfigureAwait(true);
            }
            return Ok();
        }
    }

   
}
