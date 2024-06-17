﻿using CJF.Sgcpj.Judicatura.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Application.Promociones.Comandos;
using Microsoft.Extensions.Logging;
using Moq;

namespace CJF.Sgcpj.Judicatura.Application.UnitTests.Common.Behaviours
{
    public class RequestLoggerTests
    {
        private readonly Mock<ILogger<AgregarPromocionComando>> _logger;
        private readonly Mock<ICurrentUserService> _currentUserService;
        private readonly Mock<IIdentityService> _identityService;


        public RequestLoggerTests()
        {
            _logger = new Mock<ILogger<AgregarPromocionComando>>();

            _currentUserService = new Mock<ICurrentUserService>();

            _identityService = new Mock<IIdentityService>();
        }

        // [Test]
        // public async Task ShouldCallGetUserNameAsyncOnceIfAuthenticated()
        // {
        //     _currentUserService.Setup(x => x.UserId).Returns("Administrator");

        //     var requestLogger = new LoggingBehaviour<CreateTodoItemCommand>(_logger.Object, _currentUserService.Object, _identityService.Object);

        //     await requestLogger.Process(new CreateTodoItemCommand { ListId = 1, Title = "title" }, new CancellationToken());

        //     _identityService.Verify(i => i.GetUserNameAsync(It.IsAny<string>()), Times.Once);
        // }

        // [Test]
        // public async Task ShouldNotCallGetUserNameAsyncOnceIfUnauthenticated()
        // {
        //     var requestLogger = new LoggingBehaviour<CreateTodoItemCommand>(_logger.Object, _currentUserService.Object, _identityService.Object);

        //     await requestLogger.Process(new CreateTodoItemCommand { ListId = 1, Title = "title" }, new CancellationToken());

        //     _identityService.Verify(i => i.GetUserNameAsync(null), Times.Never);
        // }
    }
}
