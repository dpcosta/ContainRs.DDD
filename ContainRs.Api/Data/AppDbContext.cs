using ContainRs.Application.Repositories;
using ContainRs.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;

namespace ContainRs.Api.Data;

public class AppDbContext : DbContext, IClienteRepository
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Cliente> Clientes { get; set; }

    public async Task<Cliente> AddAsync(Cliente cliente, CancellationToken cancellationToken = default)
    {
        await Clientes.AddAsync(cliente);
        await SaveChangesAsync(cancellationToken);
        return cliente;
    }

    public async Task<IEnumerable<Cliente>> GetWhereAsync(Expression<Func<Cliente, bool>>? filtro = null, CancellationToken cancellationToken = default)
    {
        IQueryable<Cliente> queryClientes = Clientes;
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
        return await Clientes
            .AsNoTracking()
            .OrderBy(orderBy)
            .FirstOrDefaultAsync(filtro, cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Cliente>()
        .HasKey(c => c.Id);

        modelBuilder.Entity<Cliente>()
            .Property(c => c.Nome).IsRequired();
        modelBuilder.Entity<Cliente>()
            .OwnsOne(c => c.Email, cfg =>
            {
                cfg.Property(e => e.Value)
                    .HasColumnName("Email")
                    .IsRequired();
            });

        modelBuilder.Entity<Cliente>()
            .Property(c => c.Estado)
            .HasConversion<string>();

        modelBuilder.Entity<Cliente>()
            .Property(c => c.CPF).IsRequired();
    }
}
