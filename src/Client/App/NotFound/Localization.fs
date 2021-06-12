module App.NotFound.Localization

open Locale

type LocalizationToken =
    | Oops

let localize locale token =
    match locale with
    | English ->
        match token with
        | Oops -> "Oops! That page can't be found."
    | Polish ->
        match token with
        | Oops -> "Ups, podana strona nie została znaleziona."
