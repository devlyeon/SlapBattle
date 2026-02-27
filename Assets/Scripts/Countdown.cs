using UnityEngine;

/// <summary>
/// 코드 레거시 때문에 남겨놓음. 게임 시작 확인용.
/// </summary>
public class Countdown : MonoBehaviour
{    
    public static bool gameStarted = false;

    public void StartGame()
    {
        gameStarted = true;
        gameObject.SetActive(false);
    }
}
