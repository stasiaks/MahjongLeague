module League.Router

open System
open Giraffe
open Fable.Remoting.Server
open Fable.Remoting.Giraffe
open Microsoft.AspNetCore.Http

open Shared
open League.Api
open League.Documentation

let errorHandler (ex: Exception) (routeInfo: RouteInfo<HttpContext>) =
    printfn "Error at %s on method %s" routeInfo.path routeInfo.methodName
    printfn "Exception: %O" ex
    Propagate ex

let router: HttpHandler =
    Remoting.createApi()
    |> Remoting.withRouteBuilder Route.builder
    |> Remoting.fromValue api
    |> Remoting.withErrorHandler errorHandler
    |> Remoting.withDocs "/docs/leagues" documentation
    |> Remoting.buildHttpHandler
