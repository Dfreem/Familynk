namespace Familynk.Hubs
{
    public interface IChatClient
    {
        public Task MessageRecieved(string user, string message);
    }
}