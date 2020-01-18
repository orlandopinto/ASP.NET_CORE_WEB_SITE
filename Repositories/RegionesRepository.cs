using ASP.NET_CORE_CLASS_MODELS;
using ASP.NET_CORE_WEB_SITE.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace ASP.NET_CORE_WEB_SITE.Repositories
{
	public class RegionesRepository : IRegiones
	{
		#region ..:: [ VARIABLES ] ::..

		List<Regiones> regiones = new List<Regiones>();
		Regiones region = new Regiones();
		HttpClient client = new HttpClient();

		#endregion

		#region ..:: [ CONSTRUCTOR ] ::..

		public RegionesRepository(string baseAddress = null) => client = new HttpClient(new HttpClientHandler() { ClientCertificateOptions = ClientCertificateOption.Automatic, SslProtocols = SslProtocols.Tls12 }) { BaseAddress = new Uri(baseAddress) };

		#endregion

		#region ..:: [ METHODS ] ::..

		/// <summary>
		/// Metodo que devuel las Regiones en un objeto
		/// </summary>
		/// <returns>IEnumerable<Regiones>Regiones</returns>
		public async Task<IEnumerable<Regiones>> ListarRegiones()
		{
			using (var response = await client.GetAsync(@"Regiones"))
			{
				regiones = JsonConvert.DeserializeObject<List<Regiones>>(await response.Content.ReadAsStringAsync());
			}
			client.Dispose();
			return regiones.ToList();
		}

		/// <summary>
		/// Deveuelve el detalle de una región
		/// </summary>
		/// <param name="RegionID"></param>
		/// <returns>Regiones</returns>
		public async Task<Regiones> ObtenerDetalleRegion(int RegionID)
		{
			using (var response = await client.GetAsync(@"Regiones/" + RegionID))
			{
				region = JsonConvert.DeserializeObject<Regiones>(await response.Content.ReadAsStringAsync());
			}
			client.Dispose();
			return region;
		}

		/// <summary>
		/// Actualiza los datos de una región
		/// </summary>
		/// <param name="region"></param>
		/// <returns>bool</returns>
		public async Task<bool> ActualizarRegion(Regiones region) => (await client.PutAsync(@"Regiones/" + region.RegionID, new StringContent(JsonConvert.SerializeObject(region), Encoding.Default, @"application/json"))).IsSuccessStatusCode;

		/// <summary>
		/// Agrega una nueva region
		/// </summary>
		/// <param name="region">Nombre de la Región</param>
		/// <param name="paisId">ID del País</param>
		/// <returns></returns>
		public async Task<bool> AgregarRegion(string region, int paisId) => (await client.PostAsync(@"Regiones", new StringContent(JsonConvert.SerializeObject(new Regiones() { RegionID = 0, Region = region, PaisID = paisId }), Encoding.UTF8, @"application/json"))).IsSuccessStatusCode;

		/// <summary>
		/// Metodo que elimina una región
		/// </summary>
		/// <param name="id">ID de la Región</param>
		/// <returns></returns>
		public async Task<bool> EliminarRegion(int id) => (await client.DeleteAsync(@"Regiones/" + id)).IsSuccessStatusCode; 

		#endregion
	}
}
