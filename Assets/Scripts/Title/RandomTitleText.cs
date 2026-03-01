using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class RandomTitleText : MonoBehaviour
{
    [SerializeField] IntroDirection intro;
    [SerializeField] StoryPrinter story;
    [SerializeField] Animator animator;
    [SerializeField] ResultDirection resultDirection;

    private Coroutine coroutine;
    private TMP_Text text;
    private bool IsPush = false;

    void Start()
    {
        if (gameObject.TryGetComponent(out TMP_Text text)) this.text = text;
    }

    void Update()
    {
        if (!IsPush)

        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            story.gameObject.SetActive(true);
            story.ResetStory();
        }
        else if (Keyboard.current.anyKey.wasPressedThisFrame && !story.gameObject.activeSelf)
        {
            IsPush = true;
            animator.ResetTrigger("End");
            animator.SetTrigger("Start");
            StartCoroutine(StartGame());
        }
    }

    public void Initialize()
    {
        animator.ResetTrigger("Start");
        animator.SetTrigger("End");
        IsPush = false;
    }

    IEnumerator RandomKey()
    {
        while (true)
        {
            yield return new WaitForSeconds(2.0f);
            text.text = $"[{Keyboard.current.allKeys[Random.Range(0, Keyboard.current.allKeys.Count - 1)].name.FirstCharacterToUpper()} 키]를 눌러 시작하다";
        }
    }

    IEnumerator StartGame()
    {
        resultDirection.Initialize();
        yield return new WaitForSeconds(1.0f);
        intro.Play();
    }
}
