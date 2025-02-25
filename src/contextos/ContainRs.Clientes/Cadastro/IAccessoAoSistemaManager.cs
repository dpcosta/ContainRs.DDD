namespace ContainRs.Clientes.Cadastro;

public interface IAccessoAoSistemaManager
{
    Task AdicionarClienteAsync(string email, CancellationToken cancellationToken);
    Task RemoveClienteAsync(string email, CancellationToken cancellationToken);
    Task<bool?> PossuiAcessoAsync(string email, CancellationToken cancellationToken);
    Task RejeitaAcessoAsync(string email, CancellationToken cancellationToken);
}
