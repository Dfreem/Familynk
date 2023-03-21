using System.Collections.Generic;
using System.Threading.Tasks;

namespace Familynk.Repositories
{
    public interface IFamilyRepo
    {
        Task<FamilyEvent?> GetFamilyEventAsync(int eventId);
        Task<List<FamilyEvent>> GetFamilyEventsAsync();
        Task<FamilyEvent> CreateFamilyEventAsync(FamilyEvent @event);
        Task UpdateFamilyEventAsync(FamilyEvent @event);
        Task DeleteFamilyEventAsync(int eventId);

        Task<FamilyUnit?> GetFamilyUnitAsync(int unitId);
        Task<List<FamilyUnit>> GetNeighborhoodAsync();
        Task<FamilyUnit> CreateFamilyUnitAsync(FamilyUnit unit);
        Task UpdateFamilyUnitAsync(FamilyUnit unit);
        Task DeleteFamilyUnitAsync(int unitId);

        Task<DirectMessage?> GetDMAsync(int DirectMessageId);
        Task<List<DirectMessage>> GetDMsAsync();
        Task CreateDirectMessageAsync(DirectMessage message);
        Task DeleteDMAsync(DirectMessage dm);

        Task<FamilyMessage?> GetFamilyMessageAsync(int FamilyMessageId);
        Task<List<FamilyMessage>> GetFamilyMessagesAsync();
        Task CreateFamilyMessageAsync(FamilyMessage message);
        Task DeleteFamilyMessageAsync(FamilyMessage fm);

        Task<int> AddHouseRulesAsync(HouseRules houseRules, FamilyUnit family);
        Task<HouseRules?> GetHouseRulesAsync(int houseRulesId);
        Task<int> UpdateHouseRulesAsync(HouseRules houseRules);
        Task<int> DeleteHouseRulesAsync(int houseRulesId);
    }
}
