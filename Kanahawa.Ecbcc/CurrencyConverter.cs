//
// Converter.cs
//
// Author:
//       JNaylor <>
//
// Copyright (c) 2015 JNaylor
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
using System.Xml;
using System.Xml.Linq;

namespace Kanahawa.Ecbcc
{
	public class CurrencyConverter
	{
		public Dictionary<Currency, decimal> Rates { get; private set; }

		public DateTime Day { get; private set; }

		public CurrencyConverter ()
		{
			try{
				if(!File.Exists("eurofxref-daily.xml")){
					CurrencyUpdater.Update();
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

		public decimal Convert(decimal value, Currency fromCurrency, Currency toCurrency)
		{
			if (fromCurrency == Currency.EUR) {
				return ConvertFromEur (value, toCurrency);
			}

			if (toCurrency == Currency.EUR) {
				return ConvertToEur (value, fromCurrency);
			}

			return ConvertFromEur (ConvertToEur (value, fromCurrency), toCurrency);
		}

		private decimal ConvertToEur(decimal value, Currency fromCurrency)
		{
			return value / this.Rates [fromCurrency];
		}

		private decimal ConvertFromEur(decimal value, Currency toCurrency)
		{
			return this.Rates [toCurrency] * value;
		}
	}
}

