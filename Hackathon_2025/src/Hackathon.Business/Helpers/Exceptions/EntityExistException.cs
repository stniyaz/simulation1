namespace Hackathon.Business.Helpers.Exceptions;

public class EntityExistException : Exception
{
    public EntityExistException() : base("Entity already exists!")
    {
    }

    public EntityExistException(string message) : base(message)
    {
    }

    public EntityExistException(string message, Exception innerException)
        : base(message, innerException) { }
}


