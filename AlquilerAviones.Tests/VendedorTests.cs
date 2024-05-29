using NUnit.Framework;

namespace AlquilerAviones.Tests
{
    [TestFixture]
    public class VendedorTests
    {
        [Test]
        public void TestAgregarAvion()
        {
            var vendedor = new Vendedor { Nombre = "Vendedor Prueba" };
            var avion = new Avion { Modelo = "Prueba Modelo", Tarifa = 100 };

            vendedor.AgregarAvion(avion);

            Assert.AreEqual(1, vendedor.VerAviones().Count);
            Assert.AreEqual("Prueba Modelo", vendedor.VerAviones()[0].Modelo);
            Assert.AreEqual(100, vendedor.VerAviones()[0].Tarifa);
        }
    }
}
