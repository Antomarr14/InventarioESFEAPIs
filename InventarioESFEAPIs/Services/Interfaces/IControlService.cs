using InventarioESFEAPIs.Models;

namespace InventarioESFEAPIs.Services.Interfaces
{
    public interface IControlService
    {
        Task<IEnumerable<Control>> GetControl();
        Task<Control> GetControlById(int Id);
        Task<Control> CreateControl(Control control);
        Task<Control> UpdateControl(Control control, int Id);
        Task<Control> DeleteControl(int Id);
    }
}
