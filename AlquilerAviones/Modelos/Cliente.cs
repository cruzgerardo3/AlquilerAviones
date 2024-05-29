public class Cliente : Usuario
{
    public List<Avion> AvionesAlquilados { get; set; } = new List<Avion>();

    public void AlquilarAvion(Avion avion)
    {
        AvionesAlquilados.Add(avion);
    }

    public void PagarTarifa(Avion avion)
    {
        AvionesAlquilados.Remove(avion);
    }

    public List<Avion> VerAvionesAlquilados()
    {
        return AvionesAlquilados;
    }
}
