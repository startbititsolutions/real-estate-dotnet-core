namespace Danweyne_Real_estate.Models
{
    public class UserViewModel
        {
            public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool lockoutenabled { get; set; }
        public DateTimeOffset? lockoutend { get; set; }

    }
    }

