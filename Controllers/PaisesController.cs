using ASP.NET_CORE_CLASS_MODELS;
using ASP.NET_CORE_WEB_SITE.Infraestructure;
using ASP.NET_CORE_WEB_SITE.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using X.PagedList;

namespace ASP.NET_CORE_WEB_SITE.Controllers
{
	public class PaisesController : Controller
	{
		PaisesRepository repository = new PaisesRepository();
		const int DefaultPageSize = 2;
		static PagedList<Paises> model;

		public PaisesController()
		{
			ViewData[@"Message"] = @"";
		}

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
				bool result = await repository.AgregarPais(Pais);
				if (result)
				{
					ViewData[@"TitleMessage"] = @"Nuevo país";
					ViewData[@"Message"] = @"El registro se ha agregado satisfactoriamente";
				}
				else
				{
					ViewData[@"Message"] = @"Se produjo un error al agregar el país";
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
			Paises result = new Paises();
			try
			{
				result = await repository.ObtenerDetallePais(id);
			}
			catch (Exception ex)
			{
				ShowMessages(@"Error", ex.Message);
			}
			return View(result);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(Paises paises)
		{
			try
			{
				bool result = await repository.ActualizarPais(paises);
				if (result)
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
			bool result = false;
			try
			{
				result = await repository.EliminarPais(id);
			}
			catch (Exception ex)
			{
				ShowMessages("@Error", ex.Message);
			}
			return Json(result);
		}

		public JsonResult getPagedList()
		{
			PagedList<Paises> pages = (PagedList<Paises>)model;
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