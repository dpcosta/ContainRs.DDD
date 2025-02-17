using ContainRs.Domain.Models;
using System.Linq.Expressions;

namespace ContainRs.Api.Contracts;

public interface IRepository<T>
{
    Task<T> AddAsync(
        T cliente
        , CancellationToken cancellationToken = default);

    Task<T> UpdateAsync(
        T cliente
        , CancellationToken cancellationToken = default);

    Task RemoveAsync(
        T cliente
        , CancellationToken cancellationToken = default);

    Task<T?> GetFirstAsync<TProperty>(
        Expression<Func<T, bool>> filtro
        , Expression<Func<T, TProperty>> orderBy
        , CancellationToken cancellationToken = default);

    Task<IEnumerable<T>> GetWhereAsync(
        Expression<Func<T, bool>>? filtro = default
        , CancellationToken cancellationToken = default);
}
