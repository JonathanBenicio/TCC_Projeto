using System;
using System.Collections.Generic;

namespace Model
{
    public class Alimento
    {
        public int Id { get; set; }
        public String Nome { get; set; }
        public String Marca { get; set; }
        public String Porcao_Tipo { get; set; }
        public double Porcao_Quantidade { get; set; }
        public double Porcao_Carboidratos { get; set; }
        public string Tipo { get; set; }
        public string Foto { get; set; }

        public List<Alimento_Historico> Alimento_Historicos { get; set; }
    }
}
