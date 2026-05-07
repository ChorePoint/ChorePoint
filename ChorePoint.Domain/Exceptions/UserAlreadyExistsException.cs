namespace ChorePoint.Domain.Exceptions;

public class UserAlreadyExistsException : DomainException
{
    public UserAlreadyExistsException(string email)
        : base($"User with email {email} already exists!")
    {
    }
}