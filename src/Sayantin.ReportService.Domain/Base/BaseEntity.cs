namespace ReportService.Domain.Base
{
    public abstract class BaseEntity
    {
        private List<BaseDomainEvent> _events = [];
        public IReadOnlyList<BaseDomainEvent> Events => _events.AsReadOnly();

        public void AddEvent(BaseDomainEvent @event)
        {
            _events.Add(@event);
        }

        public void RemoveEvent(BaseDomainEvent @event)
        {
            _events.Remove(@event);
        }

        public void ClearEvents()
        {
            _events.Clear();
        }
    }

    public abstract class BaseEntity<TKey> : BaseEntity
    {
        required public TKey Id { get; set; }
    }
}