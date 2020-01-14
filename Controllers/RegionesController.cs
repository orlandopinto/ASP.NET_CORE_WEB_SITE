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

namespace ASP.NET_CORE_WEB_SITE.Controllers
{
	public class RegionesController : Controller
	{
		RegionesRepository repository = new RegionesRepository();
		PaisesRepository pais_repository = new PaisesRepository();
		const int DefaultPageSize = 2;
		static PagedList<Regiones> model;
		HttpContext context;
		public RegionesController(IHttpContextAccessor httpContextAccessor)
		{
			context = httpContextAccessor.HttpContext;
			ViewData[@"Message"] = @"";
		}

		public async Task<ActionResult> Index(int page = 1, int pageSize = 2)
		{
			try
			{
				var lista = await repository.ListarRegiones();
				IEnumerable<Paises> paises = await pais_repository.ListarPaises();
				context.Session.Set<List<Paises>>("ListaPaises", paises as List<Paises>);
				ViewData["Paises"] = paises as List<Paises>;
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
				ViewData["Paises"] = HttpContext.Session.Get<IEnumerable<Paises>>("ListaPaises");
				bool result = await repository.AgregarRegion(Region, paisId);
				if (result)
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
			Regiones result = new Regiones();
			try
			{
				result = await repository.ObtenerDetalleRegion(id);
			}
			catch (Exception ex)
			{
				ShowMessages(@"Error", ex.Message);
			}
			return View(result);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(Regiones regiones)
		{
			try
			{
				bool result = await repository.ActualizarRegion(regiones);
				if (result)
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
			bool result = false;
			try
			{
				result = await repository.EliminarRegion(id);
			}
			catch (Exception ex)
			{
				ShowMessages("@Error", ex.Message);
			}
			return Json(result);
		}

		public JsonResult getPagedList()
		{
			PagedList<Regiones> pages = (PagedList<Regiones>)model;
			PaginationModel Pagination_Model = new PaginationModel() { hasNextPage = pages.HasNextPage, hasPreviousPage = pages.HasPreviousPage, pageCount = pages.PageCount, pageSize = pages.PageSize, page = pages.PageNumber };
			return Json(Pagination_Model);
		}

		private void ShowMessages(string Title, string Message)
		{
			ViewData[@"Message"] = Message;
			ViewData[@"TitleMessage"] = ViewData[@"Error"] = Title;
		}

	}
}