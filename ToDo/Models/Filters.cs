using Microsoft.AspNetCore.SignalR;

namespace ToDo.Models
{
    public class Filters
    {
        public Filters(string filterstring) {
            FilterString = filterstring ?? "all-all-all-all";
            string[] filters = FilterString.Split('-');
            UserId = filters[0];
            CategoryId = filters[1];
            Due = filters[2];
            StatusId = filters[3];
        }

        public string FilterString { get; }
        public string CategoryId { get; }
        public string Due { get; }
        public string StatusId { get; }
        public string UserId { get; }
        public bool HasCategory => CategoryId.ToLower() != "all";
        public bool HasDue => Due.ToLower() != "all";
        public bool HasStatus => StatusId.ToLower() != "all";
        public bool HasUser => UserId.ToLower() != "all";
        public static Dictionary<string, string> DueFilterValues =>
            new Dictionary<string, string>
            {
                {"future", "Future" },
                {"past", "Past" },
                {"today", "Today" }
            };

        public bool IsPast => Due.ToLower() == "past";
        public bool IsFuture => Due.ToLower() == "future";
        public bool IsToday => Due.ToLower() == "today";
    }
}
