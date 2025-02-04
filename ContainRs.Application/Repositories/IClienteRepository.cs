using ContainRs.Domain.Models;
using System.Linq.Expressions;

namespace ContainRs.Application.Repositories;

public interface IClienteRepository
{
    Task<Cliente> AddAsync(
        Cliente cliente
        , CancellationToken cancellationToken = default);

    Task<Cliente?> GetFirstAsync<TProperty>(
        Expression<Func<Cliente, bool>> filtro
        , Expression<Func<Cliente, TProperty>> orderBy
        , CancellationToken cancellationToken = default);

    Task<IEnumerable<Cliente>> GetWhereAsync(
        Expression<Func<Cliente, bool>>? filtro = default
        , CancellationToken cancellationToken = default);
}
