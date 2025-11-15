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
  <img width=400px align="left" src=https://github.com/rchtr-chn/CozyKettle/raw/main/readme-bank/cozykettle-cover-image.png>

  Welcome to the cozy world of Slow Steeps. Simple Joys, where you play as Anna, the new owner of her parents' run-down tea shop, aiming for success not through stress, but through the calming art of brewing. The core gameplay revolves around mastering the Art of Tea by following precise recipes (temperature, steeping time) for classic and signature mixes, utilizing customer feedback and your own notes to achieve a five-star rating and build reputation. In addition to serving a diverse cast of patrons with speed and accuracy, you must Manage Your Shop With A Tap using a mobile phone interface to handle essential logistics: efficiently tracking inventory (especially crucial add-ons like milks and syrups), procuring new stock while balancing quality and cost, and monitoring the bank account to ensure profitability and investment in better ingredients, all while honoring your parents' legacy by turning the humble spot into a local treasure.

###

<h2>📜 Scripts</h2>

  | Script | Description |
  | ------ | ----------- |
  | `ItemSO.cs` | scriptable object that will be inherited by `herb.cs`, `addon.cs`, and etc. |
  | `BrewingManagerScript.cs` | Manages the entire brewing flow and micromanaging other aiding managers |
  | `TimeManager.cs` | Manages the game's time system |
  | `SummaryManager.cs` | Manages the summary screen's display |
  | `Book.cs` | Manages book data and display |
  | etc. |

<h2>📂 Folder Descriptions</h2>

  ```
  ├── CozyKettle                       # Root folder of this project
    ...
    ├── Assets                         # Assets folder of this project
      ...
      ├── Resources                    # Parent folder to organize sprites, art assets, SOs, fonts, etc.
        ...
        ├── Fonts                      # Stores all fonts used in this project
        ├── SoundEffects               # Stores all BGM and audio clips used in this project
        ├── VisualArtAssets            # Parent folder of all sprites that are used in this project
        ├── Timeline                   # Parent folder of all timeline cutscenes that are used in this project
        ├── OBJs                       # Parent folder of all scriptable objects that are used in this project
        ├── Materials                  # Parent folder of all materials that are used in this project
        ├── Prefabs                    # Parent folder that stores prefabs that are instantiated during the project's runtime
        ├── AudioMixers                # Parent folder of all audio mixers in this project
        ├── Fonts                      # Parent folder of all fonts that are used int his project
        ...
      ├── Scenes                       # Stores all Unity Scenes used in this project
      ├── Scripts                      # Parent folder of all types of scripts that are used in this project
        ...
        ├── BackgroundManagers         # Stores scripts related to all managers working in the background
        ├── Cutscene                   # Stores scripts related to managing cutscene and timelines
        ├── GardenScene                # Stores scripts related to the garden scene
          ├── GardenInventory          # Stores scripts related to player's garden inventory
          ├── PlantFSM                 # Stores scripts related to plant's base states and its manager
        ├── StartMenuUI                # Stores scripts related to the start menu's UI
          ├── CloudUI                  # Stores scripts related to the cloud UI in start menu scene
        ├── StaticData                 # Stores scripts related to player's static data
        ├── TeaShop                    # Stores scripts related to the tea shop scene
          ├── Beverage                 # Stores scripts related to beverages
          ├── Book                     # Stores scripts related to the recipe book
          ├── Customer                 # Stores scripts related to customer and its manager
          ├── Dispenser                # Stores scripts related to the tea shop's dispenser
          ├── Enum                     # Stores scripts related to enum data
          ├── FrenchPress              # Stores scripts related to the tea shop's french press
          ├── Kettle                   # Stores scripts related to the tea shop's kettle
          ├── Minigame                 # Stores scripts related to the tea shop's minigames' managers
          ├── Phone                    # Stores scripts related to the players phone and its respective app/feature managers
          ├── Stove                    # Stores scripts related to the tea shop's stove
        ├── UI                         # Stores scripts related to the teap shop UI
        ...
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
