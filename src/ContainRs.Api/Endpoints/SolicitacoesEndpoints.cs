using ContainRs.Api.Contracts;
using ContainRs.Api.Domain;
using ContainRs.Api.Requests;
using ContainRs.Api.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ContainRs.Api.Endpoints;

public static class SolicitacoesEndpoints
{
    public const string ENDPOINT_NAME_GET_SOLICITACAO = "GetSolicitacao";
    public static IEndpointRouteBuilder MapSolicitacoesEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder
            .MapGroup(EndpointConstants.ROUTE_SOLICITACOES)
            .RequireAuthorization(policy => policy.RequireRole("Cliente"))
            .WithTags(EndpointConstants.TAG_LOCACAO)
            .WithOpenApi();

        group
            .MapPostSolicitacao()
            .MapGetSolicitacoes()
            .MapGetSolicitacaoById();

        return builder;
    }

    public static RouteGroupBuilder MapGetSolicitacaoById(this RouteGroupBuilder builder)
    {
        builder.MapGet("{id}", async (
            [FromRoute] Guid id
            , [FromServices] IRepository<Solicitacao> repository) =>
        {
            var solicitacao = await repository
                .GetFirstAsync(
                    s => s.Id == id,
                    s => s.Id);
            if (solicitacao is null) return Results.NotFound();
            return Results.Ok(SolicitacaoResponse.From(solicitacao));
        })
        .WithName(ENDPOINT_NAME_GET_SOLICITACAO)
        .Produces<SolicitacaoResponse>(StatusCodes.Status200OK);
        return builder;
    }

    public static RouteGroupBuilder MapGetSolicitacoes(this RouteGroupBuilder builder)
    {
        builder.MapGet("", async (
            HttpContext context,
            [FromServices] IRepository<Solicitacao> repository) =>
        {
            var clienteId = context.User.Claims
                .Where(c => c.Type.Equals("ClienteId"))
                .Select(c => c.Value)
                .FirstOrDefault();
            if (clienteId is null) return Results.Unauthorized();

            var solicitacoes = await repository.GetWhereAsync(s => s.ClienteId.ToString() == clienteId && s.Status.Status.Equals("Ativa"));
            return Results.Ok(solicitacoes.Select(SolicitacaoResponse.From));
        })
        .Produces(StatusCodes.Status401Unauthorized)
        .Produces<IEnumerable<SolicitacaoResponse>>(StatusCodes.Status200OK);
        return builder;
    }

    public static RouteGroupBuilder MapPostSolicitacao(this RouteGroupBuilder builder)
    {
        builder.MapPost("", async (
            [FromBody] SolicitacaoRequest request
            , HttpContext context
            , [FromServices] IRepository<Solicitacao> repository) =>
        {
            var clienteId = context.User.Claims
                .Where(c => c.Type.Equals("ClienteId"))
                .Select(c => c.Value)
                .FirstOrDefault();
            if (clienteId is null) return Results.Unauthorized();

            var solicitacao = new Solicitacao
            {
                ClienteId = Guid.Parse(clienteId),
                Descricao = request.Descricao,
                QuantidadeEstimada = request.QuantidadeEstimada,
                Finalidade = request.Finalidade,
                DataInicioOperacao = request.Periodo.DataInicioOperacao,
                DisponibilidadePrevia = request.Periodo.DisponibilidadePrevia,
                DuracaoPrevistaLocacao = request.Periodo.QuantidadeDias
            };
            if (request.Localizacao.EnderecoId.HasValue)
            {
                solicitacao.EnderecoId = request.Localizacao.EnderecoId.Value;
            }

            await repository.AddAsync(solicitacao);
            
            return Results.CreatedAtRoute(ENDPOINT_NAME_GET_SOLICITACAO, new { id = solicitacao.Id }, SolicitacaoResponse.From(solicitacao));
        })
        .Produces(StatusCodes.Status401Unauthorized)
        .Produces<SolicitacaoResponse>(StatusCodes.Status201Created);
        return builder;
    }
}
