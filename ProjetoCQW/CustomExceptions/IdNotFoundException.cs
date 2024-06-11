namespace ProjetoCQW.CustomExceptions
{
    public class IdNotFoundException : Exception
    {
        public IdNotFoundException(string? message) : base(message)
        {
        }
    }
}
