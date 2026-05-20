using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChorePoint.Application.Handlers.ChoreSubmission.ReviewSubmission
{
    public record ReviewSubmissionCommand(
        int ChoreSubmissionId,
        bool IsApproved
    ) : IRequest;
}
