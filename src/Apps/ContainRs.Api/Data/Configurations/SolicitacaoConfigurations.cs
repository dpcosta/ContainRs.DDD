using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContainRs.Api.Data.Configurations;

public class SolicitacaoConfigurations : IEntityTypeConfiguration<PedidoLocacao>
{
    public void Configure(EntityTypeBuilder<PedidoLocacao> builder)
    {
        builder.OwnsOne(s => s.Status, status =>
        {
            status.Property(s => s.Status)
                .HasColumnName("Status")
                .HasConversion<string>();
        });

        builder.OwnsOne(s => s.Localizacao, loc =>
        {
            loc.Property(l => l.CEP)
                .HasColumnName("CEP")
                .IsRequired();

            loc.Property(l => l.Referencias)
                .HasColumnName("Referencias");

            loc.Property(l => l.Latitude)
                .HasColumnName("Latitude")
                .HasColumnType("decimal(18,6)");

            loc.Property(l => l.Longitude)
                .HasColumnName("Longitude")
                .HasColumnType("decimal(18,6)");
        });
    }
}
