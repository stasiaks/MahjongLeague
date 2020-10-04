module Shared.Tests

#if FABLE_COMPILER
open Fable.Mocha
type T =
    static member Test x = Expect.isTrue x "Quotes are not currently supported by Fable compiler. Can't unquote test."
#else
open Expecto
type T =
    static member Test ([<ReflectedDefinition(false)>] x:Quotations.Expr<bool>) = Swensen.Unquote.Assertions.test x
#endif

open Shared

let extensions = testList "Extensions" [
    testCase "containsAll returns true when source has all elements" <| fun _ ->
        let values = seq { "EX1"; "EX2" }
        let source = seq { "NV1"; "NV2"; yield! values; "NV3" }

        T.Test ((Seq.containsAll values source))

    testCase "containsAll returns false when source has missing elements" <| fun _ ->
        let values = seq { "EX1"; "EX2" }
        let source = seq { "NV1"; "NV2"; "NV3" }

        T.Test ((Seq.containsAll values source |> not))
]

let shared = testList "Shared" [
    extensions
]
