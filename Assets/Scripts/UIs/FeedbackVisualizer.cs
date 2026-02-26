using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class FeedbackVisualizer : MonoBehaviour
{
    [Header("필수 사전 할당")]
    [SerializeField] private CanvasGroup FEEDBACK_COVER_CANVASGROUP;

    [Header("유니티 이벤트 시스템")]
    [SerializeField] private UnityEvent ON_PLAY_UNITYEVENT;
    [SerializeField] private UnityEvent ON_COMPLETE_UNITYEVENT;

    [Header("피드백 수치 사전 설정")]
    [SerializeField] private float _totalFeedbackTime = 1.0f;
    [SerializeField] private bool _isFeedback = false;

    public float TotalFeedbackTime => _totalFeedbackTime;
    public bool IsFeedback => _isFeedback;

    private Coroutine _feedbackAnimeCoroutine;

    private void Start()
    {
        this.Initialize();
    }

    // 사전 초기화가 필요할 때 사용
    public void Initialize()
    {
        FEEDBACK_COVER_CANVASGROUP.alpha = 0f;
        _isFeedback = false;
    }

    // 피드백 연출을 정지
    public void Stop()
    {
        if (_feedbackAnimeCoroutine != null)
            StopCoroutine(_feedbackAnimeCoroutine);
    }

    // 피드백 연출을 초기화
    public void Reset()
    {
        this.Stop();
        this.Initialize();
    }

    // 피드백 연출을 재생
    public void Play()
    {
        if (_isFeedback)
            this.Reset();
        _feedbackAnimeCoroutine = StartCoroutine(FeedbackAnime());
    }

    // 피드백 연출 코루틴
    private IEnumerator FeedbackAnime()
    {
        // 연출 시작
        _isFeedback = true;
        ON_PLAY_UNITYEVENT.Invoke();
        FEEDBACK_COVER_CANVASGROUP.alpha = 0.75f;

        // 연출 시간 돌리기 1
        float feedbackTimer = 0;
        while (feedbackTimer < _totalFeedbackTime)
        {
            feedbackTimer += Time.deltaTime;
            FEEDBACK_COVER_CANVASGROUP.alpha = (1 - (feedbackTimer / _totalFeedbackTime)) * 0.75f;
            yield return null;
        }

        // 연출 초기화
        ON_COMPLETE_UNITYEVENT.Invoke();
        FEEDBACK_COVER_CANVASGROUP.alpha = 0f;
        this.Reset();
    }
}
