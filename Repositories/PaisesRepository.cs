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
	public class PaisesRepository : IPaises
	{
		#region ..:: [ VARIABLES ] ::..

		List<Paises> paises = new List<Paises>();
		Paises pais = new Paises();
		HttpClient client = new HttpClient();

		#endregion

		#region ..:: [ CONSTRUCTOR ] ::..

		public PaisesRepository(string baseAddress = null)
		{
			var handler = new HttpClientHandler() { ClientCertificateOptions = ClientCertificateOption.Automatic, SslProtocols = SslProtocols.Tls12 };
			client = new HttpClient(handler);
			client.BaseAddress = new Uri(baseAddress);
		}

		#endregion

		#region ..:: [ METHODS ] ::..

		/// <summary>
		/// Devuelve un objeto de paises
		/// </summary>
		/// <returns>IEnumerable<Paises></returns>
		public async Task<IEnumerable<Paises>> ListarPaises()
		{
			using (var response = await client.GetAsync(@"Paises"))
			{
				paises = JsonConvert.DeserializeObject<List<Paises>>(await response.Content.ReadAsStringAsync());
			}
			client.Dispose();
			return paises.ToList();
		}

		/// <summary>
		/// Metodo que obtiene los datos de un pais
		/// </summary>
		/// <param name="PaisID"></param>
		/// <returns>Paises</returns>
		public async Task<Paises> ObtenerDetallePais(int PaisID)
		{
			using (var response = await client.GetAsync(@"Paises/" + PaisID))
			{
				string apiResponse = await response.Content.ReadAsStringAsync();
				pais = JsonConvert.DeserializeObject<Paises>(apiResponse);
			}
			client.Dispose();
			return pais;
		}

		/// <summary>
		/// ACtualiza un pais
		/// </summary>
		/// <param name="paises">Objeto List<Paises></param>
		/// <returns>bool</returns>
		public async Task<bool> ActualizarPais(Paises paises)
		{
			var httpResponse = await client.PutAsync(@"Paises/" + paises.PaisID, new StringContent(JsonConvert.SerializeObject(paises), Encoding.Default, @"application/json"));
			return httpResponse.IsSuccessStatusCode;
		}

		/// <summary>
		/// Agrega un nuevo paise
		/// </summary>
		/// <param name="pais">Nombre del país</param>
		/// <returns></returns>
		public async Task<bool> AgregarPais(string pais)
		{
			var contentData = new StringContent(JsonConvert.SerializeObject(new Paises() { PaisID = 0, Pais = pais }), Encoding.UTF8, @"application/json");
			HttpResponseMessage res = await client.PostAsync(@"Paises", contentData);
			return res.IsSuccessStatusCode;
		}

		/// <summary>
		/// Metodo que elimina el pais
		/// </summary>
		/// <param name="id">ID del País</param>
		/// <returns></returns>
		public async Task<bool> EliminarPais(int id)
		{
			HttpResponseMessage response = await client.DeleteAsync(@"Paises/" + id);
			return response.IsSuccessStatusCode;
		} 

		#endregion
	}
}
