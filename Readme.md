<p align="center">
  <img width="120" height="120" src="wwwroot/static/images/icons/quests.logo.svg">
</p>

# RPG Notes
RPG Notes Desktop is a Blazor Server application bundled in an Electron shell. It is desiged for assisting players and game-masters of tabletop roleplaying games in organizing and sharing notes taken during their gameplay sessions. The desktop version of this app is an expanded version of the original web version which can be found [here](https://github.com/qkmaxware/RpgNotes). Given that this app is designed as a desktop app, it has more access to local hardware resources than the original web version and as such can do more with that capability such as allowing notes to reference images on the user's hard-drive rather than only linking images from public web resources. 

# License
The code in this project is licensed under the following [LICENSE](LICENSE).

# Builds
| Platform | Architecture | Status |
|----------|--------------|--------|
| Windows | x64 | [![Build](https://github.com/qkmaxware/RpgNotes.Desktop/actions/workflows/test-win.yml/badge.svg)](https://github.com/qkmaxware/RpgNotes.Desktop/actions/workflows/test-win.yml) |
| Linux | x64 | [![Build](https://github.com/qkmaxware/RpgNotes.Desktop/actions/workflows/test-linux.yml/badge.svg)](https://github.com/qkmaxware/RpgNotes.Desktop/actions/workflows/test-linux.yml) |
| Mac OS | x64 | [![Build](https://github.com/qkmaxware/RpgNotes.Desktop/actions/workflows/test-mac.yml/badge.svg)](https://github.com/qkmaxware/RpgNotes.Desktop/actions/workflows/test-mac.yml) |

# Usage
## Downloads
Check out the [releases](https://github.com/qkmaxware/RpgNotes.Desktop/releases) for downloads to the current and past versions of the app. 

## Compatibility
This application should be functional on operating systems and architectures as defined in the [builds](#builds) section

## Terminology
The following terms are used in the documentation and in the application itself. Their meanings within the context of this project is as follows.

| Term | Meaning |
|------|---------|
| World | The workspace of the application, contains multiple articles about people, places, and things. |
| Article | A single entity within the world. These could be people, places, or things. |
| Campaign | A special kind of article used for indicating groups of player characters and documenting their adventures in the world. A world may have many different ongoing campaigns. |

## Basic Functionality
1. Create a world using the new button in the center of the main page.
   1. Define a name and what RPG system you are using. Use **Other** if your system is not listed.
2. Create an article by clicking on any of the available templates
   1. Name the entry and provide a brief description (optional). 
3. Create a cmapaign by clicking the '+' button on the home page under **Active Campaigs**
    1. Name the entry and provide a brief description (optional). 
4. Use the article browser to find articles 
5. For any article
   1. Change description
   2. Add notes
   3. Add organizational tags
   4. Add images
      1. From public online sources
      2. Uploaded from a local file
   5. Create connections to other entries using the **Connections** tab
      1. Relationships between characters
      2. Possession of items
      3. Relative position of locations