namespace FrannHammer.AccountRegistrationTool.Models
{
    public class RegistrationResult
    {
        public bool IsSuccessful { get; }
        public string Message { get; }

        public RegistrationResult(bool isSuccess, string message)
        {
            IsSuccessful = isSuccess;
            Message = message;
        }
    }
}
