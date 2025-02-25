using ContainRs.Vendas.Locacoes;

namespace ContainRs.Api.Data.Repositories;

public class LocacaoRepository(AppDbContext context) : BaseRepository<Locacao>(context)
{
}
