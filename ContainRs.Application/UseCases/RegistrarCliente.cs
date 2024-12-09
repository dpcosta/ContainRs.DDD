using ContainRs.WebApp.Data;
using ContainRs.Domain.Models;

namespace ContainRs.Application.UseCases;

public class RegistrarCliente
{
    private readonly AppDbContext context;
    public RegistrarCliente(AppDbContext context, string nome, Email email, string cPF, string? celular, string? cEP, string? rua, string? numero, string? complemento, string? bairro, string? municipio, string? estado)
    {
        this.context = context;
        Nome = nome;
        Email = email;
        CPF = cPF;
        Celular = celular;
        CEP = cEP;
        Rua = rua;
        Numero = numero;
        Complemento = complemento;
        Bairro = bairro;
        Municipio = municipio;
        Estado = estado;
    }

    public string Nome { get; }
    public Email Email { get; }
    public string CPF { get; }
    public string? Celular { get; }
    public string? CEP { get; }
    public string? Rua { get; }
    public string? Numero { get; }
    public string? Complemento { get; }
    public string? Bairro { get; }
    public string? Municipio { get; }
    public string? Estado { get; }

    public async Task<Cliente> ExecutarAsync()
    {
        var cliente = new Cliente(Nome, Email, CPF)
        {
            Celular = Celular,
            CEP = CEP,
            Rua = Rua,
            Numero = Numero,
            Complemento = Complemento,
            Bairro = Bairro,
            Municipio = Municipio,
            Estado = Estado
        };
        context.Clientes.Add(cliente);
        await context.SaveChangesAsync();

        return cliente;
    }
}
