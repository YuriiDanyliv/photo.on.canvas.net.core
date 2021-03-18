using System;

namespace POC.BLL.Dto
{
    public class OrderDto
    {
        public string Id { get; set; }

        public string CustomerName { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public DateTime CreationDate { get; set; }

        public CanvasDto Canvas { get; set; }

        public byte[] Image { get; set; }
    }
}
