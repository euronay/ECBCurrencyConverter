//
// Updater.cs
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
using System.Net;
using System.IO;

namespace Kanahawa.Ecbcc
{
	public class CurrencyUpdater
	{
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

