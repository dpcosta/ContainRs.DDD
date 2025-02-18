using ContainRs.Api.Domain;

namespace ContainRs.Api.Data.Repositories;

public class SolicitacaoRepository(AppDbContext dbContext) 
    : BaseRepository<Solicitacao>(dbContext)
{
    // métodos sobrescritos e específicos vão aqui
}
