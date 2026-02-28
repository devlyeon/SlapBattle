using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class RandomTitleText : MonoBehaviour
{
    [SerializeField] IntroDirection intro;
    [SerializeField] Animator animator;

    private Coroutine coroutine;
    private TMP_Text text;

    void Start()
    {
        if (gameObject.TryGetComponent(out TMP_Text text)) this.text = text;
        coroutine = StartCoroutine(RandomKey());
    }

    void Update()
    {
        if (Keyboard.current.anyKey.isPressed)
        {
            StopCoroutine(coroutine);
            animator.SetTrigger("Start");
            StartCoroutine(StartGame());
        }
    }

    IEnumerator RandomKey()
    {
        while (true)
        {
            yield return new WaitForSeconds(2.0f);
            text.text = $"시작 [{Keyboard.current.allKeys[Random.Range(0, Keyboard.current.allKeys.Count - 1)].name.FirstCharacterToUpper()} 키]를 누르다";
        }
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(1.0f);
        intro.gameObject.SetActive(true);
    }
}
