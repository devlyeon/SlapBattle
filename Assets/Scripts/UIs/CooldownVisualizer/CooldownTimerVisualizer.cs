using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class CooldownTimerVisualizer : MonoBehaviour
{
    [Header("필수 사전 할당")]
    [SerializeField] private UnityEvent ON_PLAY;
    [SerializeField] private UnityEvent ON_COMPLETE;
    [SerializeField] private TMP_Text COOLDOWN_TIMER_TEXT;
    [SerializeField] private CanvasGroup COOLDOWN_TIMER_TEXT_CANVASGROUP;

    [Header("디버깅")]
    private bool _isCooldown = false;
    public bool IsCooldown => _isCooldown;

    [SerializeField] private bool _isExclusive = false;
    protected string _className;

    private Coroutine _cooldownTimerCoroutine;

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
        if (!CanExecute())
            return;

        _isCooldown = false;
        COOLDOWN_TIMER_TEXT_CANVASGROUP.alpha = 0f;
    }

    /// <summary>
    /// 쿨다운 연출을 즉시 정지하는 함수
    /// </summary>
    public void Stop()
    {
        if (!_isCooldown)
            return;

        if (_cooldownTimerCoroutine != null)
            StopCoroutine(_cooldownTimerCoroutine);
    }

    /// <summary>
    /// 쿨다운 연출을 초기화하는 함수
    /// </summary>
    public void Reset()
    {
        this.Stop();
        this.Initialize();
    }

    /// <summary>
    /// 쿨다운 연출을 재생하는 함수
    /// 기존 재생 중이던 연출을 초기화 한다
    /// </summary>
    public void Play(float cooldownTime)
    {
        if (!CanExecute())
            return;

        if (cooldownTime < 0f)
        {
            Debug.LogError($"{_className}: 타이머는 음수가 될 수 없습니다!");
            return;
        }

        ON_PLAY.Invoke();

        if (_isCooldown)
            this.Reset();

        _cooldownTimerCoroutine = StartCoroutine(FeedbackAnime(cooldownTime));
    }

    private IEnumerator FeedbackAnime(float cooldownTime)
    {
        _isCooldown = true;
        COOLDOWN_TIMER_TEXT_CANVASGROUP.alpha = 1f;

        // 피드백 연출
        float timer = 0;
        while (timer < cooldownTime)
        {
            timer += Time.deltaTime;

            float remainingTime = cooldownTime - timer;

            if (remainingTime >= 1f)
                COOLDOWN_TIMER_TEXT.text = Mathf.FloorToInt(remainingTime).ToString();
            else
                COOLDOWN_TIMER_TEXT.text = remainingTime.ToString("F1");

            yield return null;
        }

        // 마지막 값 보정
        this.Initialize();

        ON_COMPLETE.Invoke();
    }

    private bool CanExecute()
    {
        bool isValid = true;

        if (COOLDOWN_TIMER_TEXT == null)
        {
            Debug.LogError($"{_className}: COOLDOWN_TIMER_TEXT이 할당되지 않았습니다!");
            isValid = false;
        }

        if (COOLDOWN_TIMER_TEXT_CANVASGROUP == null)
        {
            Debug.LogError($"{_className}: COOLDOWN_TIMER_TEXT_CANVASGROUP이 할당되지 않았습니다!");
            isValid = false;
        }

        return isValid;
    }
}