module App.Leagues.Urls

open Elmish.UrlParser

open App.Leagues.Types

let listSegment = ""

let toUrl page =
    match page with
    | List -> listSegment


let parsers binding parseBefore =
    seq {
        map (binding Page.List) (parseBefore)
        map (binding Page.List) (parseBefore </> s listSegment)
    }
