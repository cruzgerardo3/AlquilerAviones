using System;
using System.Collections.Generic;
using Spectre.Console;

namespace AlquilerAviones
{
    public class SistemaAlquiler
    {
        private List<Usuario> usuarios = new List<Usuario>();

        public SistemaAlquiler()
        {
            
            var admin = new Administrador { Email = "admin@aviones.com", Contrasena = "admin123", Nombre = "Admin" };
            var vendedor1 = new Vendedor { Email = "vendedor1@aviones.com", Contrasena = "vende123", Nombre = "Vendedor 1" };
            var vendedor2 = new Vendedor { Email = "vendedor2@aviones.com", Contrasena = "vende123", Nombre = "Vendedor 2" };
            var cliente1 = new Cliente { Email = "cliente1@aviones.com", Contrasena = "cliente123", Nombre = "Cliente 1" };
            var cliente2 = new Cliente { Email = "cliente2@aviones.com", Contrasena = "cliente123", Nombre = "Cliente 2" };

            vendedor1.AgregarAvion(new Avion { Modelo = "Cessna 172", Tarifa = 150 });
            vendedor1.AgregarAvion(new Avion { Modelo = "Piper PA-28", Tarifa = 180 });
            vendedor2.AgregarAvion(new Avion { Modelo = "Beechcraft Bonanza", Tarifa = 200 });
            vendedor2.AgregarAvion(new Avion { Modelo = "Cirrus SR22", Tarifa = 250 });

            admin.AgregarVendedor(vendedor1);
            admin.AgregarVendedor(vendedor2);
            admin.AgregarCliente(cliente1);
            admin.AgregarCliente(cliente2);

            usuarios.Add(admin);
            usuarios.Add(vendedor1);
            usuarios.Add(vendedor2);
            usuarios.Add(cliente1);
            usuarios.Add(cliente2);
        }

        public void Login()
        {
            AnsiConsole.MarkupLine("[bold yellow]Bienvenido al Sistema de Alquiler de Aviones[/]");
            while (true)
            {
                string email = AnsiConsole.Ask<string>("Ingresa tu [green]email[/]:");
                string contrasena = AnsiConsole.Prompt(
                    new TextPrompt<string>("Ingresa tu [green]contraseña[/]:")
                        .PromptStyle("red")
                        .Secret());

                try
                {
                    Usuario usuario = AutenticarUsuario(email, contrasena);
                    MostrarMenu(usuario);
                }
                catch (Exception ex)
                {
                    AnsiConsole.MarkupLine($"[bold red]Error:[/] {ex.Message}");
                }
            }
        }

        public Usuario AutenticarUsuario(string email, string contrasena)
        {
            foreach (var usuario in usuarios)
            {
                if (usuario.Email == email && usuario.Contrasena == contrasena)
                {
                    return usuario;
                }
            }
            throw new CredencialesInvalidasException("Email o contraseña incorrectos.");
        }

        private void MostrarMenu(Usuario usuario)
        {
            if (usuario is Administrador admin)
            {
                MostrarMenuAdministrador(admin);
            }
            else if (usuario is Vendedor vendedor)
            {
                MostrarMenuVendedor(vendedor);
            }
            else if (usuario is Cliente cliente)
            {
                MostrarMenuCliente(cliente);
            }
        }

        private void MostrarMenuAdministrador(Administrador admin)
        {
            while (true)
            {
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[green]¿Qué deseas hacer?[/]")
                        .AddChoices(new[] { "Ver Vendedores", "Ver Clientes", "Agregar Vendedor", "Agregar Cliente", "Cerrar Sesión" }));

                switch (choice)
                {
                    case "Ver Vendedores":
                        var vendedores = admin.VerVendedores();
                        foreach (var vendedor in vendedores)
                        {
                            AnsiConsole.MarkupLine($"[blue]{vendedor.Nombre}[/] - [green]{vendedor.Email}[/]");
                            foreach (var avion in vendedor.Aviones)
                            {
                                AnsiConsole.MarkupLine($"  - [yellow]{avion.Modelo}[/]: [red]{avion.Tarifa}[/] $");
                            }
                        }
                        break;
                    case "Ver Clientes":
                        var clientes = admin.VerClientes();
                        foreach (var cliente in clientes)
                        {
                            AnsiConsole.MarkupLine($"[blue]{cliente.Nombre}[/] - [green]{cliente.Email}[/]");
                        }
                        break;
                    case "Agregar Vendedor":
                        var nuevoVendedor = new Vendedor
                        {
                            Nombre = AnsiConsole.Ask<string>("Ingresa el [green]nombre[/] del vendedor:"),
                            Email = AnsiConsole.Ask<string>("Ingresa el [green]email[/] del vendedor:"),
                            Contrasena = AnsiConsole.Prompt(
                                new TextPrompt<string>("Ingresa la [green]contraseña[/] del vendedor:")
                                    .PromptStyle("red")
                                    .Secret())
                        };
                        admin.AgregarVendedor(nuevoVendedor);
                        usuarios.Add(nuevoVendedor);
                        AnsiConsole.MarkupLine("[bold green]Vendedor agregado exitosamente![/]");
                        break;
                    case "Agregar Cliente":
                        var nuevoCliente = new Cliente
                        {
                            Nombre = AnsiConsole.Ask<string>("Ingresa el [green]nombre[/] del cliente:"),
                            Email = AnsiConsole.Ask<string>("Ingresa el [green]email[/] del cliente:"),
                            Contrasena = AnsiConsole.Prompt(
                                new TextPrompt<string>("Ingresa la [green]contraseña[/] del cliente:")
                                    .PromptStyle("red")
                                    .Secret())
                        };
                        admin.AgregarCliente(nuevoCliente);
                        usuarios.Add(nuevoCliente);
                        AnsiConsole.MarkupLine("[bold green]Cliente agregado exitosamente![/]");
                        break;
                    case "Cerrar Sesión":
                        return;
                }
            }
        }
        private void MostrarMenuVendedor(Vendedor vendedor)
        {
            while (true)
            {
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[green]¿Qué deseas hacer?[/]")
                        .AddChoices(new[] { "Ver Aviones", "Agregar Avion", "Cerrar Sesión" }));

                switch (choice)
                {
                    case "Ver Aviones":
                        var aviones = vendedor.VerAviones();
                        foreach (var avion in aviones)
                        {
                            AnsiConsole.MarkupLine($"[yellow]{avion.Modelo}[/] - [red]{avion.Tarifa}[/] $");
                        }
                        break;
                    case "Agregar Avion":
                        string modelo = AnsiConsole.Ask<string>("Ingresa el [green]modelo[/] del avión:");
                        decimal tarifa = AnsiConsole.Ask<decimal>("Ingresa la [green]tarifa[/] del avión:");
                        vendedor.AgregarAvion(new Avion { Modelo = modelo, Tarifa = tarifa });
                        AnsiConsole.MarkupLine("[bold green]Avión agregado exitosamente![/]");
                        break;
                    case "Cerrar Sesión":
                        return;
                }
            }
        }

        private void MostrarMenuCliente(Cliente cliente)
        {
            while (true)
            {
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[green]¿Qué deseas hacer?[/]")
                        .AddChoices(new[] { "Ver Aviones Alquilados", "Alquilar Avion", "Pagar Tarifa", "Cerrar Sesión" }));

                switch (choice)
                {
                    case "Ver Aviones Alquilados":
                        var avionesAlquilados = cliente.VerAvionesAlquilados();
                        foreach (var avion in avionesAlquilados)
                        {
                            AnsiConsole.MarkupLine($"[yellow]{avion.Modelo}[/] - [red]{avion.Tarifa}[/] $");
                        }
                        break;
                    case "Alquilar Avion":
                        var vendedores = ((Administrador)usuarios.Find(u => u is Administrador)).VerVendedores();
                        var listaAviones = new List<Avion>();
                        foreach (var vendedor in vendedores)
                        {
                            listaAviones.AddRange(vendedor.VerAviones());
                        }

                        if (listaAviones.Count == 0)
                        {
                            AnsiConsole.MarkupLine("[bold red]No hay aviones disponibles para alquilar.[/]");
                        }
                        else
                        {
                            var avionSeleccionado = AnsiConsole.Prompt(
                                new SelectionPrompt<Avion>()
                                    .Title("Selecciona un [green]avión[/] para alquilar")
                                    .AddChoices(listaAviones)
                                    .UseConverter(a => $"{a.Modelo} - ${a.Tarifa}"));

                            cliente.AlquilarAvion(avionSeleccionado);
                            AnsiConsole.MarkupLine("[bold green]Avión alquilado exitosamente![/]");
                        }
                        break;
                    case "Pagar Tarifa":
                        if (cliente.AvionesAlquilados.Count == 0)
                        {
                            AnsiConsole.MarkupLine("[bold red]No tienes aviones alquilados para pagar.[/]");
                        }
                        else
                        {
                            var avionAPagar = AnsiConsole.Prompt(
                                new SelectionPrompt<Avion>()
                                    .Title("Selecciona un [green]avión[/] para pagar la tarifa")
                                    .AddChoices(cliente.AvionesAlquilados)
                                    .UseConverter(a => $"{a.Modelo} - ${a.Tarifa}"));

                            cliente.PagarTarifa(avionAPagar);
                            AnsiConsole.MarkupLine("[bold green]Tarifa pagada exitosamente![/]");
                        }
                        break;
                    case "Cerrar Sesión":
                        return;
                }
            }
        }
    }
}

