//
// Currency.cs
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
using System.ComponentModel;

namespace Kanahawa.Ecbcc
{
	public enum Currency
	{
		[Description("Euro")]
		EUR,
		[Description("US Dollar")]
		USD,
		[Description("Japanese yen")]
		JPY,
		[Description("Bulgarian lev")]
		BGN,
		[Description("Czech koruna")]
		CZK,
		[Description("Danish krone")]
		DKK,
		[Description("Pound sterling")]
		GBP,
		[Description("Hungarian forint")]
		HUF,
		[Description("Polish zloty")]
		PLN,
		[Description("Romanian leu")]
		RON,
		[Description("Swedish krona")]
		SEK,
		[Description("Swiss franc")]
		CHF,
		[Description("Norwegian krone")]
		NOK,
		[Description("Croatian kuna")]
		HRK,
		[Description("Russian rouble")]
		RUB,
		[Description("Turkish lira")]
		TRY,
		[Description("Australian dollar")]
		AUD,
		[Description("Brasilian real")]
		BRL,
		[Description("Canadian dollar")]
		CAD,
		[Description("Chinese yuan renminbi")]
		CNY,
		[Description("Hong Kong dollar")]
		HKD,
		[Description("Indonesian rupiah")]
		IDR,
		[Description("Israeli shekel")]
		ILS,
		[Description("Indian rupee")]
		INR,
		[Description("South Korean won")]
		KRW,
		[Description("Mexican peso")]
		MXN,
		[Description("Malaysian ringgit")]
		MYR,
		[Description("New Zealand dollar")]
		NZD,
		[Description("Philippine peso")]
		PHP,
		[Description("Singapore dollar")]
		SGD,
		[Description("Thai baht")]
		THB,
		[Description("South African rand")]
		ZAR
	}
}

