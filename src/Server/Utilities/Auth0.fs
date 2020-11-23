module Utilities.Auth0

open FSharp.Data
open Shared.Authentication
open Newtonsoft.Json

type UserInfo =
    { Sub: string
      Nickname: string
      Email: string
      Name: string }

let getUserInfo (SecurityToken token) =
    async {
        let! response =
            Http.AsyncRequestString
                (url = sprintf "https://%s/userinfo" domain,
                 headers = [ "Authorization", sprintf "Bearer %s" token ],
                 httpMethod = "POST")

        return JsonConvert.DeserializeObject<UserInfo>(response)
    }
