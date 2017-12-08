ECB Currency Converter
------

[![Build status](https://ci.appveyor.com/api/projects/status/qlqclv1pcp44b60d?svg=true)](https://ci.appveyor.com/project/euronay/ecbcurrencyconverter)

Currency conversion library for .NET / Mono that uses the daily exchange rates published by the European Central Bank.

## Usage

Build the solution and add a reference in your project to the compiled Kanahawa.Ecbcc.dll file.

To convert £100 Pounds sterling to US dollars:

```c#
  decimal result = new CurrencyConverter().From(Currency.GBP).To(Currency.USD).Convert(100);
```

## Getting the Source

Clone the repo:

    git clone https://github.com/euronay/ECBCurrencyConverter.git


## Licence
This code is released under the [MIT Licence](http://opensource.org/licenses/MIT)
