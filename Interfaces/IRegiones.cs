using ASP.NET_CORE_CLASS_MODELS;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ASP.NET_CORE_WEB_SITE.Interfaces
{
	public interface IRegiones
	{
		public Task<IEnumerable<Regiones>> ListarRegiones();
		public Task<bool> AgregarRegion(string pais, int paisId);
		public Task<Regiones> ObtenerDetalleRegion(int RegionID);
		public Task<bool> ActualizarRegion(Regiones region);
		public Task<bool> EliminarRegion(int id);
	}
}
