using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.LogOperationsDtos;
using SIGEBI.Application.Facades_Classes.Configuration;
using SIGEBI.Application.Interfaces;
using SIGEBI.Application.Validators.Configuration.LibroValidators;
using SIGEBI.Application.Validators.Configuration.LogOpValidators;
using SIGEBI.Persistence.Context;
using SIGEBI.Persistence.Repositories.Configuration;

namespace SIGEBI.Test.Application
{
    public class LogOpServiceTest
    {
        private readonly ILogOperationFacade _logOpFacade;
        private readonly SIGEBIContext _context;

        public LogOpServiceTest()
        {
            var options = new DbContextOptionsBuilder<SIGEBIContext>()
                .UseInMemoryDatabase("SIGEBI")
                .Options;

            _context = new SIGEBIContext(options);
            var loggerMock = new Mock<ILogger<LogOperationFacade>>();
            var loggerMock1 = new Mock<ILogger<LogOperationValidator>>();
            var loggerMock2 = new Mock<ILogger<LogOperationsRepository>>();
            var repository = new LogOperationsRepository(_context, loggerMock2.Object);
            var validator = new LogOperationValidator(repository, loggerMock1.Object);
            var mapper = new Mock<IMapper>();
            _logOpFacade = new LogOperationFacade(repository, validator, loggerMock.Object);
        }

        [Fact]
        public async Task SaveLogOp_Check_If_Entity_Is_Required()
        {
            // Arrange
            var dtoLogOp = new CreateLogOperationDto
            {
                Entity = "",
                Descripcion = "User logged in",
                TypeOperation = "Login",
                Fecha = DateTime.Now
            };
            // Act
            var result = await _logOpFacade.CreateLogOperationsAsync(dtoLogOp);
            string message = "Entity is required.";
            // Assert
            Assert.IsType<ServiceResult>(result);
            Assert.Equal(message, result.Message);
            Assert.False(result.Success);
        }

        [Fact]
        //Check that Description is required
        public async Task SaveLogOp_Check_If_Description_Is_Required()
        {
            // Arrange
            var dtoLogOp = new CreateLogOperationDto
            {
                Entity = "User",
                Descripcion = "",
                TypeOperation = "Login",
                Fecha = DateTime.Now
            };
            // Act
            var result = await _logOpFacade.CreateLogOperationsAsync(dtoLogOp);
            string message = "Description is required.";
            // Assert
            Assert.IsType<ServiceResult>(result);
            Assert.Equal(message, result.Message);
            Assert.False(result.Success);
        }

        [Fact]
        //Check that TypeOperation is required
        public async Task SaveLogOp_Check_If_TypeOperation_Is_Required()
        {
            // Arrange
            var dtoLogOp = new CreateLogOperationDto
            {
                Entity = "User",
                Descripcion = "User logged in",
                TypeOperation = "",
                Fecha = DateTime.Now
            };
            // Act
            var result = await _logOpFacade.CreateLogOperationsAsync(dtoLogOp);
            string message = "TypeOperation is required.";
            // Assert
            Assert.IsType<ServiceResult>(result);
            Assert.Equal(message, result.Message);
            Assert.False(result.Success);
        }

        [Fact]
        //Check that Fecha cannot be in the future
        public async Task SaveLogOp_Check_If_Fecha_Cannot_Be_In_The_Future()
        {
            // Arrange
            var dtoLogOp = new CreateLogOperationDto
            {
                Entity = "User",
                Descripcion = "User logged in",
                TypeOperation = "Login",
                Fecha = DateTime.Now.AddDays(1) // Future date
            };
            // Act
            var result = await _logOpFacade.CreateLogOperationsAsync(dtoLogOp);
            string message = "Fecha cannot be in the future.";
            // Assert
            Assert.IsType<ServiceResult>(result);
            Assert.Equal(message, result.Message);
            Assert.False(result.Success);

        }
    }
}
