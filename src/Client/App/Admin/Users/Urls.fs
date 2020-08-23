module App.Admin.Users.Urls

open Elmish.UrlParser

open App.Admin.Users.Types

let toUrl page =
    match page with
    | List -> ""

let parseSegment state =
    custom "users" (function
        | s when s = toUrl List -> Ok List
        | _ -> Error "Can't parse users subpage") state
