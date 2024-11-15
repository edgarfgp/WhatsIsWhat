module WhatsOld.V8

open System
open System.Net.Http

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


open System

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

open System.Collections.Generic
open System.Runtime.CompilerServices

[<Extension>]
type IEnumerableExtensions =
    [<Extension>]
    static member inline Sum(xs: IEnumerable<'T>) = Seq.sum xs

let f (x: IEnumerable<int>) = x.Sum()


[<AbstractClass>]
type AbstractClass() = class end

// Ok
let objExpr = { new AbstractClass() }

type Class() = class end

// Ok
let objExpr = { new Class() }


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


let (|IsA|_|) x = x = "A"

let s =
    match "A" with 
    | IsA -> "A"
    | _ -> "Not A"
    
let (|IsB|_|) x =
    if x = "B" then Some() else None
    
let t =
    match "B" with
    | IsB -> "B"
    | _ -> "Not B"
    
    
type Contact =
    | Email of address: string
    | Phone of countryCode: int * number: string

type Person = { name: string; contact: Contact }

let canSendEmailTo person =
    person.contact.IsEmail