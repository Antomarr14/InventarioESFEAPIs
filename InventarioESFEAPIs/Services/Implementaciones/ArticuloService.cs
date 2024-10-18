using System.Collections.Generic;
using System.Threading.Tasks;
using InventarioESFEAPIs.Context;
using InventarioESFEAPIs.DTO;
using InventarioESFEAPIs.Models;
using InventarioESFEAPIs.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InventarioESFEAPIs.Services.Implementaciones
{
    public class ArticuloService : IArticuloService
    {
        private readonly InventarioESFEContext _context;

        public ArticuloService(InventarioESFEContext inventarioESFEContext)
        {
            _context = inventarioESFEContext;
        }

        public async Task<IEnumerable<ArticuloDTO>> GetArticulos()
        {
            var articulos = await _context.Articulo
                .Include(a => a.Categoria)
                .Include(a => a.Estado)
                .Include(a => a.Marca)
                .Include(a => a.Ubicacion)
                .Include(a => a.Usuario)
                .Include(a => a.Tipo)
                .ToListAsync();

            var articulosDtoList = new List<ArticuloDTO>();
            foreach (var item in articulos)
            {
                articulosDtoList.Add(new ArticuloDTO
                {
                    Id = item.Id,
                    Nombre = item.Nombre,
                    ContenidoPorEmpaque = item.ContenidoPorEmpaque,
                    StockMaxima = item.StockMaxima,
                    Stock = item.Stock,
                    StockMinima = item.StockMinima,
                    Presentacion = item.Presentacion,
                    Disponibilidad = item.Disponibilidad,
                    IdMarca = item.IdMarca,
                    IdCategoria = item.IdCategoria,
                    UnidadDeMedida = item.UnidaddeMedida, // Asegúrate de que esto sea decimal
                    IdUbicacion = item.IdUbicacion,
                    IdUsuario = item.IdUsuario,
                    IdEstado = item.IdEstado,
                    IdTipo = item.IdTipo
                });
            }
            return articulosDtoList;
        }

        public async Task<ArticuloDTO> GetArticuloById(int id)
        {
            var articulo = await _context.Articulo
                .Include(a => a.Categoria)
                .Include(a => a.Estado)
                .Include(a => a.Marca)
                .Include(a => a.Ubicacion)
                .Include(a => a.Usuario)
                .Include(a => a.Tipo)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (articulo == null)
                throw new KeyNotFoundException("Articulo no encontrado");

            return new ArticuloDTO
            {
                Id = articulo.Id,
                Nombre = articulo.Nombre,
                ContenidoPorEmpaque = articulo.ContenidoPorEmpaque,
                StockMaxima = articulo.StockMaxima,
                Stock = articulo.Stock,
                StockMinima = articulo.StockMinima,
                Presentacion = articulo.Presentacion,
                Disponibilidad = articulo.Disponibilidad,
                IdMarca = articulo.IdMarca,
                IdCategoria = articulo.IdCategoria,
                UnidadDeMedida = articulo.UnidaddeMedida, // Asegúrate de que esto sea decimal
                IdUbicacion = articulo.IdUbicacion,
                IdUsuario = articulo.IdUsuario,
                IdEstado = articulo.IdEstado,
                IdTipo = articulo.IdTipo
            };
        }

        public async Task<ArticuloDTO> CreateArticulo(ArticuloDTO articuloDto)
        {
            var articulo = new Articulo
            {
                Id = 0, // Asegúrate de que el ID sea cero para evitar conflictos
                Nombre = articuloDto.Nombre,
                ContenidoPorEmpaque = articuloDto.ContenidoPorEmpaque,
                StockMaxima = articuloDto.StockMaxima,
                Stock = articuloDto.Stock,
                StockMinima = articuloDto.StockMinima,
                Presentacion = articuloDto.Presentacion,
                Disponibilidad = articuloDto.Disponibilidad,
                IdMarca = articuloDto.IdMarca,
                IdCategoria = articuloDto.IdCategoria,
                UnidaddeMedida = articuloDto.UnidadDeMedida, // Asegúrate de que esto sea decimal
                IdUbicacion = articuloDto.IdUbicacion,
                IdUsuario = articuloDto.IdUsuario,
                IdEstado = articuloDto.IdEstado,
                IdTipo = articuloDto.IdTipo
            };

            await _context.Articulo.AddAsync(articulo);
            await _context.SaveChangesAsync();

            return new ArticuloDTO
            {
                Id = articulo.Id, // El ID ahora es generado por la base de datos
                Nombre = articulo.Nombre,
                ContenidoPorEmpaque = articulo.ContenidoPorEmpaque,
                StockMaxima = articulo.StockMaxima,
                Stock = articulo.Stock,
                StockMinima = articulo.StockMinima,
                Presentacion = articulo.Presentacion,
                Disponibilidad = articulo.Disponibilidad,
                IdMarca = articulo.IdMarca,
                IdCategoria = articulo.IdCategoria,
                UnidadDeMedida=articulo.UnidaddeMedida, // Asegúrate de que esto sea decimal
               IdUbicacion=articulo.IdUbicacion, 
               IdUsuario=articulo.IdUsuario, 
               IdEstado=articulo.IdEstado, 
               IdTipo=articulo.IdTipo 
           };
       }

       public async Task UpdateArticulo(ArticuloDTO articuloDto, int id)
       {
           var articuloExistente= await _context.Articulo.FirstOrDefaultAsync(a => a.Id == id);
           if (articuloExistente == null)
               throw new KeyNotFoundException("Articulo no encontrado");

           // Mapeo desde DTO a entidad
           articuloExistente.Nombre=articuloDto.Nombre; 
           articuloExistente.ContenidoPorEmpaque=articuloDto.ContenidoPorEmpaque; 
           articuloExistente.StockMaxima=articuloDto.StockMaxima; 
           articuloExistente.Stock=articuloDto.Stock; 
           articuloExistente.StockMinima=articuloDto.StockMinima; 
           articuloExistente.Presentacion=articuloDto.Presentacion; 
           articuloExistente.Disponibilidad=articuloDto.Disponibilidad; 

           // Actualiza las claves foráneas si es necesario
           articuloExistente.IdMarca=articuloDto.IdMarca;  
           articuloExistente.IdCategoria=articuloDto.IdCategoria;  
           articuloExistente.UnidaddeMedida=articuloDto.UnidadDeMedida; // Asegúrate de que esto sea decimal
           articuloExistente.IdUbicacion=articuloDto.IdUbicacion;  
           articuloExistente.IdUsuario=articuloDto.IdUsuario;  
           articuloExistente.IdEstado=articuloDto.IdEstado;  
           articuloExistente.IdTipo=articuloDto.IdTipo;

           await _context.SaveChangesAsync();
       }

       public async Task<ArticuloDTO> SuprimirArticuloAsync(int id)
       {
           var articulo= await _context.Articulo.FirstOrDefaultAsync(a => a.Id == id);
           if (articulo == null)
               throw new KeyNotFoundException("Articulo no encontrado");

           // Cambiar el estado para suprimir (asumiendo que 2 es el estado de "suprimido")
           articulo.IdEstado= 2;
           _context.Articulo.Update(articulo);
           await _context.SaveChangesAsync();

           return new ArticuloDTO
           {
               Id=articulo.Id, 
               Nombre=articulo.Nombre, 
               ContenidoPorEmpaque=articulo.ContenidoPorEmpaque, 
               StockMaxima=articulo.StockMaxima, 
               Stock=articulo.Stock, 
               StockMinima=articulo.StockMinima, 
               Presentacion=articulo.Presentacion, 
               Disponibilidad=articulo.Disponibilidad, 
               IdMarca=articulo.IdMarca, 
               IdCategoria=articulo.IdCategoria, 
               UnidadDeMedida=articulo.UnidaddeMedida, // Asegúrate de que esto sea decimal
               IdUbicacion=articulo.IdUbicacion, 
               IdUsuario=articulo.IdUsuario, 
               IdEstado=articulo.IdEstado, 
               IdTipo=articulo.IdTipo
          };
      }
   }
}