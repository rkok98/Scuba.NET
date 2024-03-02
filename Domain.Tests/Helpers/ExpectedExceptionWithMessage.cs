namespace Domain.Tests.Helpers;

public sealed class ExpectedExceptionWithMessage : ExpectedExceptionBaseAttribute
{
    private readonly string _expectedExceptionMessage;
    private readonly Type _expectedExceptionType;

    public ExpectedExceptionWithMessage(Type expectedExceptionType)
    {
        _expectedExceptionType = expectedExceptionType;
        _expectedExceptionMessage = string.Empty;
    }

    public ExpectedExceptionWithMessage(Type expectedExceptionType, string expectedExceptionMessage)
    {
        _expectedExceptionType = expectedExceptionType;
        _expectedExceptionMessage = expectedExceptionMessage;
    }

    protected override void Verify(Exception exception)
    {
        Assert.IsNotNull(exception);

        Assert.IsInstanceOfType(exception, _expectedExceptionType, "Wrong type of exception was thrown.");

        if (!_expectedExceptionMessage.Length.Equals(0))
            Assert.AreEqual(_expectedExceptionMessage, exception.Message, "Wrong exception message was returned.");
    }
}