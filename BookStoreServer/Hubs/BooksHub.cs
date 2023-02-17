using BookStoreServer.Enums;
using Microsoft.AspNetCore.SignalR;

namespace BookStoreServer.Hubs;

public class BooksHub : Hub
{
    public async Task UpdateBook()
    {
        await Clients.All.SendAsync(HubMethods.ChangedBooks);
    }
}