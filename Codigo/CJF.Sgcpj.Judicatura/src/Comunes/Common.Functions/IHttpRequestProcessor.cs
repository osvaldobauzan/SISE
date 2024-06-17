using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace Common.Functions
{
    public interface IHttpRequestProcessor
    {
        public Task<IActionResult> ExecuteAsync<TRequest, TResponse>(TRequest request)
                 where TRequest : IRequest<TResponse>;

    }
}