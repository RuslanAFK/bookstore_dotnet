using BookStoreServer.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace BookStoreServer.Hubs;

[Authorize]
public class UsersHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        var username = Context.User?.Identity?.Name;
        if (username == null)
            return;
        await Groups.AddToGroupAsync(Context.ConnectionId, username);
        await base.OnConnectedAsync();
    }

    [Authorize(Roles = Roles.Creator)]
    public async Task ChangeRole(string username, string role)
    {
        await Clients.Group(username)
            .SendAsync(HubMethods.UpdatedRole, $"Your role is changed to {role}. Please login again.");
    }
    
    [Authorize(Roles = Roles.Creator)]
    public async Task DeleteUser(string username)
    {
        await Clients.Group(username)
            .SendAsync(HubMethods.UserDeleted, "You were banned from the site. Please register again.");
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var username = Context.User?.Identity?.Name;
        if (username == null)
            return;
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, username);
        await base.OnDisconnectedAsync(exception);
    }
}