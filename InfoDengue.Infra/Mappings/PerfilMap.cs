using InfoDengue.Infra.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoDengue.Infra.Mappings
{
    public class PerfilMap : IEntityTypeConfiguration<Perfil>
    {
        public void Configure(EntityTypeBuilder<Perfil> builder)
        {
            // Nome da tabela
            builder.ToTable("PERFIL");

            // Chave primária
            builder.HasKey(p => p.IdPerfil);

            // Mapeamento dos campos da entidade
            builder.Property(p => p.IdPerfil)
                .HasColumnName("IDPERFIL")
                .IsRequired();

            builder.Property(p => p.Nome)
                .HasColumnName("NOME")
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(p => p.Descricao)
                .HasColumnName("DESCRICAO")
                .HasMaxLength(255)
                .IsRequired();


            #region Mapeamento de campos únicos

            builder.HasIndex(p => p.Nome)
                .IsUnique();

            #endregion
        }
    }
}