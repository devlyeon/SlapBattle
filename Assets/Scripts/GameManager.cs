using UnityEngine;

public class GameManager : MonoBehaviour
{    
    public static bool gameStarted = false;
    public static int maxRound = 2;
    public static int currentRound = 1;

    public void StartGame()
    {
        gameStarted = true;
    }
}
