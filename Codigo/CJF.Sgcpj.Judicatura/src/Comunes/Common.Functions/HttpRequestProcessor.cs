using System.Net;
using System.Text;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Exceptions;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Microsoft.IdentityModel.Abstractions;

namespace Common.Functions
{
    public class HttpRequestProcessor : IHttpRequestProcessor
    {
        private readonly IMediator mediator;
        private readonly ILogger _log;

        public HttpRequestProcessor(IMediator mediator, ILoggerFactory loggerFactory)
        {
            this.mediator = mediator;
            _log = loggerFactory.CreateLogger(this.GetType());
        }

        public async Task<IActionResult> ExecuteAsync<TRequest, TResponse>(TRequest request)
             where TRequest : IRequest<TResponse>
        {
            try
            {
                var response = await mediator.Send(request);
                return new OkObjectResult(response);
            }
            catch (NotFoundException ex)
            {
                return new NotFoundResult();
            }
            catch (ValidationException ex)
            {
                var errors = ex.Errors;
                return new BadRequestObjectResult(ResultadoGenericoDto.Falla("Ocurrió un error con los parámetros de la solicitud", errors));

            }
            catch (RuleException ex)
            {
                List<string> errors = new List<string>();
                return new BadRequestObjectResult(ResultadoGenericoDto.Falla($"{ex.Mensaje}", errors));
            }
            catch (ForbiddenAccessException ex)
            {
                List<string> errors = new List<string>();
                if (ex.EsPermisos)
                {
                    return  await ContentResult(ex, Guid.NewGuid(), HttpStatusCode.Forbidden);
                }
                return new UnauthorizedObjectResult(ResultadoGenericoDto.Falla("Acceso no autorizado", errors));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Capturada excepcion: {ex.Message}");
                var errorId = Guid.NewGuid();
                string innerException = string.Empty;
                innerException = ex.InnerException?.Message;
                _log.LogError($"{errorId} - {ex.Message} - {ex.StackTrace} - InnerException: {innerException}", ex);

                return await ContentResult(ex, errorId, HttpStatusCode.InternalServerError);
            }
        }

        private static async Task<IActionResult> ContentResult(Exception ex, Guid errorId, HttpStatusCode errorCode)
        {
            var errorResult = ResultadoGenericoDto.Falla(
                $"Ocurrió un error en el servidor",
                new List<string>() { $"{errorId} - {ex.Message}" });

            var response = new HttpResponseMessage(errorCode);
            response.Content = new StringContent(JsonConvert.SerializeObject(errorResult, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }), Encoding.UTF8);
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            // Devolver la respuesta personalizada
            return new ContentResult
            {
                Content = await response.Content.ReadAsStringAsync(),
                StatusCode = (int)response.StatusCode
            };
        }
    }
}