module App.Admin.Urls

open Elmish.UrlParser

open App.Admin.Types

let dashboardSegment = "dashboard"
let usersSegment = "users"

let toUrl page =
    match page with
    | Dashboard -> dashboardSegment
    | Users t -> sprintf "%s/%s" <| usersSegment <| Users.Urls.toUrl t

let usersListPage = Users Users.Types.Page.List

let parseSegment state =
    custom "admin" (function
        | s when s = toUrl Dashboard -> Ok Dashboard
        | s when s = toUrl usersListPage -> Ok usersListPage
        | _ -> Error "Can't parse admin subpage") state
