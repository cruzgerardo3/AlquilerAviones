public class Vendedor : Usuario, IGestionAviones
{
    public List<Avion> Aviones { get; set; } = new List<Avion>();

    public void AgregarAvion(Avion avion)
    {
        Aviones.Add(avion);
    }

    public List<Avion> VerAviones()
    {
        return Aviones;
    }
}
