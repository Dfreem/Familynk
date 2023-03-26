using System;

namespace Familynk.Models.Messages;

public class FamilyChat
{
    public List<FamilyMessage> Messages { get; set; } = new();
    public FamilyMessage NewMessage { get; set; } = new();
    public string SenderId { get; set; } = default!;
}

