namespace Familynk.Repositories;
public class FakeFamilyRepository : IFamilyRepo
{
    private readonly List<FamilyEvent> _events = new List<FamilyEvent>();
    private readonly List<FamilyUnit> _units = new List<FamilyUnit>();
    private readonly List<DirectMessage> _dms = new List<DirectMessage>();
    private readonly List<FamilyMessage> _familyMessages = new List<FamilyMessage>();
    private readonly List<HouseRules> _houseRules = new List<HouseRules>();

    public async Task<FamilyEvent?> GetFamilyEventAsync(int eventId)
    {
        return await Task.FromResult(_events.FirstOrDefault(e => e.FamilyEventId == eventId));
    }

    public async Task<List<FamilyEvent>> GetFamilyEventsAsync()
    {
        return await Task.FromResult(_events);
    }

    public async Task<FamilyEvent> CreateFamilyEventAsync(FamilyEvent @event)
    {
        @event.FamilyEventId = _events.Count + 1;
        _events.Add(@event);
        return await Task.FromResult(@event);
    }

    public async Task UpdateFamilyEventAsync(FamilyEvent @event)
    {
        var index = _events.FindIndex(e => e.FamilyEventId == @event.FamilyEventId);
        if (index != -1)
        {
            _events[index] = @event;
        }
        await Task.CompletedTask;
    }

    public async Task DeleteFamilyEventAsync(int eventId)
    {
        var @event = await GetFamilyEventAsync(eventId);
        if (@event != null)
        {
            _events.Remove(@event);
        }
        await Task.CompletedTask;
    }

    public async Task<FamilyUnit?> GetFamilyUnitAsync(int unitId)
    {
        return await Task.FromResult(_units.FirstOrDefault(u => u.FamilyUnitId == unitId));
    }

    public async Task<List<FamilyUnit>> GetNeighborhoodAsync()
    {
        return await Task.FromResult(_units);
    }

    public async Task<FamilyUnit> CreateFamilyUnitAsync(FamilyUnit unit)
    {
        unit.FamilyUnitId = _units.Count + 1;
        _units.Add(unit);
        return await Task.FromResult(unit);
    }

    public async Task UpdateFamilyUnitAsync(FamilyUnit unit)
    {
        var index = _units.FindIndex(u => u.FamilyUnitId == unit.FamilyUnitId);
        if (index != -1)
        {
            _units[index] = unit;
        }
        await Task.CompletedTask;
    }

    public async Task DeleteFamilyUnitAsync(int unitId)
    {
        var unit = await GetFamilyUnitAsync(unitId);
        if (unit != null)
        {
            _units.Remove(unit);
        }
        await Task.CompletedTask;
    }

    public async Task<DirectMessage?> GetDMAsync(int DirectMessageId)
    {
        return await Task.FromResult(_dms.FirstOrDefault(m => m.DirectMessageId == DirectMessageId));
    }

    public async Task<List<DirectMessage>> GetDMsAsync()
    {
        return await Task.FromResult(_dms);
    }

    public async Task CreateDirectMessageAsync(DirectMessage message)
    {
        message.DirectMessageId = _dms.Count + 1;
        _dms.Add(message);
        await Task.CompletedTask;
    }

    public async Task DeleteDMAsync(DirectMessage dm)
    {
        _dms.Remove(dm);
        await Task.CompletedTask;
    }

    public async Task<FamilyMessage?> GetFamilyMessageAsync(int FamilyMessageId)
    {
        return await Task.FromResult(_familyMessages.FirstOrDefault(m => m.FamilyMessageId == FamilyMessageId));
    }

    public async Task<List<FamilyMessage>> GetFamilyMessagesAsync()
    {
        return await Task.FromResult(_familyMessages);
    }

    public async Task CreateFamilyMessageAsync(FamilyMessage message)
    {
        _familyMessages.Add(message);
        await Task.CompletedTask;
    }

    public async Task DeleteFamilyMessageAsync(FamilyMessage fm)
    {
        _familyMessages.Remove(fm);
        await Task.CompletedTask;
    }

    public async Task<int> AddHouseRulesAsync(HouseRules houseRules, FamilyUnit family)
    {
        _houseRules.Add(houseRules);
        return await Task.FromResult(_houseRules.Count);
    }

    public async Task<HouseRules?> GetHouseRulesAsync(int houseRulesId)
    {
        return await Task.FromResult(_houseRules.FirstOrDefault(hr => hr.HouseRulesId == houseRulesId));
    }

    public async Task<int> UpdateHouseRulesAsync(HouseRules houseRules)
    {
        var existingRules = _houseRules.FirstOrDefault(hr => hr.HouseRulesId == houseRules.HouseRulesId);
        if (existingRules != null)
        {
            existingRules.HouseRulesId = houseRules.HouseRulesId;
            existingRules.MagneticMessageLifespan = houseRules.MagneticMessageLifespan;
            existingRules.StickyNoteLifespan = houseRules.StickyNoteLifespan;
            existingRules.FamilyMembersCustomizeKitchen = houseRules.FamilyMembersCustomizeKitchen;
            existingRules.FamilyMembersCreateEvents = houseRules.FamilyMembersCreateEvents;
            existingRules.FamilyMembersInviteOtherMembers = houseRules.FamilyMembersInviteOtherMembers;
        }
        return await Task.FromResult(existingRules != null ? 1 : 0);
    }

    public async Task<int> DeleteHouseRulesAsync(int houseRulesId)
    {
        var rulesToRemove = _houseRules.FirstOrDefault(hr => hr.HouseRulesId == houseRulesId);
        if (rulesToRemove != null)
        {
            _houseRules.Remove(rulesToRemove);
        }
        return await Task.FromResult(rulesToRemove != null ? 1 : 0);
    }
}



