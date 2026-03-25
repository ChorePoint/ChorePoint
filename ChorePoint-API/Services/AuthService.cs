using ChorePoint_API.Models;
using ChorePoint_API.Repositories;
using Microsoft.AspNetCore.Identity;

namespace ChorePoint_API.Services
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

        public async Task<bool> Register(string name, string email, string password)
        {
            var existingUser = await _repository.GetByEmail(email);
            if (existingUser != null)
                return false;

            var parent = new Parent
            {
                Name = name,
                Email = email,
                CreatedAt = DateTime.UtcNow
            };

            parent.Password = _passwordHasher.HashPassword(parent, password);

            await _repository.Create(parent);
            return true;
        }

        public async Task<Parent> Login(string email, string password)
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
