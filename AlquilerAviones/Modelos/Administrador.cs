public class Administrador : Usuario, IGestionUsuarios
{
    private List<Vendedor> vendedores = new List<Vendedor>();
    private List<Cliente> clientes = new List<Cliente>();

    public void AgregarVendedor(Vendedor vendedor)
    {
        vendedores.Add(vendedor);
    }

    public void AgregarCliente(Cliente cliente)
    {
        clientes.Add(cliente);
    }

    public List<Vendedor> VerVendedores()
    {
        return vendedores;
    }

    public List<Cliente> VerClientes()
    {
        return clientes;
    }
}
