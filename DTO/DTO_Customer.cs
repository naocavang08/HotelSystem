namespace HotelSystem.DTO
{
    public class DTO_Customer
    {
        public int CustomerId { get; set; } 
        public string Name { get; set; } 
        public string Phone { get; set; } 
        public string CCCD { get; set; } 
        public bool? Gender { get; set; } // Giới tính (true: Nam, false: Nữ)
        public int UserId { get; set; } // ID người dùng liên kết
    }
}
