module App.Urls

open Elmish
open Elmish.UrlParser
open Elmish.Navigation

open App.Types

let homeSegment = "home"
let adminSegment = "admin"
let notFoundSegment = "404"

let toUrl page =
    match page with
    | Page.Home -> homeSegment
    | Page.Admin t -> sprintf "%s/%s" <| adminSegment <| Admin.Urls.toUrl t
    | Page.NotFound -> notFoundSegment
    |> (+) "/"

let parser: Parser<Page option> =
    oneOf
        [ map Page.Home (s "") // Default page
          map Page.Home (s homeSegment)
          map (Page.Admin Admin.Types.Page.Dashboard) (s adminSegment) // Default for incomplete URL
          map Page.Admin (s adminSegment </> Admin.Urls.parseSegment)
          map Page.NotFound (s notFoundSegment) ]
    |> parsePath

let urlUpdate route state =
    match route with
    | Some page -> state, Cmd.ofMsg (NavigateTo page)
    | None -> state, Cmd.none
