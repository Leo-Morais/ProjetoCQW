namespace ProjetoCQW.CustomExceptions
{
    public class WrongPropertyException : Exception
    {
        public WrongPropertyException(string? message) : base(message)
        {
        }
    }
}
