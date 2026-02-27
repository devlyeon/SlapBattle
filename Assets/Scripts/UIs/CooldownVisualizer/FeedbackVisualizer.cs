using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class FeedbackVisualizer : MonoBehaviour
{
    [Header("필수 사전 할당")]
    [SerializeField] private UnityEvent ON_PLAY;
    [SerializeField] private UnityEvent ON_COMPLETE;
    [SerializeField] private CanvasGroup FEEDBACK_COVER_CANVASGROUP;

    [Header("수치 사전 설정")]
    [SerializeField] private float _feedbackInitialAlpha = 0.75f;
    [SerializeField] private float _feedbackTime = 0.25f;
    public float FeedbackTime => _feedbackTime;

    [Header("디버깅")]
    private bool _isFeedback = false;
    public bool IsFeedback => _isFeedback;
    
    [SerializeField] private bool _isExclusive = false;
    protected string _className;

    private Coroutine _feedbackCoroutine;

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
        
        _isFeedback = false;
        FEEDBACK_COVER_CANVASGROUP.alpha = 0f;
    }

    /// <summary>
    /// 피드백 연출을 즉시 정지하는 함수
    /// </summary>
    public void Stop()
    {
        if (!_isFeedback)
            return;
        
        if (_feedbackCoroutine != null)
            StopCoroutine(_feedbackCoroutine);
    }

    /// <summary>
    /// 피드백 연출을 초기화하는 함수
    /// </summary>
    public void Reset()
    {
        this.Stop();
        this.Initialize();
    }

    /// <summary>
    /// 피드백 연출을 재생하는 함수
    /// 기존 재생 중이던 연출을 초기화 한다
    /// </summary>
    public void Play()
    {
        if (!CanExecute())
            return;
        
        ON_PLAY.Invoke();

        if (_isFeedback)
            this.Reset();
        
        _feedbackCoroutine = StartCoroutine(FeedbackAnime());
    }

    private IEnumerator FeedbackAnime()
    {
        _isFeedback = true;
        FEEDBACK_COVER_CANVASGROUP.alpha = _feedbackInitialAlpha;

        // 피드백 연출
        float timer = 0;
        while (timer < _feedbackTime)
        {
            timer += Time.deltaTime;
            
            float normalizedAlpha = (1 - (timer / _feedbackTime));
            FEEDBACK_COVER_CANVASGROUP.alpha = normalizedAlpha * _feedbackInitialAlpha;
            yield return null;
        }

        // 마지막 값 보정
        this.Initialize();

        ON_COMPLETE.Invoke();
    }

    private bool CanExecute()
    {
        bool isValid = true;

        if (FEEDBACK_COVER_CANVASGROUP == null)
        {
            Debug.LogError($"{_className}: FEEDBACK_COVER_CANVASGROUP이 할당되지 않았습니다!");
            isValid = false;
        }

        if (_feedbackTime < 0f)
        {
            Debug.LogError($"{_className}: 피드백 시간은 음수가 될 수 없습니다!");
            isValid = false;
        }

        return isValid;
    }
}
