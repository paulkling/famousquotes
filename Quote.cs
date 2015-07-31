using FileHelpers;

namespace famousquotes
{
	[DelimitedRecord(",")]
	public class Quote {
		public int Id;
		public string Text;
		public string Author;
		
	}

}