//
// ConverterTests.cs
//
// Author:
//       James Naylor <james@kanahawa.com>
//
// Copyright(c) 2015 James Naylor
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files(the "Software"), to deal
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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Reflection;
using FluentAssertions;

namespace Kanahawa.Ecbcc.Tests
{
	[TestClass()]
	public class ConverterTests
	{
		[TestMethod()]
		public void EurToEur()
		{
			new CurrencyConverter().From(Currency.EUR).To(Currency.EUR).Convert(1M).Should().Be(1M);
		}

		[TestMethod()]
		public void EurToGbp()
		{
			new CurrencyConverter().From(Currency.EUR).To(Currency.GBP).Convert(1M).Should().Be(0.75260M);
		}

		[TestMethod()]
		public void GbpToEur()
		{
			new CurrencyConverter().From(Currency.GBP).To(Currency.EUR).Convert(1M).Should().BeApproximately(1.3287M, 0.0001M);
		}

		[TestMethod()]
		public void GbpToGbp()
		{
			new CurrencyConverter().From(Currency.GBP).To(Currency.GBP).Convert(1M).Should().Be(1M);
		}

		[TestMethod()]
		public void GbpToUsd()
		{
			new CurrencyConverter().From(Currency.GBP).To(Currency.USD).Convert(1M).Should().BeApproximately(1.5028M, 0.0001M);
		}

		[TestMethod()]
		public void TestDescription()
		{
			Currency.EUR.Description().Should().Be("Euro", "extension method should provide description for currency");
		}
			
	}
}

