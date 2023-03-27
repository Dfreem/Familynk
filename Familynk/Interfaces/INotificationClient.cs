using System;
namespace Familynk.Interfaces;

public interface INotificationClient
{
    Task RecieveNotificationAsync(int familyUnitId, string message);
}

