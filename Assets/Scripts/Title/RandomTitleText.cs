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

    private Coroutine coroutine;
    private TMP_Text text;

    void Start()
    {
        if (gameObject.TryGetComponent(out TMP_Text text)) this.text = text;
    }

    void Update()
    {
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            story.gameObject.SetActive(true);
            story.ResetStory();
        }
        else if (Keyboard.current.anyKey.wasPressedThisFrame && !story.gameObject.activeSelf)
        {
            animator.SetTrigger("Start");
            StartCoroutine(StartGame());
        }
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(1.0f);
        intro.gameObject.SetActive(true);
    }
}
