using UnityEngine;

public class GameManager : MonoBehaviour
{    
    public static bool gameStarted = false;

    public void StartGame()
    {
        gameStarted = true;
    }
}
