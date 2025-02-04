using ContainRs.Application.Repositories;
using ContainRs.Domain.Models;

namespace ContainRs.Application.UseCases;

public class ConsultarClientePeloId
{
    private readonly IClienteRepository repository;

    public ConsultarClientePeloId(Guid id, IClienteRepository repository)
    {
        this.repository = repository;
        Id = id;
    }

    public Guid Id { get; }

    public Task<Cliente?> ExecutarAsync()
    {
        return repository
            .GetFirstAsync(c => c.Id == Id, c => c.Id);
    }

}
