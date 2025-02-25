using ContainRs.Domain.Models;

namespace ContainRs.Api.Domain;

public record StatusPedido(string Status)
{
    public static StatusPedido Ativo => new("Ativo");
    public static StatusPedido Inativo => new("Inativo");
    public static StatusPedido Cancelado => new("Cancelado");

    public override string ToString() => Status;
    public static StatusPedido? Parse(string status)
    {
        return status switch
        {
            "Ativo" => Ativo,
            "Inativo" => Inativo,
            "Cancelado" => Cancelado,
            _ => null
        };
    }
}

/// <summary>
/// Pedido formal realizado por um cliente interessado na locação de um contêiner. A solicitação pode incluir informações sobre finalidade, localização, quantidade e período desejado
/// </summary>
public class PedidoLocacao
{
    public PedidoLocacao() { }

    public Guid Id { get; set; }
    public Guid ClienteId { get; set; }
    public string Descricao { get; set; }
    public int QuantidadeEstimada { get; set; }
    public StatusPedido Status { get; set; } = StatusPedido.Ativo;
    public string Finalidade { get; set; }
    public DateTime DataInicioOperacao { get; set; }
    public int DisponibilidadePrevia { get; set; }
    public int DuracaoPrevistaLocacao { get; set; }
    public Guid EnderecoId { get; set; }
    public Endereco Localizacao { get; set; }

    public ICollection<Proposta> Propostas { get; } = [];

    public Proposta AddProposta(Proposta proposta)
    {
        Propostas.Add(proposta);
        return proposta;
    }

    public void RemoveProposta(Proposta proposta)
    {
        Propostas.Remove(proposta);
    }

}