module Shared.Tests

#if FABLE_COMPILER
open Fable.Mocha
#else
open Expecto
#endif

open Shared

let extensions = testList "Extensions" [
    testCase "containsAll returns true when source has all elements" <| fun _ ->
        let values = seq { "EX1"; "EX2" }
        let source = seq { "NV1"; "NV2"; yield! values; "NV3" }
        let expected = true

        let actual = Seq.containsAll values source

        Expect.equal actual expected "Should be true"

    testCase "containsAll returns false when source has missing elements" <| fun _ ->
        let values = seq { "EX1"; "EX2" }
        let source = seq { "NV1"; "NV2"; "NV3" }
        let expected = false

        let actual = Seq.containsAll values source

        Expect.equal actual expected "Should be false"
]

let shared = testList "Shared" [
    extensions
]