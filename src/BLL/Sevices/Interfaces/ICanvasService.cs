using System.Collections.Generic;
using System.Threading.Tasks;
using POC.BLL.Dto;

namespace POC.BLL.Services
{
    public interface ICanvasService
    {
        /// <summary>
        /// Create new canvas
        /// </summary>
        /// <param name="canvas">canvas data</param>
        /// <returns></returns>
        Task CreateCanvasAsync(CreateCanvasDto canvas);

        /// <summary>
        /// Delete canvas by ID
        /// </summary>
        /// <param name="Id">canvas ID</param>
        /// <returns></returns>
        Task DeleteCanvasByIdAsync(string Id);

        /// <summary>
        /// Get all canvases
        /// </summary>
        /// <returns></returns>
        Task<IList<CanvasDto>> GetCanvasesAsync();
    }
}
