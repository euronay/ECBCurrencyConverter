//
// UpdaterTests.cs
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
using FluentAssertions;
using NUnit.Framework;
using System;
using System.IO;

namespace Kanahawa.Ecbcc.Tests
{
	[TestFixture ()]
	public class UpdaterTests
	{
		[SetUp ()]
		public void Setup ()
		{
			// remove rates so we can make sure they are downloaded
			if (File.Exists ("eurofxref-daily.xml")) {
				File.Delete ("eurofxref-daily.xml");
			}
		}

		[Test ()]
		public void Update ()
		{
			CurrencyConverter.Update ().Should().Be(true, "the updater should be able to download the rates file");

			File.Exists ("eurofxref-daily.xml").Should ().Be (true, "the downloaded rates file should exist");

			File.OpenText ("eurofxref-daily.xml").ReadToEnd ().Should ().StartWith ("<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n<gesmes:Envelope xmlns:gesmes=\"http://www.gesmes.org/xml/2002-08-01\"", "the downloaded rates file should be in the correct format");
		}
	}
}

