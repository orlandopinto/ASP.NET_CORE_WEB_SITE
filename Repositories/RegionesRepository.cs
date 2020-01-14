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
	public class RegionesRepository : IRegiones
	{
		List<Regiones> regiones = new List<Regiones>();
		Regiones region = new Regiones();
		HttpClient client = new HttpClient();

		public RegionesRepository() => client.BaseAddress = new Uri(@"https://localhost:44309/api/");

		public async Task<IEnumerable<Regiones>> ListarRegiones()
		{
			regiones = new List<Regiones>();
			using (var response = await client.GetAsync(@"Regiones"))
			{
				string apiResponse = await response.Content.ReadAsStringAsync();
				regiones = JsonConvert.DeserializeObject<List<Regiones>>(apiResponse);
			}
			client.Dispose();
			return regiones.ToList();
		}

		public async Task<Regiones> ObtenerDetalleRegion(int RegionID)
		{
			using (var response = await client.GetAsync(@"Regiones/" + RegionID))
			{
				string apiResponse = await response.Content.ReadAsStringAsync();
				region = JsonConvert.DeserializeObject<Regiones>(apiResponse);
			}
			client.Dispose();
			return region;
		}

		public async Task<bool> ActualizarRegion(Regiones region)
		{
			var content = JsonConvert.SerializeObject(region);
			var httpResponse = await client.PutAsync(@"Regiones/" + region.RegionID, new StringContent(content, Encoding.Default, @"application/json"));
			return httpResponse.IsSuccessStatusCode;
		}

		public async Task<bool> AgregarRegion(string region, int paisId)
		{
			Regiones paises = new Regiones() { RegionID = 0, Region = region, PaisID = paisId  };
			string stringData = JsonConvert.SerializeObject(paises);
			var contentData = new StringContent(stringData, Encoding.UTF8, @"application/json");
			HttpResponseMessage res = await client.PostAsync(@"Regiones", contentData);
			return res.IsSuccessStatusCode;
		}

		public async Task<bool> EliminarRegion(int id)
		{
			HttpResponseMessage response = await client.DeleteAsync(@"Regiones/" + id);
			return response.IsSuccessStatusCode;
		}
	}
}
