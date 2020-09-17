module App.View

open Fable.FontAwesome
open Fable.React
open Fable.React.Props
open Fulma

open Locale
open App.Types
open App.Localization
open App.JwtDecode
open Shared
open Shared.Authentication

let safeComponents lstr =
    let components =
        span []
            [ a [ Href "https://github.com/SAFE-Stack/SAFE-template" ] [ str "SAFE  "; str Version.template ]
              str ", "
              a [ Href "https://saturnframework.github.io" ] [ str "Saturn" ]
              str ", "
              a [ Href "http://fable.io" ] [ str "Fable" ]
              str ", "
              a [ Href "https://elmish.github.io" ] [ str "Elmish" ]
              str ", "
              a [ Href "https://fulma.github.io/Fulma" ] [ str "Fulma" ]
              str ", "
              a [ Href "https://bulmatemplates.github.io/bulma-templates/" ] [ str "Bulma\u00A0Templates" ]
              str ", "
              a [ Href "https://zaid-ajaj.github.io/Fable.Remoting/" ] [ str "Fable.Remoting" ] ]

    span []
        [ lstr Version
          str " "
          strong [] [ str Version.app ]
          str " "
          lstr PoweredBy
          str ": "
          components ]

let signIn state dispatch lstr =
    let dispatchProps msg =
        Navbar.Item.Props [ OnClick(fun _ -> dispatch msg) ]

    match state.AccessToken, state.UserInfo with
    | None, _ -> Navbar.Item.a [ dispatchProps Login ] [ lstr SignIn ]
    | Some _, None -> Navbar.Item.a [ dispatchProps Logout ] [ lstr SignOut ]
    | Some _, Some userInfo ->
        Navbar.Item.div
            [ Navbar.Item.HasDropdown
              Navbar.Item.IsHoverable ]
            [ Navbar.Link.div []
                [ img
                    [ Src userInfo.picture
                      Style [ PaddingRight "0.5em" ] ]
                  str userInfo.name ]
              Navbar.Dropdown.div []
                [ Navbar.Item.a [ dispatchProps Logout ] [ lstr SignOut ] ] ]

let navBrand state dispatch lstr permissionContainer =
    let dispatchProps msg =
        Navbar.Item.Props [ OnClick(fun _ -> dispatch msg) ]

    Navbar.navbar [ Navbar.Color IsWhite ]
        [ Container.container []
              [ Navbar.Brand.div [] [ Navbar.Item.a [ Navbar.Item.CustomClass "brand-text" ] [ str "Mahjong League" ] ]
                Navbar.menu []
                    [ Navbar.Start.div []
                          [ Navbar.Item.a [ dispatchProps (NavigateTo Page.Home) ] [ lstr Home ]
                            permissionContainer
                                [ Permissions.Users.Read ]
                                <| Navbar.Item.a [ dispatchProps (NavigateTo <| Page.Admin Admin.Types.Page.Dashboard) ] [ lstr Admin ] ]
                      Navbar.End.div []
                          [ Navbar.Item.div
                              [ Navbar.Item.HasDropdown
                                Navbar.Item.IsHoverable ]
                                [ Navbar.Link.div []
                                      [ Fa.i
                                          [ Fa.Solid.Globe
                                            Fa.IconOption.PullLeft ] []
                                        lstr Language ]
                                  Navbar.Dropdown.div []
                                      [ Navbar.Item.a [ dispatchProps (ChangeLocale English) ] [ str "English" ]
                                        Navbar.Item.a [ dispatchProps (ChangeLocale Polish) ] [ str "Polski" ] ] ]
                            signIn state dispatch lstr ] ] ] ]

let permissionContainer (token: SecurityToken option) (requiredPermissions: Permission seq) (element: ReactElement) =
    match token with
    | None -> div [] []
    | Some (SecurityToken token) ->
        (JwtDecode.DecodePayload token).permissions
        |> Seq.map Permission
        |> Seq.containsAll requiredPermissions
        |> function
        | true -> element
        | false -> div [] []

let main (state: State) dispatch lstr permissionContainer =
    match state.CurrentPage with
    | Page.Home -> Home.View.render (HomeToken >> lstr)
    | Page.Admin page -> Admin.View.render state.Admin (adminTranslator >> dispatch) (AdminToken >> lstr) page
    | Page.NotFound -> NotFound.View.render (NotFoundToken >> lstr)

let render (state: State) (dispatch: Msg -> unit) =
    let lstr token = localize state.Locale token |> str
    let permissionContainer = state.AccessToken |> permissionContainer
    div []
        [ navBrand state dispatch lstr permissionContainer
          main state dispatch lstr permissionContainer
          Container.container [] [ footer [] [ safeComponents lstr ] ] ]
