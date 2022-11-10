namespace FarmFresh.Common.Exceptions
{
    public class ValidationException : Exception
    {
        public IDictionary<string, string> Failures { get; }

        public ValidationException()
            : base("One or more validation failures have occurred.")
        {
            Failures = new Dictionary<string, string>();
        }

        public ValidationException(List<(string PropertyName, string PropertyFailure)> failures)
            : this()
        {
            foreach (var failure in failures)
            {
                var propertyName = failure.PropertyName;
                var propertyFailure = failure.PropertyFailure;

                Failures.Add(propertyName, propertyFailure);
            }
        }
    }
}
