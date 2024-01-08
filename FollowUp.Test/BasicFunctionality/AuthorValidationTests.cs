using FollowUp.API.Features.FollowUps;

namespace FollowUp.UnitTesting.BasicFunctionality;

public class AuthorValidationTests
{
    private const int ValidId = 1;
    private const string ValidExtension = "12345";
    private readonly AuthorValidator _validator = new();

    public static Author ValidAuthor()
    {
        Author author = Author.Construct();
        author.Id = 22;
        author.Extension = "12345";

        return author;
    }

    [Fact]
    public void Author_ShouldBeInvalid_WhenIdLessThanOne()
    {
        //Arrange
        var lessThanOneId = 0;
        var author = Author.Create(lessThanOneId, ValidExtension);
        
        //Act
        var validationResult = _validator.Validate(author);

        //Assert
        Assert.False(validationResult.IsValid);
        Assert.Equal($"O Id: {lessThanOneId} do autor é inválido", validationResult.Errors.First().ErrorMessage);

    }
    
    [Fact]
    public void Author_ShouldBeInvalid_WhenExtensionIsNull()
    {
        //Arrange
        string? nullExtension = default!;
        var author = Author.Create(ValidId, nullExtension);
        
        //Act
        var validationResult = _validator.Validate(author);

        //Assert
        Assert.False(validationResult.IsValid);
        Assert.Equal("O Ramal do autor não pode ser nulo", validationResult.Errors.First().ErrorMessage);

    }
    
    [Fact]
    public void Author_ShouldBeInvalid_WhenExtensionIsGreaterThan50()
    {
        //Arrange
        var someBigExtensionValue = "111111111111111111111111111111111111111111111111111";
        var author = Author.Create(
            ValidId, 
            someBigExtensionValue);
        
        //Act
        var validationResult = _validator.Validate(author);

        //Assert
        Assert.False(validationResult.IsValid);
        Assert.Equal("O Ramal do autor deve ter no máximo 50 caracteres", validationResult.Errors.First().ErrorMessage);

    }
    
    [Fact]
    public void Author_ShouldBeValid_WhenIdIsGreaterThanOne()
    {
        //Arrange
        var someValidIdValue = 13;
        var author = Author.Create(someValidIdValue, ValidExtension);
        
        //Act
        var validationResult = _validator.Validate(author);

        //Assert
        Assert.True(validationResult.IsValid);

    }
    
    [Fact]
    public void Author_ShouldBeValid_WhenExtensionIsEmpty()
    {
        //Arrange
        var emptyExtensionValue = string.Empty;
        var author = Author.Create(ValidId, emptyExtensionValue);
        
        //Act
        var validationResult = _validator.Validate(author);

        //Assert
        Assert.True(validationResult.IsValid);

    }
}