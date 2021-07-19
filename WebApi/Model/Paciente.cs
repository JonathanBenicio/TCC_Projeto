using Model.Interface;
using System.Collections.Generic;

namespace Model
{
    public class Paciente : IUser

    {
        public double Fator_Sensibilidade { get; set; }
        public Diabetes Tipo_Diabetes { get; set; }
        public Login Login { get; set; }
        public int? Fk_Nutricionista_Id { get; set; }

        public Nutricionista Nutricionista { get; set; }

        public List<Historico_Alimentar> Historico_Alimentars { get; set; }
        public List<Tipo_Refeicao> Tipo_Refeicaos { get; set; }
    }
}
