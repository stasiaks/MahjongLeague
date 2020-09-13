module User.Router

open Giraffe
open Fable.Remoting.Server
open Fable.Remoting.Giraffe

open Shared
open User.Api
open User.Documentation

let router: HttpHandler =
    Remoting.createApi()
    |> Remoting.withRouteBuilder Route.builder
    |> Remoting.fromValue api
    |> Remoting.withDocs "/docs/users" documentation
    |> Remoting.buildHttpHandler
