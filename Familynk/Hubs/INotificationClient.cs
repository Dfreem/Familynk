namespace Familynk.Hubs
{
    public interface INotificationClient
    {
        public Task RecieveNotificationAsync(int familyUnitId, string message);
    }
}