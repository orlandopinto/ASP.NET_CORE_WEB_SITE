using ASP.NET_CORE_CLASS_MODELS;
using ASP.NET_CORE_WEB_SITE.Infraestructure;
using ASP.NET_CORE_WEB_SITE.Repositories;
using ASP.NET_CORE_WEB_SITE.Settings;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using X.PagedList;

namespace ASP.NET_CORE_WEB_SITE.Controllers
{
	public class PaisesController : Controller
	{
		#region ..:: [ VARIABLES ] ::..

		PaisesRepository repository;
		const int DefaultPageSize = 2;
		static PagedList<Paises> model;

		#endregion

		#region ..:: [ CONSTRUCTORS ] ::..

		public PaisesController(SettingsStoreApp settings) => repository = new PaisesRepository(settings.StoreAppSettings.WebApiBaseUrl);

		#endregion

		#region ..:: [ ActionResults ] ::..

		public async Task<ActionResult> Index(int page = 1, int pageSize = 2)
		{
			try
			{
				var lista = await repository.ListarPaises();
				var newList = lista.ToPagedList(page, DefaultPageSize);
				model = (PagedList<Paises>)newList;
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
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(string Pais)
		{
			try
			{
				if (await repository.AgregarPais(Pais))
				{
					ViewData[@"TitleMessage"] = @"Nuevo país";
					ViewData[@"Message"] = @"El registro se ha agregado satisfactoriamente";
				}
				else
				{
					ViewData[@"TitleMessage"] = ViewData[@"Error"] = @"Error";
					ViewData[@"Message"] = @"Se produjo un error al agregar el país";
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
			try
			{
				return View(await repository.ObtenerDetallePais(id));
			}
			catch (Exception ex)
			{
				ShowMessages(@"Error", ex.Message);
			}
			return View(new Paises());
		}

		[HttpPost]
		public async Task<IActionResult> Edit(Paises paises)
		{
			try
			{
				if (await repository.ActualizarPais(paises))
				{
					ViewData[@"TitleMessage"] = @"Actualización país";
					ViewData[@"Message"] = @"El registro se ha actualizado satisfactoriamente";
				}
				else
				{
					ViewData[@"Message"] = @"Se produjo un error al actualizar el país";
					ViewData[@"TitleMessage"] = ViewData[@"Error"] = @"Error";
				}
			}
			catch (Exception ex)
			{
				ShowMessages("@Error", ex.Message);
			}
			return View(paises);
		}

		[HttpDelete]
		public async Task<JsonResult> Delete(int id)
		{
			try
			{
				return Json(await repository.EliminarPais(id));
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
			PagedList<Paises> pages = model;
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