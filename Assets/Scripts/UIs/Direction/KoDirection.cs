using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class KoDirection : MonoBehaviour
{
    [Header("필수 사전 할당")]
    [SerializeField] private UnityEvent ON_PLAY;
    [SerializeField] private UnityEvent ON_COMPLETE;
    [SerializeField] private UnityEvent ON_KO;

    [SerializeField] private DirectionUI DARK_DISSOLVE;
    [SerializeField] private DirectionUI KO_TEXT;
    [SerializeField] private DirectionUI WHITE_DISSOLVE;

    [Header("디버깅")]
    private bool _isPlaying = false;
    public bool IsPlaying => _isPlaying;

    protected string _className;

    private void Awake()
    {
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

        _isPlaying = false;

        DARK_DISSOLVE.Alpha(EaseType.Instant, 0f, 0f, 0f);
        KO_TEXT.Alpha(EaseType.Instant, 0f, 0f, 0f);
        WHITE_DISSOLVE.Alpha(EaseType.Instant, 0f, 0f, 0f);
    }

    /// <summary>
    /// 인트로 연출을 재생하는 함수
    /// 기존 재생 중이던 연출을 초기화 한다
    /// </summary>
    public void Play(bool isEnd)
    {
        if (!CanExecute())
            return;

        ON_PLAY.Invoke();

        if (!isEnd)
            StartCoroutine(PlayerKoDirection());
        else
            StartCoroutine(PlayerKoEndDirection());
    }

    private IEnumerator PlayerKoDirection()
    {
        _isPlaying = true;

        DARK_DISSOLVE.Alpha(EaseType.Linear, 0.1f, 0f, 1f);

        // 락 앤 롤!
        KO_TEXT.Scale(EaseType.Linear, 0.1f, Vector3.one * 20f, Vector3.one * 12f);
        KO_TEXT.Alpha(EaseType.Instant, 0f, 0f, 1f);
        yield return new WaitForSeconds(0.1f);

        ON_KO.Invoke();
        KO_TEXT.Scale(EaseType.Linear, 0.75f, Vector3.one * 12f, Vector3.one * 12.5f);
        WHITE_DISSOLVE.Alpha(EaseType.Linear, 0.25f, 1f, 0f);
        yield return new WaitForSeconds(0.75f);

        DARK_DISSOLVE.Alpha(EaseType.Linear, 0.1f, 1f, 0f);
        KO_TEXT.Scale(EaseType.Linear, 0.1f, Vector3.one * 12.5f, new Vector3(20f, 1f, 1f));
        KO_TEXT.Alpha(EaseType.Linear, 0.1f, 1f, 0f);
        yield return new WaitForSeconds(0.1f);

        ON_COMPLETE.Invoke();

        _isPlaying = false;
    }

    private IEnumerator PlayerKoEndDirection()
    {
        _isPlaying = true;

        DARK_DISSOLVE.Alpha(EaseType.Linear, 0.1f, 0f, 1f);

        // 락 앤 롤!
        KO_TEXT.Scale(EaseType.Linear, 0.1f, Vector3.one * 20f, Vector3.one * 12f);
        KO_TEXT.Alpha(EaseType.Instant, 0f, 0f, 1f);
        yield return new WaitForSeconds(0.1f);

        ON_KO.Invoke();
        KO_TEXT.Scale(EaseType.Linear, 2.5f, Vector3.one * 12f, Vector3.one * 12.5f);
        WHITE_DISSOLVE.Alpha(EaseType.Linear, 0.25f, 1f, 0f);
        yield return new WaitForSeconds(0.25f);

        WHITE_DISSOLVE.Alpha(EaseType.Linear, 2.25f, 0f, 1f);
        yield return new WaitForSeconds(2.25f);

        WHITE_DISSOLVE.Alpha(EaseType.Instant, 0f, 0f, 0f);

        // DARK_DISSOLVE.Alpha(EaseType.Linear, 0.1f, 1f, 0f);
        // KO_TEXT.Scale(EaseType.Linear, 0.1f, Vector3.one * 12.5f, new Vector3(20f, 1f, 1f));
        // KO_TEXT.Alpha(EaseType.Linear, 0.1f, 1f, 0f);
        // yield return new WaitForSeconds(0.1f);

        ON_COMPLETE.Invoke();

        _isPlaying = false;
    }

    private bool CanExecute()
    {
        bool isValid = true;

        if (DARK_DISSOLVE == null)
        {
            Debug.LogError($"{_className}: DARK_DISSOLVE가 할당되지 않았습니다!");
            isValid = false;
        }

        if (KO_TEXT == null)
        {
            Debug.LogError($"{_className}: KO_TEXT가 할당되지 않았습니다!");
            isValid = false;
        }

        if (WHITE_DISSOLVE == null)
        {
            Debug.LogError($"{_className}: WHITE_DISSOLVE가 할당되지 않았습니다!");
            isValid = false;
        }

        return isValid;
    }
}
