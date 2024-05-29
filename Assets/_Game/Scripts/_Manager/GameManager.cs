using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    MainMenu = 0,
    Gameplay = 1,
    Pause = 2,
    Win = 3,
    Lose = 4
}

public class GameManager : Singleton<GameManager>
{

}
