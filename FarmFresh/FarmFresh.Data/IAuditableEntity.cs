namespace FarmFresh.Data
{
    public abstract class IAuditableEntity<TKey> : IEntity<TKey>
    {
        public Guid? CreatedBy { get; set; }

        public DateTime Created { get; set; }

        public Guid? LastModifiedBy { get; set; }

        public DateTime? LastModified { get; set; }
    }
}
