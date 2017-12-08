//
// Converter.cs
//
// Author:
//       James Naylor <james@kanahawa.com>
//
// Copyright (c) 2015 James Naylor
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml;
using System.Xml.Linq;

namespace Kanahawa.Ecbcc
{
	public class CurrencyConverter
	{
		private Currency _fromCurrency = Currency.EUR;
		private Currency _toCurrency = Currency.EUR;

		public Dictionary<Currency, decimal> Rates { get; private set; }

		public DateTime Day { get; private set; }


		public CurrencyConverter ()
		{
			try{
				if(!File.Exists("eurofxref-daily.xml")){
					CurrencyConverter.Update();
				}

				// try and load our rates
				XDocument doc = XDocument.Load("eurofxref-daily.xml");

				XElement dateNode = doc.Descendants("{http://www.ecb.int/vocabulary/2002-08-01/eurofxref}Cube").First(el => el.Attribute("time") != null);

				Day = DateTime.ParseExact(dateNode.Attribute("time").Value, "yyyy-MM-dd", CultureInfo.InvariantCulture );

				Rates = new Dictionary<Currency, decimal>();

				dateNode.Elements().ToList().ForEach(el => {
					Rates.Add((Currency)Enum.Parse(typeof(Currency), el.Attribute("currency").Value), System.Convert.ToDecimal(el.Attribute("rate").Value));
				});

				// add EUR
				Rates.Add(Currency.EUR, 1M);
			}
			catch(Exception ex) {
				throw new Exception ("Could not load rates from file", ex);
			}
		}

		public CurrencyConverter From(Currency currency)
		{	
			_fromCurrency = currency;
			return this;
		}

		public CurrencyConverter To(Currency currency)
		{
			_toCurrency = currency;
			return this;
		}


		public decimal Convert(decimal value)
		{
			if (_fromCurrency == Currency.EUR) {
				return ConvertFromEur (value, _toCurrency);
			}

			if (_toCurrency == Currency.EUR) {
				return ConvertToEur (value, _fromCurrency);
			}

			return ConvertFromEur (ConvertToEur (value, _fromCurrency), _toCurrency);
		}

		private decimal ConvertToEur(decimal value, Currency fromCurrency)
		{
			return value / this.Rates [fromCurrency];
		}

		private decimal ConvertFromEur(decimal value, Currency toCurrency)
		{
			return this.Rates [toCurrency] * value;
		}

		public static bool Update()
		{
			try
			{
				// make request to get rates
				var request = WebRequest.Create("https://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml");

				// get response
				var response = request.GetResponse();

				// save response to file
				var fileStream = File.Create("eurofxref-daily.xml");
				response.GetResponseStream ().CopyTo(fileStream);
				fileStream.Close();

				return true;
			}
			catch(Exception ex)
			{
				throw new Exception("Update failed", ex);
			}
		}

	}
}

