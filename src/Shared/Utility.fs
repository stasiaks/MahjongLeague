module Shared.Utility
open System

let parseGuid (s: string) =
    try
        Guid.Parse(s) |> Some
    with
    | _ -> None