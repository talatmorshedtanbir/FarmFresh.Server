namespace FarmFresh.Common.Exceptions
{
    public class DuplicationException : Exception
    {
        public DuplicationException(string name)
            : base($"{name} already exists.")
        {
        }
    }
}
