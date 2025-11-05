using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGEBI.Application.Interfaces;

namespace SIGEBI.Test.Application
{
    public class BibliotecarioServicesTest
    {
        private readonly IBibliotecarioService _bibliotecarioServices;

        [Fact]
        public async Task SaveBiblio_Check_If_CedulaExist()
        {
            // Arrange
            string cedula = "023 - 1121334 - 5";

            // Act

            // Assert

        }
    }
}
