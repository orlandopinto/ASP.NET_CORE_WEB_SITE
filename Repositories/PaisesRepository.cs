using ASP.NET_CORE_CLASS_MODELS;
using ASP.NET_CORE_WEB_SITE.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ASP.NET_CORE_WEB_SITE.Repositories
{
	public class PaisesRepository : IPaises
	{
		List<Paises> paises = new List<Paises>();
		Paises pais = new Paises();
		HttpClient client = new HttpClient();

		public PaisesRepository() => client.BaseAddress = new Uri(@"https://localhost:44309/api/");

		public async Task<IEnumerable<Paises>> ListarPaises()
		{
			paises = new List<Paises>();
			using (var response = await client.GetAsync(@"Paises"))
			{
				string apiResponse = await response.Content.ReadAsStringAsync();
				paises = JsonConvert.DeserializeObject<List<Paises>>(apiResponse);
			}
			client.Dispose();
			return paises.ToList();
		}

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

		public async Task<bool> ActualizarPais(Paises paises)
		{
			var content = JsonConvert.SerializeObject(paises);
			var httpResponse = await client.PutAsync(@"Paises/" + paises.PaisID, new StringContent(content, Encoding.Default, @"application/json"));
			return httpResponse.IsSuccessStatusCode;
		}

		public async Task<bool> AgregarPais(string pais)
		{
			Paises paises = new Paises() { PaisID = 0, Pais = pais };
			string stringData = JsonConvert.SerializeObject(paises);
			var contentData = new StringContent(stringData, Encoding.UTF8, @"application/json");
			HttpResponseMessage res = await client.PostAsync(@"Paises", contentData);
			return res.IsSuccessStatusCode;
		}

		public async Task<bool> EliminarPais(int id)
		{
			HttpResponseMessage response = await client.DeleteAsync(@"Paises/" + id);
			return response.IsSuccessStatusCode;
		}
	}
}
