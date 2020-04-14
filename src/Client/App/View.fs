module App.View

open Fable.FontAwesome
open Fable.React
open Fable.React.Props
open Fulma

open Locale
open App.Types
open App.Localization

let safeComponents lstr =
    let components =
        span []
            [ a [ Href "https://github.com/SAFE-Stack/SAFE-template" ]
                  [ str "SAFE  "
                    str Version.template ]
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

let navBrand dispatch lstr =
    let dispatchProps msg = Navbar.Item.Props [ OnClick(fun _ -> dispatch msg) ]
    Navbar.navbar [ Navbar.Color IsWhite ]
        [ Container.container []
              [ Navbar.Brand.div []
                    [ Navbar.Item.a [ Navbar.Item.CustomClass "brand-text" ] [ str "Mahjong League" ] ]
                Navbar.menu []
                    [ Navbar.Start.div []
                          [ Navbar.Item.a [ dispatchProps (NavigateTo Page.Home) ] [ lstr Home ]
                            Navbar.Item.a [ dispatchProps (NavigateTo (Page.Admin Admin.Types.Page.Dashboard)) ] [ lstr Admin ] ]
                      Navbar.End.div []
                          [ Navbar.Item.div [ Navbar.Item.HasDropdown; Navbar.Item.IsHoverable ]
                                [ Navbar.Link.div []
                                      [ Fa.i [ Fa.Solid.Globe; Fa.IconOption.PullLeft ] []
                                        lstr Language ]
                                  Navbar.Dropdown.div []
                                      [ Navbar.Item.a [ dispatchProps (ChangeLocale English) ] [ str "English" ]
                                        Navbar.Item.a [ dispatchProps (ChangeLocale Polish) ] [ str "Polski" ] ] ] ] ] ] ]

let main (state: State) dispatch lstr =
    match state.CurrentPage with
    | Page.Home -> Home.View.render (HomeToken >> lstr)
    | Page.Admin page -> Admin.View.render state.Admin (AdminMsg >> dispatch) (AdminToken >> lstr) page

let render (state: State) (dispatch: Msg -> unit) =
    let lstr token = localize state.Locale token |> str
    div []
        [ navBrand dispatch lstr
          main state dispatch lstr
          Container.container [] [ footer [] [ safeComponents lstr ] ] ]
