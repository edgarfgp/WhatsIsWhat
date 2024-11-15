module WhatsNew.Preview

// Currently in Preview
// Allow object expression without overrides https://github.com/dotnet/fsharp/pull/17387

[<AbstractClass>]
type AbstractClass() = class end

// Ok
let objExpr = { new AbstractClass() }

type Class11() = class end

// Ok
let objExpr2 = { new Class11() }