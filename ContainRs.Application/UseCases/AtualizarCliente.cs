using ContainRs.Application.Repositories;
using ContainRs.Domain.Models;

namespace ContainRs.Application.UseCases;

public class AtualizarCliente
{
    private readonly IClienteRepository repository;

    public AtualizarCliente(IClienteRepository repository, Guid id, string nome, Email email, string cPF, string? celular, string? cEP, string? rua, string? numero, string? complemento, string? bairro, string? municipio, UnidadeFederativa? estado)
    {
        this.repository = repository;
        Id = id;
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

    public Guid Id { get; }
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
    public UnidadeFederativa? Estado { get; }
}
