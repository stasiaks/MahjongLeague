module Client.Tests.Utility

open Fable.Mocha
open App.Urls

// fsharplint:disable MemberNames
let createLocation endpoint =
    { new Browser.Types.Location with
        member __.assign url = ()

        member __.hash
            with get () = ""
            and set (value) = ()


        member __.protocol
            with get () = "http:"
            and set (value) = ()


        member __.port
            with get () = "8081"
            and set (value) = ()


        member __.hostname
            with get () = "mahjong.example"
            and set (value) = ()


        member __.host
            with get () = "mahjong.example:8081"
            and set (value) = ()


        member __.href
            with get () = "http://mahjong.example:8081/"
            and set (value) = ()


        member __.origin
            with get () = "http://mahjong.example:8081/"
            and set (value) = ()


        member __.password
            with get () = ""
            and set (value) = ()


        member __.pathname
            with get () = endpoint
            and set (value) = ()


        member __.search
            with get () = ""
            and set (value) = ()


        member __.username
            with get () = ""
            and set (value) = ()

        member __.reload force = ()

        member __.replace url = ()
        member this.toString() =
            sprintf "%s//%s:%s%s" this.protocol this.hostname this.port this.pathname }
// fsharplint:enable


let testUrl endpoint expectedPage =
    testCase (sprintf "\"%s\" endpoint" endpoint)
    <| fun _ ->
        let url = createLocation endpoint
        let expectedOption = expectedPage |> Some

        let actualPage = parser url

        Expect.equal actualPage expectedOption
        <| sprintf "%s should parse to %O" url.pathname expectedOption
