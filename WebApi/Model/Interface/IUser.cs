namespace Model.Interface
{
    public class IUser

    {
        public int? Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public int? Fk_Login_Id { get; set; }
    }
}
