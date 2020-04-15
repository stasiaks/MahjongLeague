module App.Urls

open Elmish
open Elmish.UrlParser
open Elmish.Navigation

open App.Types

let homeSegment = "home"
let adminSegment = "admin"

let toUrl page =
    match page with
    | Home -> homeSegment
    | Admin t -> sprintf "%s/%s" <| adminSegment <| Admin.Urls.toUrl t
    |> (+) "/"

let parser: Parser<Page option> =
    oneOf
        [ map Home (s homeSegment)
          map Admin (s adminSegment </> Admin.Urls.parseSegment) ]
    |> parsePath

let urlUpdate route state =
    match route with
    | Some page -> state, Cmd.ofMsg (NavigateTo page)
    | None -> state, Cmd.none
