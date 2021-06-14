using DotNet.CleanArchitecture.Core;
using DotNet.CleanArchitecture.Core.Interfaces;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;

namespace Events.Repository.Query
{
    public class EventQueryFunction
    {
        private readonly IEventStore _eventStore;

        public EventQueryFunction(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        [Function(nameof(EventQueryFunction))]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req,
            FunctionContext executionContext)
        {
            string requestBody = await req.ReadAsStringAsync();

            var data = JsonConvert.DeserializeAnonymousType(requestBody, new { Id = 0, Name = string.Empty });

            var events = await _eventStore.ReadAsync(data.Name, data.Id.ToString());

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteStringAsync(events.ToJson());

            return response;
        }
    }
}
