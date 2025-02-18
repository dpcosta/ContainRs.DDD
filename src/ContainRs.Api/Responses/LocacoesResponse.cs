using ContainRs.Api.Domain;

namespace ContainRs.Api.Responses;

public record SolicitacaoResponse(string Id, string Status, string Finalidade)
{
    public static SolicitacaoResponse From(Solicitacao solicitacao) => new(
        Id: solicitacao.Id.ToString(),
        Status: solicitacao.Status.ToString(),
        Finalidade: solicitacao.Finalidade
    );
}
