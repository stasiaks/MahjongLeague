module App.Admin.Urls

open Elmish.UrlParser

open App.Admin.Types

let dashboardSegment = "dashboard"
let usersSegment = "users"

let toUrl page =
    match page with
    | Dashboard -> dashboardSegment
    | Users t ->
        sprintf "%s/%s"
        <| usersSegment
        <| Users.Urls.toUrl t

let usersListPage = Users Users.Types.Page.List

let parsers binding parseBefore =
    seq {
        map (binding Page.Dashboard) (parseBefore)
        map (binding Page.Dashboard) (parseBefore </> s dashboardSegment)
        yield! Users.Urls.parsers (Page.Users >> binding) (parseBefore </> s usersSegment)
    }
