module App.Admin.Urls

open Elmish.UrlParser

open App.Admin.Types

let toUrl page =
    match page with
    | Dashboard -> "dashboard"
    | Users -> "users"

let parseSegment state =
    custom "admin" (function
        | s when s = toUrl Dashboard -> Ok Dashboard
        | s when s = toUrl Users -> Ok Users
        | _ -> Error "Can't parse admin subpage") state
