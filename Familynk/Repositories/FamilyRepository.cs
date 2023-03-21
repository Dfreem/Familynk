using System;
using Microsoft.EntityFrameworkCore;

namespace Familynk.Repositories;

public class FamilyRepository : IFamilyRepo
{
    private readonly FamilyContext _dbContext;
    private readonly IServiceProvider _services;
    private readonly SignInManager<FamilyMember> _signInManager;
    private readonly UserManager<FamilyMember> _userManager;

    public FamilyRepository(IServiceProvider services, FamilyContext context)
    {
        _dbContext = context;
        _services = services;
        _signInManager = _services.GetRequiredService<SignInManager<FamilyMember>>();
        _userManager = _signInManager.UserManager;
    }

    // FamilyEvent CRUD operations
    public async Task<FamilyEvent?> GetFamilyEventAsync(int eventId)
    {
        return await _dbContext.Events.FirstOrDefaultAsync(e => e.FamilyEventId == eventId);
    }

    public async Task<List<FamilyEvent>> GetFamilyEventsAsync()
    {
        return await _dbContext.Events.ToListAsync();
    }

    public async Task<FamilyEvent> CreateFamilyEventAsync(FamilyEvent @event)
    {
        await _dbContext.Events.AddAsync(@event);
        await _dbContext.SaveChangesAsync();
        return @event;
    }

    public async Task UpdateFamilyEventAsync(FamilyEvent @event)
    {
        _dbContext.Entry(@event).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteFamilyEventAsync(int eventId)
    {
        var @event = await _dbContext.Events.FindAsync(eventId);
        _dbContext.Events.Remove(@event!);
        await _dbContext.SaveChangesAsync();
    }

    // FamilyUnit CRUD operations
    public async Task<FamilyUnit?> GetFamilyUnitAsync(int unitId)
    {
        return await _dbContext.Neighborhood.FirstOrDefaultAsync(u => u.FamilyUnitId == unitId);
    }

    public async Task<List<FamilyUnit>> GetNeighborhoodAsync()
    {
        return await _dbContext.Neighborhood.ToListAsync();
    }

    public async Task<FamilyUnit> CreateFamilyUnitAsync(FamilyUnit unit)
    {
        await _dbContext.Neighborhood.AddAsync(unit);
        await _dbContext.SaveChangesAsync();
        return unit;
    }

    public async Task UpdateFamilyUnitAsync(FamilyUnit unit)
    {
        _dbContext.Entry(unit).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteFamilyUnitAsync(int unitId)
    {
        var unit = await _dbContext.Neighborhood.FindAsync(unitId);
        _dbContext.Neighborhood.Remove(unit!);
        await _dbContext.SaveChangesAsync();
    }

    // AppMessage CRUD operations
    public async Task<DirectMessage?> GetDMAsync(int DirectMessageId)
    {
        return await _dbContext.DMs.FirstOrDefaultAsync(m => m.AppMessageId == DirectMessageId);
    }

    public async Task<List<DirectMessage>> GetDMsAsync()
    {
        return await _dbContext.DMs.ToListAsync();
    }

    public async Task CreateDirectMessageAsync(DirectMessage message)
    {
        await _dbContext.DMs.AddAsync(message);
        await _dbContext.SaveChangesAsync();
    }
    public async Task DeleteDMAsync(DirectMessage dm)
    {
        _dbContext.DMs.Remove(dm);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<FamilyMessage?> GetFamilyMessageAsync(int FamilyMessageId) => await _dbContext.ChatTv.FirstOrDefaultAsync(m => m.AppMessageId == FamilyMessageId);

    public async Task<List<FamilyMessage>> GetFamilyMessagesAsync()
    {
        return await _dbContext.ChatTv.ToListAsync();
    }

    public async Task CreateFamilyMessageAsync(FamilyMessage message)
    {
        await _dbContext.ChatTv.AddAsync(message);
        await _dbContext.SaveChangesAsync();
    }
    public async Task DeleteFamilyMessageAsync(FamilyMessage fm)
    {
        _dbContext.ChatTv.Remove(fm);
        await _dbContext.SaveChangesAsync();
    }

    // Create
    public async Task<int> AddHouseRulesAsync(HouseRules houseRules, FamilyUnit family)
    {
        family = _dbContext.Neighborhood.Find(family)!;
        family.Rules = houseRules;
        _dbContext.Rules.Add(houseRules);
        return await _dbContext.SaveChangesAsync();
    }

    // Read
    public async Task<HouseRules?> GetHouseRulesAsync(int houseRulesId)
    {
        return await _dbContext.Rules.FindAsync(houseRulesId);
    }

    // Update
    public async Task<int> UpdateHouseRulesAsync(HouseRules houseRules)
    {
        _dbContext.Rules.Update(houseRules);
        return await _dbContext.SaveChangesAsync();
    }

    // Delete
    public async Task<int> DeleteHouseRulesAsync(int houseRulesId)
    {
        var houseRules = await GetHouseRulesAsync(houseRulesId);

        if (houseRules == null)
        {
            return 0;
        }

        _dbContext.Rules.Remove(houseRules);
        return await _dbContext.SaveChangesAsync();
    }
}

