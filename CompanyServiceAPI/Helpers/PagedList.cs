using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CompanyServiceAPI.Helpers
{
	public class PagedList<T> : List<T>
	{
		public PagedList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
		{
			CurrentPage = pageNumber;
			TotalPages = (int) Math.Ceiling(count / (double) pageSize);
			PageSize = pageSize;
		}

		public int CurrentPage { get; set; }
		public int TotalPages { get; set; }
		public int PageSize { get; set; }
		public int TotalCount { get; set; }
	}
}
