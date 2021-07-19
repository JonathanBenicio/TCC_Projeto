using System;

namespace Model
{
    public class Tipo_Refeicao
    {
        public int? Id { get; set; }
        public string Nome { get; set; }
        public TimeSpan Horario { get; set; }
        public double Glicemia_Alvo { get; set; }
        public Paciente Paciente { get; set; }
        public int Fk_Paciente_Id { get; set; }

        public Historico_Alimentar Historico_Alimentar { get; set; }
    }
}
