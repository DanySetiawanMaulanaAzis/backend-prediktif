namespace prediktif.Models
{
    public class User
    {
        public int UserId { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public bool Is_Operator { get; set; }

        public bool Is_Technician { get; set; }

        public bool Is_Engineer { get; set; }

        public DateTime Created_At { get; set; }
    }
}
