using UnityEngine;

public class GameManager : MonoBehaviour
{    
    public static bool gameStarted = false;
    public static int maxRound = 2;
    public static int currentRound = 1;

    [SerializeField] private Animator animatorA, animatorB;

    public void BeforeStart()
    {
        animatorA.SetTrigger("In");
        animatorB.SetTrigger("In");
    }

    public void StartGame()
    {
        gameStarted = true;
    }
}
