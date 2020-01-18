using ASP.NET_CORE_CLASS_MODELS;
using ASP.NET_CORE_WEB_SITE.Infraestructure;
using ASP.NET_CORE_WEB_SITE.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using X.PagedList;
using ASP.NET_CORE_WEB_SITE.Extensions;
using ASP.NET_CORE_WEB_SITE.Settings;

namespace ASP.NET_CORE_WEB_SITE.Controllers
{
	public class RegionesController : Controller
	{
		#region ..:: [ VARIABLES ] ::..

		RegionesRepository repository;
		PaisesRepository pais_repository;
		const int DefaultPageSize = 2;
		static PagedList<Regiones> model;
		HttpContext context;

		#endregion

		#region ..:: [ CONSTRUCTOR ] ::..

		public RegionesController(IHttpContextAccessor httpContextAccessor, SettingsStoreApp settings)
		{
			repository = new RegionesRepository(settings.StoreAppSettings.WebApiBaseUrl);
			pais_repository = new PaisesRepository(settings.StoreAppSettings.WebApiBaseUrl);
			context = httpContextAccessor.HttpContext;
		}

		#endregion

		#region ..:: [ ActionResults ] ::..

		public async Task<ActionResult> Index(int page = 1, int pageSize = 2)
		{
			try
			{
				IEnumerable<Paises> paises = await pais_repository.ListarPaises();
				context.Session.Set(@"ListaPaises", paises as List<Paises>);
				ViewData[@"Paises"] = paises as List<Paises>;
				var lista = await repository.ListarRegiones();
				var newList = lista.ToPagedList(page, DefaultPageSize);
				model = (PagedList<Regiones>)newList;
				return View(model);
			}
			catch (Exception ex)
			{
				ShowMessages("@Error", ex.Message);
			}
			return View();
		}

		public IActionResult Create()
		{
			ViewData["Paises"] = HttpContext.Session.Get<IEnumerable<Paises>>("ListaPaises");
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(string Region, int paisId)
		{
			try
			{
				ViewData["Paises"] = HttpContext.Session.Get<IEnumerable<Paises>>(@"ListaPaises");
				if (await repository.AgregarRegion(Region, paisId))
				{
					ViewData[@"TitleMessage"] = @"Nueva región";
					ViewData[@"Message"] = @"El registro se ha agregado satisfactoriamente";
				}
				else
				{
					ViewData[@"Message"] = @"Se produjo un error al agregar la región";
					ViewData[@"TitleMessage"] = ViewData[@"Error"] = @"Error";
				}
			}
			catch (Exception ex)
			{
				ShowMessages("@Error", ex.Message);
			}
			return View();
		}

		public async Task<IActionResult> Edit(int id)
		{
			ViewData[@"Paises"] = HttpContext.Session.Get<IEnumerable<Paises>>(@"ListaPaises");
			try
			{
				var result = await repository.ObtenerDetalleRegion(id);
				return View(result);
			}
			catch (Exception ex)
			{
				ShowMessages(@"Error", ex.Message);
			}
			return View(new Regiones());
		}

		[HttpPost]
		public async Task<IActionResult> Edit(Regiones regiones)
		{
			try
			{
				ViewData[@"Paises"] = HttpContext.Session.Get<IEnumerable<Paises>>(@"ListaPaises");
				if (await repository.ActualizarRegion(regiones))
				{
					ViewData[@"TitleMessage"] = @"Actualización región";
					ViewData[@"Message"] = @"El registro se ha actualizado satisfactoriamente";
				}
				else
				{
					ViewData[@"Message"] = @"Se produjo un error al actualizar la región";
					ViewData[@"TitleMessage"] = ViewData[@"Error"] = @"Error";
				}
			}
			catch (Exception ex)
			{
				ShowMessages("@Error", ex.Message);
			}
			return View(regiones);
		}

		[HttpDelete]
		public async Task<JsonResult> Delete(int id)
		{
			try
			{
				return Json(await repository.EliminarRegion(id));
			}
			catch (Exception ex)
			{
				ShowMessages("@Error", ex.Message);
			}
			return Json(false);
		}
		#endregion

		#region ..:: [ METHODS ] ::..

		public JsonResult getPagedList()
		{
			PagedList<Regiones> pages = model;
			PaginationModel Pagination_Model = new PaginationModel();
			if (pages != null) Pagination_Model = new PaginationModel() { hasNextPage = pages.HasNextPage, hasPreviousPage = pages.HasPreviousPage, pageCount = pages.PageCount, pageSize = pages.PageSize, page = pages.PageNumber };
			else Pagination_Model = null;
			return Json(Pagination_Model);
		}

		private void ShowMessages(string Title, string Message)
		{
			ViewData[@"Message"] = Message;
			ViewData[@"TitleMessage"] = ViewData[@"Error"] = Title;
		} 

		#endregion
	}
}