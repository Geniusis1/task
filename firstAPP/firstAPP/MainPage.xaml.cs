using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Text.RegularExpressions;

namespace firstAPP
{
	public partial class MainPage : ContentPage
	{
		private const string URL = "https://yastatic.net/market-export/_/partner/help/YML.xml";
		private string page;

		public IList<Offer> Offers { get; set; }

		public MainPage()
		{
			getPage();
			InitializeComponent();
			ParseInfo();
			OutInfo();
		}

		private void getPage(){
			page = new WebClient().DownloadString(URL);
		}

		private void ParseInfo()
		{
			Regex regex = new Regex(@"offer id=.[0-9]{1,}");
			MatchCollection matches1 = regex.Matches(page);
			if (matches1.Count > 0)
			{
				Offers = new List<Offer>();
				foreach (Match match1 in matches1)
				{
					regex = new Regex(@"[0-9]{1,}");
					MatchCollection matches2 = regex.Matches(match1.Value);
					foreach (Match match2 in matches2)
					{
						Offers.Add(new Offer(match2.Value));
					}
				}
			}
		}

		private void OutInfo()
		{
			BindingContext = this;
		}

	}

	public class Offer
	{
		/*	
		 *	Можно добавить дополнительные поля 
		 *	для другой нужной информации, но
		 *	для этого надо переделать метод ParseInfo
		*/
		public string ID { get; set; }

		public Offer(string ID)
		{
			this.ID = ID;
		}

		public override string ToString()
		{
			return ID;
		}
	}
}
