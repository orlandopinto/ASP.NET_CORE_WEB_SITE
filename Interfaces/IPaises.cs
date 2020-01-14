using ASP.NET_CORE_CLASS_MODELS;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ASP.NET_CORE_WEB_SITE.Interfaces
{
	public interface IPaises
	{
		public Task<IEnumerable<Paises>> ListarPaises();
		public Task<bool> AgregarPais(string pais);
		public Task<Paises> ObtenerDetallePais(int PaisID);
		public Task<bool> ActualizarPais(Paises paises);
		public Task<bool> EliminarPais(int id);
	}
}
