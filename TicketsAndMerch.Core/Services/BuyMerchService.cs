// TicketsAndMerch.Core.Services/BuyMerchService.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TicketsAndMerch.Core.CustomEntities;
using TicketsAndMerch.Core.Entities;
using TicketsAndMerch.Core.Exceptions;
using TicketsAndMerch.Core.Interfaces;
using TicketsAndMerch.Core.QueryFilters;

namespace TicketsAndMerch.Core.Services
{
    public class BuyMerchService : IBuyMerchService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BuyMerchService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<BuyMerch>> GetAllBuyMerchDapperAsync()
        {
            return await _unitOfWork.BuyMerchRepositoryExtra.GetAll();
        }

        public async Task<BuyMerch> GetBuyMerchByIdAsync(int id)
        {
            return await _unitOfWork.BuyMerchRepositoryExtra.GetById(id);
        }

        public async Task<BuyMerch> BuyMerchAsync(BuyMerch buyMerch)
        {
            // Validación de usuario
            var user = await _unitOfWork.UserRepositoryExtra.GetById(buyMerch.UserId);
            if (user == null)
                throw new BussinessException("El usuario no está registrado.");

            // Validación de producto (Merch)
            var merch = await _unitOfWork.MerchRepositoryExtra.GetById(buyMerch.MerchId);
            if (merch == null)
                throw new BussinessException("El producto seleccionado no existe.");

            if (merch.Stock <= 0)
                throw new BussinessException("El producto está agotado.");

            if (merch.Stock < buyMerch.Quantity)
                throw new BussinessException("No hay suficiente stock para la cantidad solicitada.");

            // Calcular el total según precio del producto
            buyMerch.TotalAmount = buyMerch.Quantity * merch.Price;
            buyMerch.PaymentState = "Pendiente";

            // Guardar la compra
            await _unitOfWork.BuyMerchRepository.Add(buyMerch);

            // Actualizar stock del producto
            merch.Stock -= buyMerch.Quantity;
            if (merch.Stock < 0)
                merch.Stock = 0;

            // Si quisieras marcar descripción como AGOTADO:
            if (merch.Stock == 0 && !string.IsNullOrEmpty(merch.Description) && !merch.Description.Contains("(AGOTADO)"))
                merch.Description += " (AGOTADO)";

            await _unitOfWork.MerchRepository.Update(merch);
            await _unitOfWork.SaveChangesAsync();

            return buyMerch;
        }

        public async Task DeleteBuyMerchAsync(int id)
        {
            await _unitOfWork.BuyMerchRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
