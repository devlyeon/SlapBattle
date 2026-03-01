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
        else if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Application.Quit();  
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

    IEnumerator StartGame()
    {
        resultDirection.Initialize();
        yield return new WaitForSeconds(1.0f);
        intro.Play();
    }
}
