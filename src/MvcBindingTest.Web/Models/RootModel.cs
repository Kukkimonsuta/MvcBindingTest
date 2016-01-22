using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcBindingTest.Web.Models
{
	public class RootModel
	{
		public string Foo { get; set; }

		public IEnumerable<ChildModel> ChildModel { get; set; }
	}
}
