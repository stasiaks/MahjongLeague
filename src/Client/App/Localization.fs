module App.Localization

open Locale

type LocalizationToken =
    | HomeToken of Home.Localization.LocalizationToken
    | AdminToken of Admin.Localization.LocalizationToken
    | LeaguesToken of Leagues.Localization.LocalizationToken
    | NotFoundToken of NotFound.Localization.LocalizationToken
    | Home
    | Leagues
    | Admin
    | SignIn
    | SignOut
    | Language
    | Version
    | PoweredBy

let localize locale token =
    match locale, token with
    | _, HomeToken t -> Home.Localization.localize locale t
    | _, AdminToken t -> Admin.Localization.localize locale t
    | _, LeaguesToken t -> Leagues.Localization.localize locale t
    | _, NotFoundToken t -> NotFound.Localization.localize locale t

    // English
    | English, Home -> "Home"
    | English, Leagues -> "Leagues"
    | English, Admin -> "Admin"
    | English, SignIn -> "Sign in"
    | English, SignOut -> "Sign out"
    | English, Language -> "Language"
    | English, Version -> "Version"
    | English, PoweredBy -> "powered by"

    // Polish (Polski)
    | Polish, Home -> "Strona główna"
    | Polish, Leagues -> "Ligi"
    | Polish, Admin -> "Administracja"
    | Polish, SignIn -> "Zaloguj się"
    | Polish, SignOut -> "Wyloguj się"
    | Polish, Language -> "Język"
    | Polish, Version -> "Wersja"
    | Polish, PoweredBy -> "przy pomocy"
