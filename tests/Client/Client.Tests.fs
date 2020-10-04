module Client.Tests

open Fable.Mocha

open App.State
open App.Types
open App.Urls

let client = testList "Client" [
    testCase "Default state values" <| fun _ ->
        let expectedPage = App.Types.Page.NotFound
        let expectedLocale = Locale.English

        let
            { CurrentPage = actualPage
              Locale = actualLocale } , _ = init(None)

        Expect.equal actualPage expectedPage "Default page should be NotFound"
        Expect.equal actualLocale expectedLocale "Default locale should be English"

    testList "URL parser" [

// fsharplint:disable MemberNames
        let createLocation endpoint = {
            new Browser.Types.Location with
                member this.assign url = ()
                member this.hash
                    with get () = ""
                    and set (value) = ()
                member this.protocol
                    with get () = "http:"
                    and set (value) = ()
                member this.port
                    with get () = "8081"
                    and set (value) = ()
                member this.hostname
                    with get () = "mahjong.example"
                    and set (value) = ()
                member this.host
                    with get () = "mahjong.example:8081"
                    and set (value) = ()
                member this.href
                    with get () = "http://mahjong.example:8081/"
                    and set (value) = ()
                member this.origin
                    with get () = "http://mahjong.example:8081/"
                    and set (value) = ()
                member this.password
                    with get () = ""
                    and set (value) = ()
                member this.pathname
                    with get () = endpoint
                    and set (value) = ()
                member this.search
                    with get () = ""
                    and set (value) = ()
                member this.username
                    with get () = ""
                    and set (value) = ()
                member this.reload force = ()
                member this.replace url = ()
                member this.toString () = sprintf "%s//%s:%s%s" this.protocol this.hostname this.port this.pathname
        }
// fsharplint:enable
        let testUrl endpoint expectedPage = testCase (sprintf "\"%s\" endpoint" endpoint) <| fun _ ->
                let url = createLocation endpoint
                let expectedOption = expectedPage |> Some

                let actualPage = parser url

                Expect.equal actualPage expectedOption <| sprintf "%s should parse to %O" url.pathname expectedOption
        testUrl "/" <| App.Types.Page.Home
        testUrl "/home" <| App.Types.Page.Home
        testUrl "/404" <| App.Types.Page.NotFound
        testUrl "/admin" <| App.Types.Page.Admin App.Admin.Types.Page.Dashboard
        testUrl "/admin/dashboard" <| App.Types.Page.Admin App.Admin.Types.Page.Dashboard
        testUrl "/admin/users" <| App.Types.Page.Admin (App.Admin.Types.Page.Users App.Admin.Users.Types.Page.List)
    ]
]

let all =
    testList "All"
        [
#if FABLE_COMPILER // This preprocessor directive makes editor happy
            Shared.Tests.shared
#endif
            client
        ]

[<EntryPoint>]
let main _ = Mocha.runTests all
