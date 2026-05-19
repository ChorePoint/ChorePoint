namespace ChorePoint.Domain.Exceptions;

public class ParentAlreadyExistsException(string email) : DomainException($"Parent with email [{email}] already exists!");