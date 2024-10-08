using System;
using InventarioESFEAPIs.Models;

namespace InventarioESFEAPIs.Services.Interfaces;

public interface IPrestamoService
{
        Task<IEnumerable<Prestamo>> GetPrestamos();

        Task<Prestamo> GetPrestamoById(int Id);
        Task<Prestamo> CreatePrestamo(Prestamo prestamo);
        Task<Prestamo> UpdatePrestamo(Prestamo prestamo, int Id);
        Task<Prestamo> SuprimirPrestamoAsync(int Id);
}
