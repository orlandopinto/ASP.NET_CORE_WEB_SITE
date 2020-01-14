using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_CORE_WEB_SITE.Models
{
	public class Modelos
	{
	}
	public class Paises
	{
		public int PaisID { get; set; }
		[Required, MaxLength(50)]
		public string Pais { get; set; }
	}

	public class Regiones
	{
		[Key]
		public int RegionID { get; set; }
		public int PaisID { get; set; }
		[Required, MaxLength(50)]
		public string Region { get; set; }
	}
}
