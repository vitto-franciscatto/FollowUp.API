
using FollowUp.API.Features.Tags;

namespace FollowUp.UnitTesting
{
    public class BasicFunctionalityTest
    {
        private readonly API.Features.FollowUps.FollowUp _followUp;
        public BasicFunctionalityTest()
        {
            _followUp = API.Features.FollowUps.FollowUp.Create(
                21, 
                2134, 
                null, 
                null, 
                "message", 
                DateTime.Now,
                DateTime.Now.AddDays(1.0),
                new List<Tag>()
                {
                    Tag.Create(
                        3, 
                        "residue")
                }
                );
        }

        [Fact]
        public void ChangeSenderShouldUpdateSenderValue()
        {
            //Arrange
            string newSender = "newSender";
            //Act
            
            //Assert
            
        }
    }
}