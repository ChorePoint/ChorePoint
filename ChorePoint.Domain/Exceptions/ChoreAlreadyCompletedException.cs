namespace ChorePoint.Domain.Exceptions;

public class ChoreAlreadyCompletedException : DomainException
{
    public ChoreAlreadyCompletedException(string message) : base(message) {}
}