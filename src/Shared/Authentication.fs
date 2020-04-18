module Shared.Authentication

type SecurityToken = SecurityToken of string

type SecureRequest<'a> =
    { Token: SecurityToken
      Content: 'a }

type AuthenticationError =
    | TokenExpired
    | TokenInvalid
    | NoAccess
