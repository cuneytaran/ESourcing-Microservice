using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Esourcing.Sourcing.Hubs
{
    //TODO : SignalR kurulumu 1
    public class AuctionHub :Hub
    {
        //whatsapp daki grup oluşturma gibi grup oluşturma
        public async Task AddToGroup(string groupName)
        {
            //Gorups SignalR dan geliyor, ConnectionId gruba girin kişinin id si
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        //Teklif gönder. diğer taraftan invoce dediğimizide  SendBidAsync yi on dediğimizde ise Bids metotunu çağıracağız.
        public async Task SendBidAsync(string groupName,string user,string bid)
        {
            //grupları Bids metoduna, useri ve bid=teklif bilgisini i gönder 
            await Clients.Group(groupName).SendAsync("Bids", user, bid);
        }
    }
}
