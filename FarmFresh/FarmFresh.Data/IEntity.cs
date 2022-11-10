namespace FarmFresh.Data
{
    public abstract class IEntity<TKey>
    {
        public TKey Id { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }

        public IEntity()
        {
            IsDeleted = false;
            IsActive = true;
        }
    }
}
