namespace ContainRs.Domain.Models;

public class Endereco
{
    public Endereco() { }
    public Guid Id { get; set; }
    public string CEP { get; set; }
    public string Rua { get; set; }
    public string? Numero { get; set; }
    public string? Complemento { get; set; }
    public string? Bairro { get; set; }
    public string Municipio { get; set; }
    public UnidadeFederativa? Estado { get; set; }
    public Guid ClienteId { get; set; }
    public Cliente Cliente { get; set; }
}

public class Cliente
{
    private Cliente() { } // EF Core

    public Cliente(string nome, Email email, string cPF)
    {
        Nome = nome;
        Email = email;
        CPF = cPF;
    }

    public Guid Id { get; set; }
    public string Nome { get; private set; }
    public Email Email { get; private set; }
    public string CPF { get; private set; }
    public string? Celular { get; set; }
    public ICollection<Endereco> Enderecos { get; set; }

    public Endereco AddEndereco(Endereco endereco)
    {
        Enderecos ??= [];
        Enderecos.Add(endereco);
        return endereco;
    }

    public void RemoveEndereco(Endereco endereco)
    {
        Enderecos.Remove(endereco);
    }

    public Endereco AddEndereco(string cep, string rua, string? numero, string? complemento, string? bairro, string municipio, UnidadeFederativa? estado)
    {
        var endereco = new Endereco
        {
            CEP = cep,
            Rua = rua,
            Numero = numero,
            Complemento = complemento,
            Bairro = bairro,
            Municipio = municipio,
            Estado = estado
        };
        return AddEndereco(endereco);
    }
}
