module App.Localization

open Fable.React
open Locale

type LocalizationToken =
    | Home
    | Admin
    | Language

let localize locale token =
    match locale with
    | English ->
        match token with
        | Home -> "Home"
        | Admin -> "Admin"
        | Language -> "Language"
    | Polish ->
        match token with
        | Home -> "Strona główna"
        | Admin -> "Administracja"
        | Language -> "Język"

let lstr locale token = localize locale token |> str
