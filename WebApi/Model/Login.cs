namespace Model
{
    public class Login
    {
        public int? Id { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        public Nutricionista Nutricionista { get; set; }

        public Paciente Paciente { get; set; }

    }
}
