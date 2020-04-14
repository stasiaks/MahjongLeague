module App.Urls

open Elmish
open Elmish.UrlParser
open Elmish.Navigation

open App.Types

let toUrl page =
    match page with
    | Home -> "home"
    | Admin _ -> "admin"

let parser: Parser<Page option> =
    oneOf [
        map Home (s <| toUrl Home)
        map (Admin Admin.Types.Page.Dashboard) (s <| toUrl (Admin <| Admin.Types.Page.Dashboard) )
    ] |> parsePath

let urlUpdate route state =
    match route with
    | Some page -> state, Cmd.ofMsg (NavigateTo page)
    | None -> state, Cmd.none
