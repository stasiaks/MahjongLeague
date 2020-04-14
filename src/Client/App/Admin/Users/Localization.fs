module App.Admin.Users.Localization

open Locale

type LocalizationToken =
    | Users

let localize locale token =
    match locale with
    | English ->
        match token with
        | Users -> "Users"
    | Polish ->
        match token with
        | Users -> "Użytkownicy"