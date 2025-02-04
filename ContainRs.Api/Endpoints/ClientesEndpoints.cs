using ContainRs.Api.Requests;
using ContainRs.Api.Responses;
using ContainRs.Application.Repositories;
using ContainRs.Application.UseCases;
using ContainRs.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContainRs.Api.Endpoints;

public static class ClientesEndpoints
{
    public const string TAG_CLIENTES = "Clientes";
    public const string ENDPOINT_GROUP_ROUTE = "clientes";
    public const string ENDPOINT_NAME_GET_CLIENTE = "GetCliente";
    public static IEndpointRouteBuilder MapClientesEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder
            .MapGroup(ENDPOINT_GROUP_ROUTE)
            //.RequireAuthorization()
            .WithTags(TAG_CLIENTES)
            .WithOpenApi();

        group
            .MapGetClientes()
            .MapGetClienteById()
            .MapPostClientes();

        return builder;
    }

    public static RouteGroupBuilder MapGetClienteById(this RouteGroupBuilder builder)
    {
        builder.MapGet("{id}", async (
            [FromRoute] Guid id
            , [FromServices] IClienteRepository repository) =>
        {
            var useCase = new ConsultarClientePeloId(id, repository);
            var cliente = await useCase.ExecutarAsync();
            if (cliente is null) return Results.NotFound();
            return Results.Ok(new ClienteResponse(cliente.Id.ToString(), cliente.Nome, cliente.Email.Value));
        })
        .WithName(ENDPOINT_NAME_GET_CLIENTE)
        .WithTags(TAG_CLIENTES)
        .Produces<IEnumerable<ClienteResponse>>(StatusCodes.Status200OK);
        return builder;
    }

    public static RouteGroupBuilder MapGetClientes(this RouteGroupBuilder builder)
    {
        builder.MapGet("", async (
            [FromQuery] string? estado
            , [FromServices] IClienteRepository repository) =>
        {
            var useCase = new ConsultarClientes(UfStringConverter.From(estado), repository);
            var clientes = await useCase.ExecutarAsync();
            return Results.Ok(clientes.Select(c => new ClienteResponse(c.Id.ToString(), c.Nome, c.Email.Value)));
        })
        .WithTags(TAG_CLIENTES)
        .Produces<IEnumerable<ClienteResponse>>(StatusCodes.Status200OK);
        return builder;
    }

    public static RouteGroupBuilder MapPostClientes(this RouteGroupBuilder builder)
    {
        builder.MapPost("", async (
            [FromBody] RegistroRequest request
            , [FromServices] IClienteRepository repository) =>
        {
            var useCase = new RegistrarCliente(repository, request.Nome, new Email(request.Email), request.CPF, request.Celular, request.CEP, request.Rua, request.Numero, request.Complemento, request.Bairro, request.Municipio, UfStringConverter.From(request.Estado));
            var cliente = await useCase.ExecutarAsync();
            return Results.CreatedAtRoute(ENDPOINT_NAME_GET_CLIENTE, new { id = cliente.Id }, new ClienteResponse(cliente.Id.ToString(), cliente.Nome, cliente.Email.Value));
        })
        .WithTags(TAG_CLIENTES)
        .Produces<ClienteResponse>(StatusCodes.Status201Created);
        return builder;
    }
}
