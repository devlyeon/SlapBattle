using System.Collections;
using TMPro;
using UnityEngine;

public class Countdown : MonoBehaviour
{    
    public static bool gameStarted = false;
    private Animator animator;
    private TMP_Text text;
    private int time = 3;

    void Start()
    {
        if (gameObject.TryGetComponent(out Animator animator)) this.animator = animator;
        else Debug.LogError(gameObject.name + "에 지정된 Animator가 없습니다.");

        if (gameObject.TryGetComponent(out TMP_Text text)) this.text = text;
        else Debug.LogError(gameObject.name + "에 지정된 TMP Text가 없습니다.");

        text.text = time.ToString();
        StartCoroutine(PlayCountdown());
    }

    IEnumerator PlayCountdown()
    {
        animator.SetBool("PlayAnim", true);
        for (; time > 0; time--)
        {
            text.text = time.ToString();
            yield return new WaitForSeconds(1.0f);
        }
        
        text.text = "START!!!";
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(0.2f);
        gameStarted = true;
    }
}
