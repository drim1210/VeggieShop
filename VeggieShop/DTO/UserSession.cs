namespace VeggieShop.DTO
{
    public class UserSession
    {
        // dùng chung toàn app (tuỳ bạn dùng hay không)
        public static UserSession Current { get; } = new UserSession();

        public int UserId { get; set; }
        public string Username { get; set; } = "";
        public string FullName { get; set; } = "";
        public string RoleName { get; set; } = "Staff";

        // ✅ để public để AuthBUS có thể new
        public UserSession() { }
    }
}
