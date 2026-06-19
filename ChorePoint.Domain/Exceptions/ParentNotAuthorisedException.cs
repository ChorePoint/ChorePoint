namespace ChorePoint.Domain.Exceptions;

public class ParentNotAuthorisedException(int parentIdFromContext) : DomainException($"Parent with ID [{parentIdFromContext}] is not authorised to access the resource");