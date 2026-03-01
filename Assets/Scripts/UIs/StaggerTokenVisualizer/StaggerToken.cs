using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class StaggerToken : MonoBehaviour
{
    [Header("필수 사전 할당")]
    [SerializeField] private UnityEvent ON_PLAY;
    [SerializeField] private UnityEvent ON_COMPLETE;
    [SerializeField] private DirectionUI STRAGGER_TOKEN;
    [SerializeField] private DirectionUI STRAGGER_TOKEN_COVER;
    [SerializeField] private FeedbackVisualizer STRAGGER_TOKEN_COVER_FEEDBACK;

    [Header("디버깅")]
    [SerializeField] private bool _isExclusive = false;
    protected string _className;

    private void Awake()
    {
        if (_isExclusive)
            this.Initialize();
    }

    /// <summary>
    /// 이 클래스 내부의 변수들을 초기화
    /// </summary>
    public void Initialize()
    {
        _className = this.GetType().Name;

        if (!CanExecute())
            return;

        STRAGGER_TOKEN.Alpha(EaseType.Instant, 0f, 0f, 1f);
    }

    /// <summary>
    /// 쿨다운 연출을 재생하는 함수
    /// 기존 재생 중이던 연출을 초기화 한다
    /// </summary>
    public void Play()
    {
        if (!CanExecute())
            return;

        ON_PLAY.Invoke();

        StartCoroutine(FeedbackAnime());
    }

    private IEnumerator FeedbackAnime()
    {
        // 피드백 연출
        STRAGGER_TOKEN.Alpha(EaseType.Linear, 0.15f, 1f, 0f);
        STRAGGER_TOKEN_COVER.Scale(EaseType.Linear, 0.25f, new Vector3(15f, 15f, 1f), new Vector3(0f, 100f, 1f));
        STRAGGER_TOKEN_COVER.Alpha(EaseType.Instant, 0f, 0f, 1f);
        yield return new WaitForSeconds(0.15f);

        STRAGGER_TOKEN_COVER_FEEDBACK.Play();
        yield return new WaitForSeconds(0.1f);

        ON_COMPLETE.Invoke();
    }

    private bool CanExecute()
    {
        bool isValid = true;

        if (STRAGGER_TOKEN == null)
        {
            Debug.LogError($"{_className}: STRAGGER_TOKEN이 할당되지 않았습니다!");
            isValid = false;
        }

        if (STRAGGER_TOKEN_COVER == null)
        {
            Debug.LogError($"{_className}: STRAGGER_TOKEN_COVER이 할당되지 않았습니다!");
            isValid = false;
        }

        if (STRAGGER_TOKEN_COVER_FEEDBACK == null)
        {
            Debug.LogError($"{_className}: STRAGGER_TOKEN_COVER_FEEDBACK이 할당되지 않았습니다!");
            isValid = false;
        }

        return isValid;
    }
}
