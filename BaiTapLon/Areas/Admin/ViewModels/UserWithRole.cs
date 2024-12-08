using BaiTapLon.Models;

namespace BaiTapLon.Areas.Admin.ViewModels
{
    public class UserWithRole
    {
        public AppUser User { get; set; }
        public IList<string> Role { get; set; }
    }
}