using ContainRs.Api.Contracts;
using ContainRs.Api.Requests;
using ContainRs.Api.Responses;
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
            .MapPostClientes()
            .MapPutCliente()
            .MapDeleteCliente();

        return builder;
    }

    public static RouteGroupBuilder MapGetClienteById(this RouteGroupBuilder builder)
    {
        builder.MapGet("{id}", async (
            [FromRoute] Guid id
            , [FromServices] IRepository<Cliente> repository) =>
        {
            var cliente = await repository
                .GetFirstAsync(
                    c => c.Id == id, 
                    c => c.Id);
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
            , [FromServices] IRepository<Cliente> repository) =>
        {
            var clientes = Enumerable.Empty<Cliente>();
            if (estado is not null)
            {
                clientes = await repository
                    .GetWhereAsync(c => c.Estado == UfStringConverter.From(estado));
            } else
            {
                clientes = await repository.GetWhereAsync();
            }
            
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
            , [FromServices] IRepository<Cliente> repository) =>
        {
            var cliente = new Cliente(request.Nome, new Email(request.Email), request.CPF)
            {
                Celular = request.Celular,
                CEP = request.CEP,
                Rua = request.Rua,
                Numero = request.Numero,
                Complemento = request.Complemento,
                Bairro = request.Bairro,
                Municipio = request.Municipio,
                Estado = UfStringConverter.From(request.Estado)
            };
            await repository.AddAsync(cliente);

            return Results.CreatedAtRoute(ENDPOINT_NAME_GET_CLIENTE, new { id = cliente.Id }, new ClienteResponse(cliente.Id.ToString(), cliente.Nome, cliente.Email.Value));
        })
        .WithTags(TAG_CLIENTES)
        .Produces<ClienteResponse>(StatusCodes.Status201Created);
        return builder;
    }
    public static RouteGroupBuilder MapPutCliente(this RouteGroupBuilder builder)
    {
        builder.MapPut("{id}", async (
            [FromRoute] Guid id,
            [FromBody] RegistroRequest request,
            [FromServices] IRepository<Cliente> repository,
            CancellationToken cancellationToken) =>
        {
            var clienteExistente = await repository.GetFirstAsync(c => c.Id == id, c => c.Id);
            if (clienteExistente is null) return Results.NotFound();

            //clienteExistente.Nome = request.Nome;
            //clienteExistente.Email = new Email(request.Email);
            //clienteExistente.CPF = request.CPF;
            clienteExistente.Celular = request.Celular;
            clienteExistente.CEP = request.CEP;
            clienteExistente.Rua = request.Rua;
            clienteExistente.Numero = request.Numero;
            clienteExistente.Complemento = request.Complemento;
            clienteExistente.Bairro = request.Bairro;
            clienteExistente.Municipio = request.Municipio;
            clienteExistente.Estado = UfStringConverter.From(request.Estado);

            await repository.UpdateAsync(clienteExistente, cancellationToken);

            return Results.Ok(new ClienteResponse(clienteExistente.Id.ToString(), clienteExistente.Nome, clienteExistente.Email.Value));
        })
        .WithTags(TAG_CLIENTES)
        .Produces<ClienteResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
        return builder;
    }

    public static RouteGroupBuilder MapDeleteCliente(this RouteGroupBuilder builder)
    {
        builder.MapDelete("{id}", async (
            [FromRoute] Guid id,
            [FromServices] IRepository<Cliente> repository,
            CancellationToken cancellationToken) =>
        {
            var clienteExistente = await repository.GetFirstAsync(c => c.Id == id, c => c.Id);
            if (clienteExistente is null) return Results.NotFound();

            await repository.RemoveAsync(clienteExistente, cancellationToken);

            return Results.NoContent();
        })
        .WithTags(TAG_CLIENTES)
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound);
        return builder;
    }

    

}
