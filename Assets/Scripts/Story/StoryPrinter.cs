using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

// 코드 복붙해온거라 뭔가 엉망진창인데 신경 안 쓰고 나중에 다 정리할 예정
// 근데 그게 오늘은 아닐 수도
public class StoryPrinter : MonoBehaviour
{

    [Header("Text Objects")]
    [SerializeField] private TMP_Text scriptText;
    [SerializeField] private TMP_Text dpText;

    private Coroutine coroutine;

    [SerializeField] private List<string> data = new List<string>();
    private int currentPosition = 0;
    private bool isPrinting = false, isFinish = false;
    private Animator animator;

    void Start()
    {
        if (gameObject.TryGetComponent(out Animator animator)) this.animator = animator;
        ResetStory();
    }

    public void ResetStory()
    {
        scriptText.text = "";
        isPrinting = false;
        isFinish = false;
        currentPosition = 0;
        StartCoroutine(Delay());
        animator.SetBool("Show", true);
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1.0f);
        PrintStory();
    }

    IEnumerator DisableObject()
    {
        animator.SetBool("Show", false);
        yield return new WaitForSeconds(1.0f);
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            FinishStory();
        }
        else if (Keyboard.current.anyKey.wasPressedThisFrame)
        {
            PrintStory();
        }
    }

    public int GetStoryCount() { return data.Count; }
    public void SetStory(List<string> data) { this.data = data; }
    public bool GetIsFinish() { return isFinish; }
    public bool GetActive() { return gameObject.activeSelf; }
    public void SetActive(bool active) { gameObject.SetActive(active); }

    public void PrintStory()
    {
        if (!isPrinting)
        { // 만약 출력 중이 아니라면
            PrintText(); // 다음 대사가 있다면 출력합니다.
        }
        else
        { // 출력 중이라면
            StopCoroutine(coroutine); // 출력 중인 코루틴을 멈추고
            scriptText.text = data[currentPosition - 1]; // 대사는 한 번에 전부 보여줍니다.
            isPrinting = false; // 텍스트가 출력 중이 아님을 표시합니다.
        }
    }

    void PrintText()
    {
        // 만약 다음 대사가 없다면 출력하지 않습니다.
        if (currentPosition >= data.Count || isFinish)
        {
            isFinish = true;
            FinishStory();
            return;
        }

        coroutine = StartCoroutine(PrintText(data[currentPosition]));
        if (currentPosition == 6)
        {
            dpText.gameObject.SetActive(true);
            animator.SetTrigger("DP");
        }
        if (++currentPosition == data.Count) isFinish = true;
    }

    IEnumerator PrintText(string script)
    {
        isPrinting = true; // 출력 중임을 알립니다.
        for (int i = 1; i <= script.Length; i++)
        { // 대사의 글자 수만큼 반복합니다.
            scriptText.text = script.Substring(0, i); // 대사를 한 글자씩 나타나게끔 출력합니다.
            yield return new WaitForSeconds(0.02f); // 0.02초만큼 기다렸다가 반복문을 실행합니다. 코루틴문에서 필수적으로 들어가야하는 구문입니다.
        }
        isPrinting = false; // 출력이 끝났음을 알립니다.
    }

    public void FinishStory()
    {
        StartCoroutine(DisableObject());
    }
}