module App.Urls

open App.Types

let toUrl page =
    match page with
    | Home -> "home"
    | Admin -> "admin"