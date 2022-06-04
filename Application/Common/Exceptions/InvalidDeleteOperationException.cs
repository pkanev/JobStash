namespace JobStash.Application.Common.Exceptions;

public class InvalidDeleteOperationException : Exception
{
    public InvalidDeleteOperationException(string message)
            : base(message)
    {
    }
}
