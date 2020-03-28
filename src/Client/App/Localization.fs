module App.Localization

open Fable.React
open Locale

type LocalizationToken =
    | HomeToken of Home.Localization.LocalizationToken
    | Home
    | Admin
    | Language
    | Version
    | PoweredBy

let localize locale token =
    match locale, token with
    | _, HomeToken t -> Home.Localization.localize locale t

    // English
    | English, Home -> "Home"
    | English, Admin -> "Admin"
    | English, Language -> "Language"
    | English, Version -> "Version"
    | English, PoweredBy -> "powered by"

    // Polish (Polski)
    | Polish, Home -> "Strona główna"
    | Polish, Admin -> "Administracja"
    | Polish, Language -> "Język"
    | Polish, Version -> "Wersja"
    | Polish, PoweredBy -> " przy pomocy"

let localizedStr locale token = localize locale token |> str
