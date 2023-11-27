using System.Collections.ObjectModel;

namespace MicrobApp.Models
{
    public class PostPaginated
    {
        public int CurrentPage { get; set; } = 0;
        public int TotalCount { get; set; } = 0;
        public int TotalPages { get; set; } = 0;
        public bool HasPrevious { get; set; } = false;
        public bool HasNext { get; set; } = false;
        public ObservableCollection<Post> Posts { get; set; } = new ObservableCollection<Post>();
    }
}
