using ContainRs.Api.Contracts;
using ContainRs.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ContainRs.Api.Data.Repositories;

public class ClienteRepository(AppDbContext dbContext) : IRepository<Cliente>
{
    public async Task<Cliente> AddAsync(Cliente cliente, CancellationToken cancellationToken = default)
    {
        await dbContext.Clientes.AddAsync(cliente, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return cliente;
    }

    public async Task<IEnumerable<Cliente>> GetWhereAsync(Expression<Func<Cliente, bool>>? filtro = null, CancellationToken cancellationToken = default)
    {
        IQueryable<Cliente> queryClientes = dbContext.Clientes;
        if (filtro != null)
        {
            queryClientes = queryClientes.Where(filtro);
        }
        return await queryClientes
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Cliente?> GetFirstAsync<TProperty>(Expression<Func<Cliente, bool>> filtro, Expression<Func<Cliente, TProperty>> orderBy, CancellationToken cancellationToken = default)
    {
        return await dbContext.Clientes
            .AsNoTracking()
            .OrderBy(orderBy)
            .FirstOrDefaultAsync(filtro, cancellationToken);
    }

    public Task RemoveAsync(Cliente cliente, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Cliente> UpdateAsync(Cliente cliente, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
