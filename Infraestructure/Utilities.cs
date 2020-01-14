using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_CORE_WEB_SITE.Infraestructure
{
	public class Utilities
	{
	}

	public class PaginationModel
	{
		public bool hasNextPage { get; set; }
		public bool hasPreviousPage { get; set; }
		public int pageCount { get; set; }
		public int pageSize { get; set; }
		public int page { get; set; }
	}
}
