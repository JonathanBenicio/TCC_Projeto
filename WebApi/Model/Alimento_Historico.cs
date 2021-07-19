namespace Model
{
    public class Alimento_Historico
    {
        public int Id { get; set; }
        public double Quantidade { get; set; }
        public double Carboidratos_Total { get; set; }

        public int Fk_Alimento_Id { get; set; }
        public Alimento Alimento { get; set; }
        public int Fk_Historico_Alimentar_Id { get; set; }

        public Historico_Alimentar Historico_Alimentar { get; set; }
    }
}
