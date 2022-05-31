using System.Collections.Generic;

namespace CW_FrontEnd_rebuilt.ObjectModel
{
    public class User
    {
        public int userId { get; set; }
        public string userName { get; set; }
        public string role { get; set; }
        public string bio { get; set; }
        public string password { get; set; }
#nullable enable
        public string? imageLink { get; set; }
#nullable enable
        public ICollection<Character>? characters { get; set; }
    }
}
