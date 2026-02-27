using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class IntroDirection : MonoBehaviour
{
    [Header("필수 사전 할당")]
    [SerializeField] private UnityEvent ON_PLAY;
    [SerializeField] private UnityEvent ON_COMPLETE;
    [SerializeField] private UnityEvent ON_ARE;
    [SerializeField] private UnityEvent ON_YOU;
    [SerializeField] private UnityEvent ON_READY;
    [SerializeField] private UnityEvent ON_LETSROCK;

    [SerializeField] private DirectionUI ARE_TEXT;
    [SerializeField] private DirectionUI YOU_TEXT;
    [SerializeField] private DirectionUI READY_TEXT;
    [SerializeField] private DirectionUI LETSROCK_TEXT;

    [Header("디버깅")]
    private bool _isPlaying = false;
    public bool IsPlaying => _isPlaying;

    protected string _className;

    private void Awake()
    {
        this.Initialize();
    }

    private void Start()
    {
        if (CanExecute())
            this.Play();
    }

    /// <summary>
    /// 이 클래스 내부의 변수들을 초기화
    /// </summary>
    public void Initialize()
    {
        _className = this.GetType().Name;

        if (!CanExecute())
            return;

        _isPlaying = false;

        ARE_TEXT.Alpha("Instant", 0f, 0f, 0f);
        YOU_TEXT.Alpha("Instant", 0f, 0f, 0f);
        READY_TEXT.Alpha("Instant", 0f, 0f, 0f);
        LETSROCK_TEXT.Alpha("Instant", 0f, 0f, 0f);
    }

    /// <summary>
    /// 인트로 연출을 재생하는 함수
    /// 기존 재생 중이던 연출을 초기화 한다
    /// </summary>
    public void Play()
    {
        if (!CanExecute())
            return;

        ON_PLAY.Invoke();
        
        StartCoroutine(PlayIntroDirection());
    }

    private IEnumerator PlayIntroDirection()
    {
        _isPlaying = true;

        // 준비하시고...
        ON_ARE.Invoke();
        ARE_TEXT.Move("Smooth", 0.5f, "y", 200f, 150f);
        ARE_TEXT.Alpha("Lerp", 0.25f, 0f, 1f);
        yield return new WaitForSeconds(0.75f);

        ON_YOU.Invoke();
        YOU_TEXT.Move("Smooth", 0.5f, "x", -50f, 0f);
        YOU_TEXT.Alpha("Lerp", 0.25f, 0f, 1f);
        yield return new WaitForSeconds(0.35f);

        ON_READY.Invoke();
        READY_TEXT.Move("Smooth", 0.5f, "y", -200f, -150f);
        READY_TEXT.Alpha("Lerp", 0.25f, 0f, 1f);
        yield return new WaitForSeconds(1f);

        ARE_TEXT.Alpha("Lerp", 0.25f, 1f, 0f);
        YOU_TEXT.Alpha("Lerp", 0.25f, 1f, 0f);
        READY_TEXT.Alpha("Lerp", 0.25f, 1f, 0f);
        yield return new WaitForSeconds(0.3f);

        // 락 앤 롤!
        ON_LETSROCK.Invoke();
        LETSROCK_TEXT.Scale("Lerp", 0.1f, Vector3.one * 20f, Vector3.one * 12f);
        LETSROCK_TEXT.Alpha("Instant", 0f, 0f, 1f);
        yield return new WaitForSeconds(0.1f);

        LETSROCK_TEXT.Scale("Lerp", 0.75f, Vector3.one * 12f, Vector3.one * 12.5f);
        yield return new WaitForSeconds(0.75f);

        LETSROCK_TEXT.Scale("Lerp", 0.1f, Vector3.one * 12.5f, new Vector3(20f, 1f, 1f));
        LETSROCK_TEXT.Alpha("Lerp", 0.1f, 1f, 0f);
        yield return new WaitForSeconds(0.1f);

        ON_COMPLETE.Invoke();

        _isPlaying = false;
    }

    private bool CanExecute()
    {
        bool isValid = true;

        if (ARE_TEXT == null)
        {
            Debug.LogError($"{_className}: ARE_TEXT가 할당되지 않았습니다!");
            isValid = false;
        }

        if (YOU_TEXT == null)
        {
            Debug.LogError($"{_className}: YOU_TEXT가 할당되지 않았습니다!");
            isValid = false;
        }

        if (READY_TEXT == null)
        {
            Debug.LogError($"{_className}: READY_TEXT가 할당되지 않았습니다!");
            isValid = false;
        }

        if (LETSROCK_TEXT == null)
        {
            Debug.LogError($"{_className}: LETSROCK_TEXT가 할당되지 않았습니다!");
            isValid = false;
        }

        return isValid;
    }
}
