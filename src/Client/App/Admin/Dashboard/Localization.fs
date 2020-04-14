module App.Admin.Dashboard.Localization

open Locale

type LocalizationToken =
    | General
    | Dashboard

let localize locale token =
    match locale with
    | English ->
        match token with
        | General -> "General"
        | Dashboard -> "Dashboard"
    | Polish ->
        match token with
        | General -> "Ogólne"
        | Dashboard -> "Podsumowanie"