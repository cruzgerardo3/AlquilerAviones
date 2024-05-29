using NUnit.Framework;
using System;

namespace AlquilerAviones.Tests
{
    [TestFixture]
    public class SistemaAlquilerTests
    {
        private SistemaAlquiler sistema;

        [SetUp]
        public void Setup()
        {
            sistema = new SistemaAlquiler();
        }

        [Test]
        public void TestLoginConCredencialesValidas()
        {
            var usuario = sistema.AutenticarUsuario("admin@aviones.com", "admin123");
            Assert.IsNotNull(usuario);
            Assert.IsInstanceOf<Administrador>(usuario);
        }

        [Test]
        public void TestLoginConCredencialesInvalidas()
        {
            Assert.Throws<CredencialesInvalidasException>(() => sistema.AutenticarUsuario("invalido@correo.com", "123"));
        }
    }
}
