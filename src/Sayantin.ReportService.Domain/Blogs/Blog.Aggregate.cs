using ReportService.Domain.Blogs.Events;

namespace ReportService.Domain.Blogs
{
    /// <summary>
    /// Blog Aggregate Root class.
    /// </summary>
    public partial class Blog : IAggregateRoot
    {
        public Blog(string title, string description, int authorId)
            : this()
        {
            Update(title, description, authorId);
        }

        public void Update(string title, string description, int authorId)
        {
            Title = title.Trim();
            Description = description.Trim();
            AuthorId = authorId;
        }

        public void Publish()
        {
            IsPublished = true;

            var addEvent = new BlogPublishedEvent()
            {
                BlogId = Id,
                BlogTitle = Title
            };

            AddEvent(addEvent);
        }

        public void Deliver()
        {
            IsDelivered = true;
        }
    }
}
