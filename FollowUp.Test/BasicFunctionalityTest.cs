using FollowUp.Domain;

namespace FollowUp.UnitTesting
{
    public class BasicFunctionalityTest
    {
        private readonly Domain.FollowUp _followUp;
        public BasicFunctionalityTest()
        {
            _followUp = Domain.FollowUp.Create(
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