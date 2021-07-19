using Microsoft.EntityFrameworkCore;
using Model;

namespace WebApi.Data
{
    public class WebApiContextMySQL : DbContext
    {
        public WebApiContextMySQL(DbContextOptions<WebApiContextMySQL> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Alimento_Historico>()
            .HasOne(x => x.Historico_Alimentar)
            .WithMany(x => x.Alimento_Historicos)
            .HasForeignKey(x => x.Fk_Historico_Alimentar_Id);

            modelBuilder.Entity<Alimento_Historico>()
            .HasOne(x => x.Alimento)
            .WithMany(x => x.Alimento_Historicos)
            .HasForeignKey(x => x.Fk_Alimento_Id);

            modelBuilder.Entity<Historico_Alimentar>()
            .HasOne(x => x.Tipo_Refeicao)
            .WithOne(x => x.Historico_Alimentar)
            .HasForeignKey<Historico_Alimentar>(x => x.Fk_Tipo_Refeicao_Id);

            modelBuilder.Entity<Historico_Alimentar>()
            .HasOne(x => x.Paciente)
            .WithMany(x => x.Historico_Alimentars)
            .HasForeignKey(x => x.Fk_Paciente_Id);

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

            modelBuilder.Entity<Tipo_Refeicao>()
                .HasOne(x => x.Paciente)
                .WithMany(x => x.Tipo_Refeicaos)
                .HasForeignKey(x => x.Fk_Paciente_Id);



        }


        //public DbSet<Paciente> Pacientes { get; set; }

        //public DbSet<Nutricionista> Nutricionistas { get; set; }

        public DbSet<Login> Logins { get; set; }
        public DbSet<Alimento> Alimentos { get; set; }
        public DbSet<Alimento_Historico> Alimento_Historicos { get; set; }

        //public DbSet<Paciente> Pacientes { get; set; }

        //public DbSet<Nutricionista> Nutricionistas { get; set; }

        public DbSet<Nutricionista> Nutricionistas { get; set; }

        //public DbSet<Paciente> Pacientes { get; set; }

        //public DbSet<Nutricionista> Nutricionistas { get; set; }

        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Historico_Alimentar> Historico_Alimentars { get; set; }
        public DbSet<Tipo_Refeicao> Tipo_Refeicaos { get; set; }

    }

}
