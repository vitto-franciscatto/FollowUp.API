using FollowUp.API.Features.FollowUps;

namespace FollowUp.UnitTesting.BasicFunctionality;

public class ContactValidationTests
{
    public static Contact ValidContact()
    {
        return Contact.Create("Jhon", "(19) 99199-2993", "Hitman");
    }
}