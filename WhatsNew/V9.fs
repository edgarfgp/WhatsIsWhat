module WhatsNew.V9

open System
open System.Collections.Generic
open System.Runtime.CompilerServices

// Enforce AttributeTargets on structs and classes https://github.com/dotnet/fsharp/pull/16790

[<AttributeUsage(AttributeTargets.Struct)>]
type CustomStructAttribute() =
    inherit Attribute()

[<AttributeUsage(AttributeTargets.Class)>]
type CustomClassAttribute() =
    inherit Attribute()

[<CustomStruct>]
type Class(x: int) = class end

[<CustomClass>]
type Class2(x: int) = class end

[<CustomStruct; CustomClass>]
type Class3(x: int) = class end

[<Class; CustomStruct>]
type Class4 = class end


// Enforce attribute targets on functions and values https://github.com/dotnet/fsharp/pull/16692 
[<AttributeUsage(AttributeTargets.Method)>]
type MethodOnlyAttribute() = 
  inherit System.Attribute()

[<MethodOnly>]
let someFunction () = "abc"

[<MethodOnly>]
let someValue =  "def"

[<AttributeUsage(AttributeTargets.Field)>]
type FieldOnlyAttribute() = 
  inherit System.Attribute()

[<FieldOnly>]
let someFunction2 () = "abc"

[<FieldOnly>]
let someValue2 =  "def"

type IEnumerableExtensions =
    [<Extension>]
    static member inline Sum(xs: IEnumerable<'T>) = Seq.sum xs

let f (x: IEnumerable<int>) = x.Sum()

// Support empty-bodied computation expressions https://github.com/dotnet/fsharp/pull/17352
type Builder () =
    member _.Zero () = Seq.empty
    member _.Delay f = f
    member _.Run f = f ()
            
let builder = Builder ()

let x = builder { }


#nowarn 57
#nowarn 0057
#nowarn FS0057

#nowarn "57"
#nowarn "0057"
#nowarn "FS0057"


[<TailCall>]
let someNonRecFun x = x + x

[<TailCall>]
let someX = 23

[<TailCall>]
let rec someRecLetBoundValue = nameof(someRecLetBoundValue)

// Boolean-returning and return-type-directed partial active patterns https://github.com/dotnet/fsharp/pull/16473
let (|IsA|_|) x = x = "A"

let s =
    match "A" with 
    | IsA -> "A"
    | _ -> "Not A"


// Make .Is* discriminated union properties visible https://github.com/dotnet/fsharp/pull/16341
type Contact =
    | Email of address: string
    | Phone of countryCode: int * number: string

type Person = { name: string; contact: Contact }

let canSendEmailTo person =
    person.contact.IsEmail
    
    
    
let allPlayers = [ "Alice"; "Bob"; "Charlie"; "Dave" ]
let round1Order = allPlayers |> List.randomShuffle // [ "Charlie"; "Dave"; "Alice"; "Bob" ]

/// Better error reporting for let bindings. https://github.com/dotnet/fsharp/pull/17601
[<VolatileField>]
let mutable x = 6

// Improve active pattern error reporting https://github.com/dotnet/fsharp/pull/17666
let (|One|Two|Three|Four|Five|Six|Seven|Eight|) x = One

let (|A|B|C|D|E|F|G|H|) x =
    match x with
    | 0 -> A
    | 1 -> B
    | 2 -> C
    | 3 -> D
    | 4 -> E
    | 5 -> F
    | 6 -> G
    | _ -> H
    
let (|A|B|_|) = None  // FS3872: Multi-case partial active patterns are not supported. Consider using a single-case partial active pattern or a full active pattern.

