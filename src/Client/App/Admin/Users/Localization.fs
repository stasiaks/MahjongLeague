module App.Admin.Users.Localization

open Locale

type LocalizationToken =
    | General
    | Users

let localize locale token =
    match locale with
    | English ->
        match token with
        | General -> "General"
        | Users -> "Users"
    | Polish ->
        match token with
        | General -> "Ogólne"
        | Users -> "Użytkownicy"
