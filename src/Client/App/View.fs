module App.View

open Fable.FontAwesome
open Fable.React
open Fable.React.Props
open Fulma

open Shared
open App.Localization
open App.Types

let safeComponents =
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
        [ str "Version "
          strong [] [ str Version.app ]
          str " powered by: "
          components ]

let navBrand locale dispatch =
    let navigateProps destination = Navbar.Item.Props [ OnClick(fun _ -> NavigateTo destination |> dispatch) ]
    Navbar.navbar [ Navbar.Color IsWhite ]
        [ Container.container []
              [ Navbar.Brand.div [] [ Navbar.Item.a [ Navbar.Item.CustomClass "brand-text" ] [ str "SAFE Admin" ] ]
                Navbar.menu []
                    [ Navbar.Start.div []
                          [ Navbar.Item.a [ navigateProps Home ] [ lstr locale LocalizationToken.Home ]
                            Navbar.Item.a [ navigateProps Admin ] [ lstr locale LocalizationToken.Admin ] ] ] ] ]

let main (state: State) dispatch =
    match state.CurrentPage with
    | Home -> Home.View.render state.Locale
    | Admin -> Admin.View.render state.Admin (AdminMsg >> dispatch)

let render (state: State) (dispatch: Msg -> unit) =
    div []
        [ navBrand state.Locale dispatch
          main state dispatch
          footer [] [ safeComponents ] ]
