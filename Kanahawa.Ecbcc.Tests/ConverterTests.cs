//
// ConverterTests.cs
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
using FluentAssertions;
using NUnit.Framework;
using System;
using System.IO;
using System.Reflection;

namespace Kanahawa.Ecbcc.Tests
{
	[TestFixture ()]
	public class ConverterTests
	{
		CurrencyConverter converter;

		[SetUp()]
		public void Setup ()
		{
			// remove any existing rates
			if (File.Exists ("eurofxref-daily.xml")) {
				File.Delete ("eurofxref-daily.xml");
			}

			// we package up a fixed version of the rates file with the tests so we can test conversions (rates change daily!)
			var setDateRefs = Assembly.GetExecutingAssembly ().GetManifestResourceStream ("Kanahawa.Ecbcc.Tests.eurofxref-daily.xml");

			var fileStream = File.Create("eurofxref-daily.xml");
			setDateRefs.CopyTo(fileStream);
			fileStream.Close();

			converter = new CurrencyConverter ();
		}

		[Test ()]
		public void Date ()
		{
			converter.Day.Should ().Be (new DateTime (2015, 02, 02), "test rates are from 2015-02-02");
		}

		[Test ()]
		public void RateCount()
		{
			converter.Rates.Count.Should ().BeGreaterThan (0, "there should be more than 0 rates");
		}

		[Test]
		public void EurToEur()
		{
			converter.Convert (1M, Currency.EUR, Currency.EUR).Should ().Be (1M);
		}

		[Test]
		public void EurToGbp()
		{
			converter.Convert (1M, Currency.EUR, Currency.GBP).Should ().Be (0.75260M);
		}

		[Test]
		public void GbpToEur()
		{
			converter.Convert (1M, Currency.GBP, Currency.EUR).Should ().BeApproximately (1.3287M, 0.0001M);
		}

		[Test]
		public void GbpToGbp()
		{
			converter.Convert (1M, Currency.GBP, Currency.GBP).Should ().Be (1M);
		}

		[Test]
		public void GbpToUsd()
		{
			converter.Convert (1, Currency.GBP, Currency.USD).Should ().BeApproximately (1.5028M, 0.0001M);
		}
			
	}
}

