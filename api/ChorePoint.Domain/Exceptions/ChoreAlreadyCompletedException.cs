namespace ChorePoint.Domain.Exceptions;

public class ChoreAlreadyCompletedException(string message) : DomainException(message);
