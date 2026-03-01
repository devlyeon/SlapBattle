using UnityEngine;

public class GameManager : MonoBehaviour
{    
    public static bool gameStarted = false;

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
