module Utilities.JsonWebToken

open System
open Microsoft.IdentityModel.Tokens
open System.IdentityModel.Tokens.Jwt

open Shared.Authentication
open System.Security.Claims
open System.Net
open Newtonsoft.Json.Linq

type TokenValidationResult =
    | Valid of ClaimsPrincipal
    | InvalidToken
    | Expired

let jwtTokenHandler = JwtSecurityTokenHandler()

let issuerSigningKeyResolver = // TODO: Make it not so naive
    new IssuerSigningKeyResolver(fun token securityToken kid validationParameters ->
    let req =
        sprintf "https://%s/.well-known/jwks.json" domain
        |> Uri
        |> WebRequest.Create

    use response = req.GetResponse()
    use stream = response.GetResponseStream()
    use reader = new IO.StreamReader(stream)
    reader.ReadToEnd()
    |> JObject.Parse
    |> fun x -> x.SelectToken "keys"
    |> fun x -> x.Values()
    |> Seq.map (string >> JsonWebKey.Create)
    |> Seq.cast<SecurityKey>)

let validateToken (SecurityToken token) =
    let validationParameters = TokenValidationParameters()
    validationParameters.ValidAudience <- audience
    validationParameters.ValidIssuer <- sprintf "https://%s/" domain
    validationParameters.IssuerSigningKeyResolver <- issuerSigningKeyResolver
    try // TODO: Consider replacing with ErrorHandler
        let claims, token =
            jwtTokenHandler.ValidateToken(token, validationParameters)
        Valid claims
    with
    | :? SecurityTokenExpiredException -> Expired
    | :? ArgumentNullException
    | :? ArgumentException
    | :? SecurityTokenException -> InvalidToken

let readClaims (SecurityToken token) = (jwtTokenHandler.ReadJwtToken token).Claims |> List.ofSeq
