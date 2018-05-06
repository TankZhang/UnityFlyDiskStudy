using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{

    private GameManagement gameManagement;
    // Use this for initialization
    void Start()
    {
        gameManagement = GameObject.Find("UI").GetComponent<GameManagement>();
    }

    void OnMouseDown()
    {
        switch (gameObject.name)
        {
            case "GameStart":
                gameManagement.ChangeGameState(GameManagement.GameState.GAME);
                break;
            case "GameReset":
                gameManagement.ChangeGameState(GameManagement.GameState.START);
                break;

        }
    }
}
