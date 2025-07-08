# GEMINI.md

This file provides guidance to Gemini CLI when working with code in this repository.

## Project Overview

This .Net C# solution has 2 projects LoteriaMexicanaServer and LoteriaMexicanaTypes.

LoteriaMexicanaServer it's the project that provides the API for a game called Loteria Mexicana information about the game are found at: https://en.wikipedia.org/wiki/Loter%C3%ADa

The basics of the game are: Lotería (Spanish word meaning "lottery") is a traditional Mexican board game of chance, similar to bingo, but played with a deck of cards instead of numbered balls. Each card has an image of an everyday object. Each player has at least one tabla, a board with a randomly created 4 × 4 grid selected from the card images. Players choose a tabla ("board") to play with, from a variety of previously created tablas, each with a different selection of images. To start the game, the caller (cantor, "singer") shuffles the deck. One by one, the caller picks a card from the deck and announces it to the players by its name, sometimes using a verse before reading the card name. Each player locates the matching pictogram of the card just announced on their board and marks it off with a chip or other kind of marker. In Mexico, it is traditional to use beans as markers. The winner is the first player that shouts "¡Lotería!" after completing a pattern on their tabla, similar to bingo: row, column, diagonal, four corners, or unique to this game, four in a square (pozo).

The second project LoteriaMexicanaTypes, it's shared with an external proeject to be able to share Types (Data Objects) to communicate between the game (client) and the server.

The client is not fully relevant for this project but it's a Godot 4.4 projct

## Technology Stack

- **Framework**: .Net Core 8
- **Communication Layer**: SignalR
- **Programming Language**: C#


## Project Structure

```
LoteriaMexicanaServer/
├── LoteriaMexicanaServer/    # Main API Server
│   ├── Hubs/
│   │   └── GameHub.cs        # Main API Layer (SignalR Hub)
│   ├── Managers/             # This is intended to manage the Models and encapsulate the main business logic to manage themselves.
│   ├── Models/               # Right now this are only records, but in the future some of this will be saved to a database
│   ├── Services/             # Encapsualte business logic and the interaction between the models and the managers
│   └── Program.cs            # Main file, configures and starts the API
└── LoteriaMexicanaTypes/     # Shared Types (Data Objects) to communicate with Client
```


## Current Features

- ✅ Player connects to the server
- ✅ Player joins a room and it's assigned a card
- ✅ Player leaves the room

## Planned Features

- [ ] Player sends a "loteria" signal
1. Receive signal with the player's sheet and "marked" cards on the sheet
2. Receive a timestam of when the signal was captured on the client
3. Check if the player actually won based on the rules
5. If 2 player sent loteria signal close to each other award based on timestamp
- [ ] Communicate winner to all other players

## Architecture Notes

- Use composition over inheritance
- Use JetBrains C# Style guide for reference (https://www.jetbrains.com/help/rider/Settings_Code_Style_CSHARP.html)
