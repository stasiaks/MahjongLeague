module App.Home.Localization

open Fable.React
open Locale

type LocalizationToken =
    | HelloWorld

let localize locale token =
    match locale with
    | English ->
        match token with
        | HelloWorld -> "Hello world!"
    | Polish ->
        match token with
        | HelloWorld -> "Witaj świecie!"