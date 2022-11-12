namespace FarmFresh.Common.Exceptions
{
    public class NullRequestException : Exception
    {
        public NullRequestException(string name)
            : base($"Request object {name} is null.")
        {
        }
    }
}
