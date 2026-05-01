using ChorePoint.API.Models;
using ChorePoint.API.Repositories;
using ChorePoint.API.Results;
using Microsoft.AspNetCore.Identity;

namespace ChorePoint.API.Services
{
    public class AuthService
    {
        private readonly IParentRepository _repository;
        private readonly IPasswordHasher<Parent> _passwordHasher;

        public AuthService(IParentRepository repository, IPasswordHasher<Parent> passwordHasher)
        {
            _repository = repository;
            _passwordHasher = passwordHasher;
        }

        public async Task<IServiceResult> Register(
            string firstName,
            string lastName,
            string email,
            string password
        )
        {
            var existingUser = await _repository.GetByEmail(email);
            if (existingUser != null)
                return new ServiceResult(false, "User already exists!");

            var parent = new Parent
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                CreatedAt = DateTime.UtcNow
            };
            parent.Password = _passwordHasher.HashPassword(parent, password);

            await _repository.Create(parent);
            return new ServiceResult(true);
        }

        public async Task<Parent?> Login(string email, string password)
        {
            var parent = await _repository.GetByEmail(email);
            if (parent == null)
                return null;

            var result = _passwordHasher.VerifyHashedPassword(parent, parent.Password, password);
            if (result == PasswordVerificationResult.Failed)
                return null;

            return parent;
        }
    }
}
