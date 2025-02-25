using ContainRs.Api.Domain;

namespace ContainRs.Api.Propostas;

public record SolicitacaoResponse(string Id, string Status, string Finalidade)
{
    public static SolicitacaoResponse From(Solicitacao solicitacao) => new(
        Id: solicitacao.Id.ToString(),
        Status: solicitacao.Status.ToString(),
        Finalidade: solicitacao.Finalidade
    );
}

public record PropostaResponse(string Id, string Status, decimal Valor, DateTime DataExpiracao)
{
    public static PropostaResponse From(Proposta proposta) => new(
        Id: proposta.Id.ToString(),
        Status: proposta.Status.ToString(),
        Valor: proposta.ValorTotal,
        DataExpiracao: proposta.DataExpiracao
    );
}
