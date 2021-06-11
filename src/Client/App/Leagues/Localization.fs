module App.Leagues.Localization

open Locale

type LocalizationToken =
    | Leagues
    | List

let localize locale token =
    match locale with
    | English ->
        match token with
        | Leagues -> "Leagues"
        | List -> "List"
    | Polish ->
        match token with
        | Leagues -> "Ligi"
        | List -> "Lista"
