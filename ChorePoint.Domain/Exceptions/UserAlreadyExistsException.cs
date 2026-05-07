namespace ChorePoint.Domain.Exceptions;

public class UserAlreadyExistsException(string email) : DomainException($"User with email {email} already exists!");