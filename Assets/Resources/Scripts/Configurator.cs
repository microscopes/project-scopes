﻿/*!
 * @file Configurator.cs
 * @brief Contains definition of Configurator class.
 * author MicroScopes
 */

//==================================================
//               D I R E C T I V E S
//==================================================

using System.Collections.Generic;
using UnityEngine;

//==================================================
//                 N A M E S P A C E
//==================================================

/*!
 * @brief A global namespace for project-scopes.
 * 
 * @detail Contains all project-scopes related classes.
 */
namespace ProjectScopes
{

//==================================================
//                    C L A S S
//==================================================

/*!
 * @brief Stores the game configuration.
 * 
 * @details Contains information about minimum and maximum number of players,
 *          current number of players, initial size of the arena and all players
 *          speed and thickness as well as each player's specific data.
 */
public class Configurator
{
    // Minimum and maximum number of players that can participate the game.
    public const int MinNoOfPlayers = 2;
    public const int MaxNoOfPlayers = 6;

    // Current number of players.
    private int currentNoOfPlayers = 0;

    // Initial size of the arena. User is able to specify whether the size
    // should be small (0), normal (1) or large (2). Default value: normal.
    private int initialArenaSize = 1;
    private int[,] arenaSize = new int[,] { { 400, 400 }, { 600, 600 }, { 800, 800 } };

    // Initial speed of all players. User is able to specify whether the speed
    // should be slow (0), normal (1) or fast (2). Default value: normal.
    private int initialPlayersSpeed = 1;
    private float[] playersSpeed = new float[] { 1.0f, 2.0f, 4.0f };

    // Initial size of all players. User is able to specify whether the size
    // should be thin (0), normal (1) or fat (2). Default value: normal.
    private int initialPlayersSize = 1;
    private int[] playersSize = new int[] { 3, 5, 9 };

    // List of all players initial data.
    private Player[] players = new Player[6];

    /*!
     * @brief Constructor. Initializes configurator object with initial data.
     * 
     * @details Sets the players initial nickname, color, speed, size and movement keys.
     */
    public Configurator()
    {
        players[0] = new Player("PLAYER 1", new Color(214f/255f, 0f, 147f/255f),
                                playersSpeed[InitialPlayersSpeed], playersSize[InitialArenaSize],
                                new KeyCode[] {KeyCode.A, KeyCode.S});
        players[1] = new Player("PLAYER 2", new Color(153f/255f, 0f, 1f),
                                playersSpeed[InitialPlayersSpeed], playersSize[InitialArenaSize],
                                new KeyCode[] {KeyCode.D, KeyCode.F});
        players[2] = new Player("PLAYER 3", new Color(51f/255f, 153f/255f, 1f),
                                playersSpeed[InitialPlayersSpeed], playersSize[InitialArenaSize],
                                new KeyCode[] {KeyCode.G, KeyCode.H});
        players[3] = new Player("PLAYER 4", new Color(1f, 153f/255f, 102f/255f),
                                playersSpeed[InitialPlayersSpeed], playersSize[InitialArenaSize],
                                new KeyCode[] {KeyCode.X, KeyCode.C});
        players[4] = new Player("PLAYER 5", new Color(0f, 204f/255f, 153f/255f),
                                playersSpeed[InitialPlayersSpeed], playersSize[InitialArenaSize],
                                new KeyCode[] {KeyCode.J, KeyCode.K});
        players[5] = new Player("PLAYER 6", new Color(102f/255f, 205f/255f, 102f/255f),
                                playersSpeed[InitialPlayersSpeed], playersSize[InitialArenaSize],
                                new KeyCode[] {KeyCode.N, KeyCode.M});
    }

    /*!
     * @brief Allows to set and get current number of players.
     * 
     * @details The minimum and maximum number of players are defined by
     *          MinNoOfPlayers and MaxNoOfPlayers constants.
     * @return Number of players that are currently ready to play.
     */
     public int CurrentNoOfPlayers
     {
        set
        {
            currentNoOfPlayers = value;
        }
        get
        {
            return currentNoOfPlayers;
        }
     }

     /*!
      * @brief Allows to set and get the initial size of game arena.
      * 
      * @details The user has possibility to specify whether the size of the
      *          arena should be samll, normal or large.
      * @return Specificator of arena size (0: small, 1: normal, 2: large).
      */
     public int InitialArenaSize
     {
        set
        {
            initialArenaSize = value;
        }
        get
        {
            return initialArenaSize;
        }
     }

     /*!
      * @breif Allows to set and get the initial speed of all players.
      * 
      * @details The user has possibility to specify whether the speed of all
      *          players should be initially slow, normal or fast.
      * @return Specificator of initial players speed (0: slow, 1: normal, 2: fast).
      */
     public int InitialPlayersSpeed
     {
        set
        {
            initialPlayersSpeed = value;
        }
        get
        {
            return initialPlayersSpeed;
        }
     }

     /*!
      * @brief Allows to set and get the initial spize of all players.
      * 
      * @details The user has possibility to specify whether the size of all
      *          players should be initially thin, normal or fat.
      * @return Specificator of initial players size (0: thin, 1: normal, 2: fat).
      */
     public int InitialPlayersSize
     {
        set
        {
            initialPlayersSize = value;
        }
        get
        {
            return initialPlayersSize;
        }
     }

     /*!
      * @brief Allows to get information of all players.
      * 
      * @details GUI can update player specific information depending on
      *          user input.
      */
     public Player[] Players
     {
        get
        {
            return players;
        }
     }          
}

}
 
 