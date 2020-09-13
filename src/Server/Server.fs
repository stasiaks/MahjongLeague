open System.IO

open Giraffe
open Saturn

let tryGetEnv = System.Environment.GetEnvironmentVariable >> function null | "" -> None | x -> Some x

let publicPath = Path.GetFullPath "../Client/public"

let port =
    "SERVER_PORT"
    |> tryGetEnv |> Option.map uint16 |> Option.defaultValue 8085us

let combinedRouter =
    choose
        [ User.Router.router ]

let app = application {
    url ("http://0.0.0.0:" + port.ToString() + "/")
    use_router combinedRouter
    memory_cache
    use_static publicPath
    use_gzip
}

run app
