namespace HotelSystem.DTO
{
    public class DTO_Customer
    {
        public int CustomerId { get; set; } 
        public string Name { get; set; } 
        public string Phone { get; set; } 
        public string CCCD { get; set; } 
        public bool? Gender { get; set; } // Giới tính (true: Nam, false: Nữ)
        public string GenderDisplay
        {
            get => Gender == true ? "Nam" : Gender == false ? "Nữ" : "";
            set
            {
                if (value == "Nam") Gender = true;
                else if (value == "Nữ") Gender = false;
                else Gender = null;
            }
        }
        public int UserId { get; set; } // ID người dùng liên kết
    }
}
