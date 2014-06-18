using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Threading.Tasks;

namespace SignalRChat.Hubs {
    [HubName("ChatHub")]
    public class ChatHub : Hub {
        public void Send(string name, string message) {
            // Call the addNewMessageToPage method to update clients.
            Clients.All.addNewMessageToPage(name, message);
        }

        public void Send(string groupName, string name, string message) {
            Clients.Group(groupName).addNewMessageToPage(name, message);
        }

        public async Task JoinGroup(string oldGroup, string groupName) {
            Groups.Remove(Context.ConnectionId, oldGroup);
            await Groups.Add(Context.ConnectionId, groupName);
            Clients.Group(groupName).addNewMessageToPage(Context.User.Identity.Name + " joined.");
        }
    }
}