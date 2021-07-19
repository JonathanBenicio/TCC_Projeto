using Microsoft.EntityFrameworkCore;
using Model;

namespace WebApi.Data
{
    public class WebApiContext : DbContext
    {






        public WebApiContext(DbContextOptions<WebApiContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Alimento_Historico>()
            .HasOne(p => p.Historico_Alimentar)
            .WithMany(b => b.Alimento_Historicos)
            .HasForeignKey(p => p.Fk_Historico_Alimentar_Id);

            modelBuilder.Entity<Historico_Alimentar>()
            .HasOne(x => x.Tipo_Refeicao)
            .WithOne(x => x.Historico_Alimentar)
            .HasForeignKey<Historico_Alimentar>(x => x.Fk_Tipo_Refeicao_Id);

            modelBuilder.Entity<Nutricionista>()
            .HasOne(x => x.Login)
            .WithOne(x => x.Nutricionista)
            .HasForeignKey<Nutricionista>(x => x.Fk_Login_Id);

            modelBuilder.Entity<Paciente>()
            .HasOne(x => x.Login)
            .WithOne(x => x.Paciente)
            .HasForeignKey<Paciente>(x => x.Fk_Login_Id);

            modelBuilder.Entity<Paciente>()
            .HasOne(x => x.Nutricionista)
            .WithMany(x => x.Pacientes)
            .HasForeignKey(x => x.Fk_Nutricionista_Id);



        }


        //public DbSet<Paciente> Pacientes { get; set; }

        //public DbSet<Nutricionista> Nutricionistas { get; set; }

        public DbSet<Login> Login { get; set; }

        //public DbSet<Paciente> Pacientes { get; set; }

        //public DbSet<Nutricionista> Nutricionistas { get; set; }

        public DbSet<Nutricionista> Nutricionista { get; set; }

        //public DbSet<Paciente> Pacientes { get; set; }

        //public DbSet<Nutricionista> Nutricionistas { get; set; }

        public DbSet<Paciente> Paciente { get; set; }
        public DbSet<Historico_Alimentar> Historico_Alimentar { get; set; }
        public DbSet<Tipo_Refeicao> Tipo_Refeicao { get; set; }
        public DbSet<Model.Alimento_Historico> Alimento_Historico { get; set; }

    }

}
