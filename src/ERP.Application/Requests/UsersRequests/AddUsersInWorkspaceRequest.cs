using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ERP.Shared;
using ERP.Shared.Messages;

namespace ERP.Application.Requests.UsersRequests
{
    public class AddUsersInWorkspaceRequest : BaseRequestWithValidation
    {
        public AddUsersInWorkspaceRequest(List<string> users)
        {
            Users = users;
        }

        [Required]
        public List<string> Users { get; }

        public override async Task ValidateAsync() =>
            ValidationResult = await LazyValidator.ValidateAsync<AddUsersInWorkspaceRequestValidator>(this);
    }
}
