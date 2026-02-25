using System.Collections;
using TMPro;
using UnityEngine;

public class Message : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private TMP_Text text;

    void Start()
    {
        if (gameObject.TryGetComponent(out TMP_Text text)) this.text = text;
        if (gameObject.TryGetComponent(out Animator animator)) this.animator = animator;
    }

    void ShowMessage(string message)
    {
        text.text = message;
        animator.SetTrigger("show");
        StartCoroutine(DestroyObj());
    }

    IEnumerator DestroyObj()
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);
    }
}
