<table width="100%">
  <tr>
    <!-- Top large gif -->
    <td colspan="2" align="center">
      <img src="https://github.com/rchtr-chn/CozyKettle/raw/main/readme-bank/cozykettle-gif-2.gif" width="100%"/>
    </td>
  </tr>
  <tr>
    <!-- Bottom two gifs -->
    <td align="center" width="50%">
      <img src="https://github.com/rchtr-chn/CozyKettle/raw/main/readme-bank/cozykettle-gif-1.gif" width="100%"/>
    </td>
    <td align="center" width="50%">
      <img src="https://github.com/rchtr-chn/CozyKettle/raw/main/readme-bank/cozykettle-gif-3.gif" width="100%"/>
    </td>
  </tr>
</table>

<h2>☕ Cozy Kettle</h2>
  <img width=200px align="left" src=https://github.com/rchtr-chn/CozyKettle/raw/main/readme-bank/cozykettle-cover-image.png>

  Step into the charming, slightly dusty shoes of Anna, the new heir to your parents' run-down tea shop. Forget stressful deadlines, your path to success is paved with precision, warmth, and the perfect cup of tea. Embrace a cozy new life dedicated to the calming art of brewing.

###

<h2>📜 Scripts</h2>

  | Script | Description |
  | ------ | ----------- |
  | `DeckManagerScript.cs` | Manages starting deck and saves any modification done to deck by player |
  | `HandManagerScript.cs` | Receives cards from `DeckManagerScript.cs` to be drawn on hand and returned to when needed|
  | `GameManagerScript.cs` | Organizes and centralized other minor managers and manages the turn-based system |
  | `ShopManagerScript.cs` | Manages the shop's cards to be displayed and sold to the player |
  | `Card.cs` | Blueprint for SOs that will carry a card's value and the potential card effect |
  | etc. |

<h2>📂 Folder Descriptions</h2>

  ```
  ├── Rat-Gambler                      # Root folder of this project
    ...
    ├── Assets                         # Assets folder of this project
      ...
      ├── Audio                        # Stores all BGM and audio clips used in this project
      ├── Fonts                        # Stores all fonts used in this project
      ├── Resources                    # Parent folder to organize blueprints (Scriptable Objects) and prefabs
        ├── CardData                   # Parent folder of all scriptable object types that are used in this project
          ...
        ├── Prefabs                    # Parent folder that stores prefabs that are instantiated during the project's runtime
          ...
      ├── Scenes                       # Stores all Unity Scenes used in this project
      ├── Scripts                      # Parent folder of all types of scripts that are used in this project
        ├── BackgroundManagers         # Stores scripts related to managers that function the game in the background
        ├── CardBehavior               # Stores scripts related to a card prefab
        ├── CardEffects                # Stores scripts consisting the logic behind every power cards
        ├── Cardshop                   # Stores scripts related to the card shop
        ├── CardSystem                 # Stores scripts related to card deck creation and usability during gameplay
        ├── Cookie                     # Stores scripts related to wagering cookies mechanic and cookie value modification
      ├── Sprites                      # Parent folder of all sprites that are used in this project
      ...
    ...
  ...
  ```
<h2>💡 Contributions</h2>

As the sole main programmer, I dedicated around 60 hours in total to this project, developing all of the mechanics that make the game function as intended, such as brewing minigame mechanics, inventory and shop system, cutscene mechanics, and etc.
  

<h2>⬇️ Game Pages</h2>
  itch.io: https://rchtr-chn.itch.io/cozy-kettle
  
<h2>🎮 Controls</h2>

  | Input | Function |
  | -------------------- | --------------------- |
  | Hold and move cursor | Select and drag item/UI |
  
<h2>📋 Project Information</h2>

  ![Unity Version 6000.2.9f1](https://img.shields.io/badge/Unity_Version-6000.2.9f1-FFFFFF.svg?style=flat-square&logo=unity) <br/>
  Game Build: ![Windows](https://img.shields.io/badge/Windows-004fe1.svg?style=flat-square&logo=windows) <br/>
  All art assets are made by our [game artist](https://kelvinkel.my.canva.site/) <br/>
  BGM is made by our [game designer](https://github.com/wi1wil) <br/>
  All SFX can be found in [![Pixabay](https://img.shields.io/badge/Pixabay-191B26.svg?style=flat-square&logo=Pixabay)](https://pixabay.com) <br/> <br/>
  
  <b>Team:</b>
  - <a href="https://github.com/rchtr-chn">Richter Cheniago</a> (Game programmer)
  - <a href="https://github.com/wi1wil">Wilson Halim</a> (Game designer)
  - <a href="https://kelvinkel.my.canva.site/">Kelvin</a> (Game artist)
