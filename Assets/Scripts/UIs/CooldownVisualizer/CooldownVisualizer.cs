using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CooldownVisualizer : MonoBehaviour
{
    [Header("필수 사전 할당")]
    [SerializeField] private UnityEvent ON_PLAY;
    [SerializeField] private UnityEvent ON_COMPLETE;
    [SerializeField] private Image COOLDOWN_COVER_IMAGE;
    [SerializeField] private CooldownTimerVisualizer COOLDOWN_TIMER_VISUALIZER;
    [SerializeField] private FeedbackVisualizer FEEDBACK_VISUALIZER;

    [Header("쿨다운 수치 사전 설정")]
    [SerializeField] private float _cooldownTime = 1.0f;
    public float CooldownTime => _cooldownTime;

    [Header("디버깅")]
    [SerializeField] private bool _isCooldown = false;
    public bool IsCooldown => _isCooldown;

    [SerializeField] private bool _isExclusive = false;
    protected string _className;

    private Coroutine _cooldownCoroutine;

    private void Awake()
    {
        if (_isExclusive)
        {
            this.Initialize();
            COOLDOWN_TIMER_VISUALIZER.Initialize();
            FEEDBACK_VISUALIZER.Initialize();
        }
    }

    /// <summary>
    /// 이 클래스 내부의 변수들을 초기화
    /// </summary>
    public void Initialize()
    {
        if (!CanExecute())
            return;

        _isCooldown = false;

        COOLDOWN_COVER_IMAGE.fillAmount = 0f;
    }

    /// <summary>
    /// 쿨다운 연출을 즉시 정지하는 함수
    /// </summary>
    public void Stop()
    {
        if (!_isCooldown)
            return;

        if (_cooldownCoroutine != null)
            StopCoroutine(_cooldownCoroutine);
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
    public void Play()
    {
        if (!CanExecute())
            return;

        ON_PLAY.Invoke();

        if (_isCooldown)
            this.Reset();

        _cooldownCoroutine = StartCoroutine(CooldownAnime());
    }

    private IEnumerator CooldownAnime()
    {
        _isCooldown = true;
        COOLDOWN_COVER_IMAGE.fillAmount = 1f;
        COOLDOWN_TIMER_VISUALIZER.Play(_cooldownTime);

        // 쿨다운 연출
        float timer = 0;
        while (timer < _cooldownTime)
        {
            timer += Time.deltaTime;

            COOLDOWN_COVER_IMAGE.fillAmount = 1 - (timer / _cooldownTime);
            yield return null;
        }

        // 마지막 값 보정
        this.Initialize();
        FEEDBACK_VISUALIZER.Play();

        ON_COMPLETE.Invoke();
        yield return new WaitForSeconds(FEEDBACK_VISUALIZER.FeedbackTime);
    }

    private bool CanExecute()
    {
        bool isValid = true;

        if (COOLDOWN_COVER_IMAGE == null)
        {
            Debug.LogError($"{_className}: COOLDOWN_COVER_IMAGE이 할당되지 않았습니다!");
            isValid = false;
        }

        if (COOLDOWN_TIMER_VISUALIZER == null)
        {
            Debug.LogError($"{_className}: COOLDOWN_TIMER_VISUALIZER이 할당되지 않았습니다!");
            isValid = false;
        }

        if (FEEDBACK_VISUALIZER == null)
        {
            Debug.LogError($"{_className}: FEEDBACK_VISUALIZER이 할당되지 않았습니다!");
            isValid = false;
        }

        if (_cooldownTime < 0f)
        {
            Debug.LogError($"{_className}: 타이머는 음수가 될 수 없습니다!");
            isValid = false;
        }

        return isValid;
    }
}
