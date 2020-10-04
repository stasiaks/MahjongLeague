module App.Admin.Users.Urls

open Elmish.UrlParser

open App.Admin.Users.Types

let listSegment = "list"

let toUrl page =
    match page with
    | List -> listSegment

let parsers binding parseBefore =
    seq {
        map (binding Page.List) (parseBefore)
        map (binding Page.List) (parseBefore </> s listSegment)
    }
