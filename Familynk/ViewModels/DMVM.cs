namespace Familynk.ViewModels;

public class DMVM
{
    public List<DirectMessage> SentMessages { get; set; } = new();
    public List<DirectMessage> RecievedMessages { get; set; } = new();
    public List<DirectMessage> Responses { get; set; } = new();
    public List<FamilyMember> Senders { get; set; } = new();
    public FamilyMember Owner { get; set; } = default!;
    [Required(ErrorMessage = "Please select a recipient.")]
    public string? SendMessageTo { get; set; } = default;
    public DirectMessage? NewMessage { get; set; } = null;
    public DirectMessage? EditMessage { get; set; } = null;
    public List<FamilyMember> Contacts { get; set; } = new();
}

