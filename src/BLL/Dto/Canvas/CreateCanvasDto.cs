using System.ComponentModel.DataAnnotations;

namespace POC.BLL.Dto
{
    public class CreateCanvasDto
    {
        [Required(ErrorMessage = "It is necessary to specify the price of a canvas")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "It is necessary to specify the size of canvas")]
        public string Size { get; set; }
    }
}