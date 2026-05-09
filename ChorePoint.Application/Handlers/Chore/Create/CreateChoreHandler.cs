using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Entities;
using ChorePoint.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.Marshalling;
using System.Text;

namespace ChorePoint.Application.Handlers.Chore.Create
{
    public class CreateChoreHandler(IAppDbContext context, IUserService userService) : IRequestHandler<CreateChoreCommand>
    {
        public async Task Handle(CreateChoreCommand request, CancellationToken cancellationToken)
        {
            var parentId = userService.GetUserId();

            var existingUser = await context.Users
                .FirstOrDefaultAsync(u => u.Id == request.KidId, cancellationToken);

            if (existingUser == null)
                throw new NotFoundException($"No kid exists for this kid id: {request.KidId}");

            if (existingUser.ParentId != parentId)
                throw new DomainException($"Kid ID does not belong to the current parent: {parentId}");

            var chore = new Domain.Entities.Chore
            {
                Name = request.Name,
                Icon = request.Icon,
                Points = request.Points,
                Difficulty = request.Difficulty,
                Frequency = request.Frequency,
                DueDay = request.DueDay,
                UserId = request.KidId,
                Description = request.Description,
            };

            context.Chores.Add(chore);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
