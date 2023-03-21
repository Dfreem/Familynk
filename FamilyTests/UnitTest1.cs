using Familynk.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FamilyTests;

[TestFixture]
public class FakeFamilyRepositoryTests
{
    private FakeFamilyRepository _repo;

    [SetUp]
    public void Setup()
    {
        _repo = new FakeFamilyRepository();
    }

    [Test]
    public async Task CreateFamilyEventAsync_ShouldCreateNewEvent()
    {
        // Arrange
        var newEvent = new FamilyEvent
        {
            Title = "Test Event",
            EventDate = DateTime.Now,
            Details = "This is a test event"
        };

        // Act
        var createdEvent = await _repo.CreateFamilyEventAsync(newEvent);
        Assert.Multiple(() =>
        {

            // Assert
            Assert.That(_repo.GetFamilyEventsAsync().Result, Has.Count.EqualTo(1));
            Assert.That(createdEvent.Title, Is.EqualTo(newEvent.Title));
        });
        Assert.That(createdEvent.EventDate, Is.EqualTo(newEvent.EventDate));
        Assert.That(createdEvent.Details, Is.EqualTo(newEvent.Details));
    }

    [Test]
    public async Task UpdateFamilyEventAsync_ShouldUpdateExistingEvent()
    {
        // Arrange
        var existingEvent = new FamilyEvent
        {
            FamilyEventId = 1,
            Title = "Test Event",
            EventDate = DateTime.Now,
            Details = "This is a test event"
        };
        _repo.CreateFamilyEventAsync(existingEvent).Wait();

        var updatedEvent = new FamilyEvent
        {
            FamilyEventId = 1,
            Title = "Updated Test Event",
            EventDate = DateTime.Now.AddDays(1),
            Details = "This is an updated test event"
        };

        // Act
        await _repo.UpdateFamilyEventAsync(updatedEvent);

        // Assert
        var result = await _repo.GetFamilyEventAsync(updatedEvent.FamilyEventId);
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Title, Is.EqualTo(updatedEvent.Title));
            Assert.That(result.EventDate, Is.EqualTo(updatedEvent.EventDate));
            Assert.That(result.Details, Is.EqualTo(updatedEvent.Details));
        });
    }

    [Test]
    public async Task DeleteFamilyEventAsync_ShouldDeleteExistingEvent()
    {
        // Arrange
        var existingEvent = new FamilyEvent
        {
            FamilyEventId = 1,
            Title = "Test Event",
            EventDate = DateTime.Now,
            Details = "This is a test event"
        };
        _repo.CreateFamilyEventAsync(existingEvent).Wait();

        // Act
        await _repo.DeleteFamilyEventAsync(existingEvent.FamilyEventId);

        // Assert
        var result = await _repo.GetFamilyEventAsync(existingEvent.FamilyEventId);
        Assert.IsNull(result);
        Assert.That(_repo.GetFamilyEventsAsync().Result.Count, Is.EqualTo(0));
    }

}
        