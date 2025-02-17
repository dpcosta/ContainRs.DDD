namespace ContainRs.Api.Responses;

public record ClienteResponse(string Id, string Nome, string Email, string? Celular, IEnumerable<EnderecoResponse>? Enderecos = null);
public record EnderecoResponse(string Id, string Logradouro, string? Numero, string? Complemento, string? Bairro, string Cidade, string? Estado, string CEP);
public record RegistrationStatusResponse(string ClienteId, string Email, string Status);
