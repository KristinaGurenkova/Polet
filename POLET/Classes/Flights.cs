using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POLET.Classes
{
	public class Flights
	{
		public int idflight { get; set; }
		public int numberseats { get; set; }
		public string fromflight { get; set; }
		public string whereflight { get; set; }
		public DateTime datefrom { get; set; }
		public TimeSpan timefrom { get; set; } 
		public DateTime datewhere { get; set; }
		public TimeSpan timewhere { get; set; }
		public int? idtransfer { get; set; }
		public Transfers Transfers { get; set; }
		public int price { get; set; }
	}
}
