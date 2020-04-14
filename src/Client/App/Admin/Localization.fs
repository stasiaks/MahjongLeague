module App.Admin.Localization

type LocalizationToken =
    | DashboardToken of Dashboard.Localization.LocalizationToken
    | UsersToken of Users.Localization.LocalizationToken

let localize locale token =
    match token with
    | DashboardToken t -> Dashboard.Localization.localize locale t
    | UsersToken t -> Users.Localization.localize locale t
