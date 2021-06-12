module App.Urls

open Elmish
open Elmish.UrlParser
open Elmish.Navigation

open App.Types

let homeSegment = "home"
let adminSegment = "admin"
let leaguesSegment = "leagues"
let notFoundSegment = "404"

let toUrl page =
    match page with
    | Page.Home -> homeSegment
    | Page.Admin t ->
        sprintf "%s/%s"
        <| adminSegment
        <| Admin.Urls.toUrl t
    | Page.Leagues t ->
        sprintf "%s/%s"
        <| leaguesSegment
        <| Leagues.Urls.toUrl t
    | Page.NotFound -> notFoundSegment
    |> (+) "/"

let parser: Parser<Page option> =
    seq {
        map Page.Home (s "") // Default page
        map Page.Home (s homeSegment)
        map Page.NotFound (s notFoundSegment)
        yield! Admin.Urls.parsers Page.Admin (s adminSegment)
        yield! Leagues.Urls.parsers Page.Leagues (s leaguesSegment)
    }
    |> (List.ofSeq >> oneOf)
    |> parsePath

let urlUpdate route state =
    match route with
    | Some page -> state, Cmd.ofMsg (NavigateTo page)
    | None -> state, Cmd.none
