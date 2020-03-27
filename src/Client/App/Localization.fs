module App.Localization

open Fable.React
open Locale

type LocalizationToken =
    | Home
    | Admin

let localize locale token =
    match locale with
    | English ->
        match token with
        | Home -> "Home"
        | Admin -> "Admin"
    | Polish ->
        match token with
        | Home -> "Strona główna"
        | Admin -> "Administracja"

let lstr locale token = localize locale token |> str
