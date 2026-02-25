using System.Collections;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private int remainTime = 90;
    private TMP_Text text;

    void Start()
    {
        if (gameObject.TryGetComponent(out TMP_Text text)) this.text = text;
    }

    public void StartTimer()
    {
        StartCoroutine(Discount());
    }

    IEnumerator Discount()
    {
        while (remainTime > 0)
        {
            yield return new WaitForSeconds(1.0f);
            remainTime--;
            text.text = string.Format("{0:00} : {1:00}", remainTime / 60, remainTime % 60);
        }
    }
}
