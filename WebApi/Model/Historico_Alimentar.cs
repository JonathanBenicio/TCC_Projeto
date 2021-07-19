using System;
using System.Collections.Generic;

namespace Model
{
    public class Historico_Alimentar
    {
        public int Id { get; set; }
        public double Glicemia_Obtida { get; set; }
        public double Glicemia_Alvo { get; set; }
        public double Carboidratos_Total { get; set; }
        public double Carboidratos_Insulina { get; set; }
        public double Insulina_Calculada { get; set; }
        public DateTime Data_Hora { get; set; }
        public List<Alimento_Historico> Alimento_Historicos { get; set; }
        public Tipo_Refeicao Tipo_Refeicao { get; set; }
        public int Fk_Tipo_Refeicao_Id { get; set; }
        public Paciente Paciente { get; set; }
        public int Fk_Paciente_Id { get; set; }


    }
}
