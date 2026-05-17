using AppCore.Interfaces;
using AppCore.Models;
using Infrastructure.EntityFramework.Context;

namespace Infrastructure.Seeders;

public class InteractionDbSeeder : IDataSeeder
{
    public int Order => 3;
    private readonly ContactsDbContext _contactsDbContext;
    public InteractionDbSeeder(ContactsDbContext contactsDbContext)
    {
        _contactsDbContext = contactsDbContext;
    }
    public async Task SeedAsync()
    {
        var interactions = new Interaction[]
        {
            new SmsInteraction
            {
                Id = Guid.Parse("5ba5987a-f09b-48c1-b87a-edc387a07203"),
                ContactId = Guid.Parse("937348fd-7f13-4b8f-9c28-bb13639cf13c"),
                Date = DateTime.Now,
                Content = "Zmiana godzin pracy...",
                Type = InteractionType.Sms,
                CreatedAt = DateTime.Now,
                PhoneNumber = "123456789"
            },
            new MeetingInteraction
            {
                Id = Guid.Parse("c541590a-5c35-4fcf-a6ec-670e18f5c37d"),
                ContactId = Guid.Parse("937348fd-7f13-4b8f-9c28-bb13639cf13c"),
                Date = DateTime.Now,
                Content = "Uzgodnienie zasobów do wykorzystania w...",
                Type = InteractionType.Meeting,
                CreatedAt = DateTime.Now,
                Location = "ul. Jana Pawła 999z",
                DurationMinutes = 60
            },
            new EmailInteraction
            {
                Id = Guid.Parse("f6016857-ccf4-4d4e-88eb-59d59349579a"),
                ContactId = Guid.Parse("3f1bab9c-dc9c-475b-b281-755e3a23b150"),
                Date = DateTime.Now,
                Content = "Dziękujemy Wam...",
                Type = InteractionType.Email,
                CreatedAt = DateTime.Now,
                EmailAddress = "leon.leonowski@mail.com",
                Subject = "Podziękowania"
            }
        };

        foreach (var interaction in interactions)
        {
            if(await _contactsDbContext.Interactions.FindAsync(interaction.Id) != null)
                continue;
            
            await _contactsDbContext.Interactions.AddAsync(interaction);
        }
        
        await _contactsDbContext.SaveChangesAsync();
    }
    


}