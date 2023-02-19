using System.Net;
using System.Threading.Tasks;
using AutoFixture;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Configurations;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Extensions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.FunctionApp.InProc.SecurityFlows;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.FunctionApp.Models;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Microsoft.Azure.WebJobs.Extensions.OpenApi.FunctionApp.InProc
{
    public class DerivedClassTrigger
    {
        private readonly ILogger<PetHttpTrigger> _logger;
        private readonly OpenApiSettings _openapi;
        private readonly Fixture _fixture;

        public DerivedClassTrigger(ILogger<PetHttpTrigger> log, OpenApiSettings openapi, Fixture fixture)
        {
            this._logger = log.ThrowIfNullOrDefault();
            this._openapi = openapi.ThrowIfNullOrDefault();
            this._fixture = fixture.ThrowIfNullOrDefault();
        }

        [FunctionName(nameof(DerivedClassTrigger.HideInherited))]
        [OpenApiOperation(operationId: "hideInherited", tags: new[] { "derived" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(MyDerived), Description = "The OK response")]
        public async Task<IActionResult> HideInherited(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "derived/hideinherited")] HttpRequest req)
        {
            this._logger.LogInformation($"document title: {this._openapi.DocTitle}");

            return await Task.FromResult(new OkObjectResult(this._fixture.Create<MyDerived>())).ConfigureAwait(false);
        }
    }
}

