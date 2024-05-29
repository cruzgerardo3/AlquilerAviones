using NUnit.Framework;

namespace AlquilerAviones.Tests
{
    [TestFixture]
    public class ClienteTests
    {
        [Test]
        public void TestAlquilarAvion()
        {
            var cliente = new Cliente { Nombre = "Cliente Prueba" };
            var avion = new Avion { Modelo = "Prueba Modelo", Tarifa = 100 };

            cliente.AlquilarAvion(avion);

            Assert.AreEqual(1, cliente.VerAvionesAlquilados().Count);
            Assert.AreEqual("Prueba Modelo", cliente.VerAvionesAlquilados()[0].Modelo);
            Assert.AreEqual(100, cliente.VerAvionesAlquilados()[0].Tarifa);
        }

        [Test]
        public void TestPagarTarifa()
        {
            var cliente = new Cliente { Nombre = "Cliente Prueba" };
            var avion = new Avion { Modelo = "Prueba Modelo", Tarifa = 100 };

            cliente.AlquilarAvion(avion);
            cliente.PagarTarifa(avion);

            Assert.AreEqual(0, cliente.VerAvionesAlquilados().Count);
        }
    }
}
