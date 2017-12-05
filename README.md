ECB Currency Converter
------

Currency conversion library for .NET / Mono that uses the daily exchange rates published by the European Central Bank.

## Usage

Build the solution and add a reference in your project to the compiled Kanahawa.Ecbcc.dll file.

To convert Pounds sterling to US dollars:

```c#
  CurrencyConverter converter = new CurrencyConverter();
  decimal result = converter.Convert(100, Currency.GBP, Currency.USD);
```

## Getting the Source

Clone the repo:

    git clone https://github.com/euronay/ECBCurrencyConverter.git


## Licence
This code is released under the [MIT Licence](http://opensource.org/licenses/MIT)
