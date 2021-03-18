using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using BLL.Mapper;
using Microsoft.EntityFrameworkCore;
using POC.BLL.Dto;
using POC.DAL.Entities;
using POC.DAL.Repositories;

namespace POC.BLL.Services
{
    public class CanvasService : ICanvasService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CanvasService(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <inheritdoc/>
        public async Task CreateCanvasAsync(CreateCanvasDto canvasDto)
        {
            var canvas = ObjMapper.Map<CreateCanvasDto, Canvas>(canvasDto);
            _unitOfWork.Canvas.Create(canvas);
            await _unitOfWork.SaveAsync();
        }

        /// <inheritdoc/>
        public async Task DeleteCanvasByIdAsync(string Id)
        {
            var canvas = await _unitOfWork.Canvas.FindByIdAsync(Id);

            if (canvas != null)
            {
                _unitOfWork.Canvas.Delete(canvas);
                await _unitOfWork.SaveAsync();
            }
        }

        /// <inheritdoc/>
        public async Task<IList<CanvasDto>> GetCanvasesAsync()
        {
            var result = await _unitOfWork.Canvas
                .FindAll()
                .ProjectTo<CanvasDto>(ObjMapper.configuration)
                .OrderBy(x => x.Price)
                .ToListAsync();

            return result;
        }
    }
}