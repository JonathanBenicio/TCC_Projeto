using Model.Interface;
using System.Collections.Generic;

namespace Model
{
    public class Nutricionista : IUser
    {

        public string Endereco { get; set; }

        public Login Login { get; set; }

        public List<Paciente> Pacientes { get; set; }

    }
}
