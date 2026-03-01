using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public enum PlayerResult
{
    DRAW = 0, PLAYER_A = 1, PLAYER_B = 2
}

public class OutroDirection : MonoBehaviour
{
    [Header("필수 사전 할당")]
    [SerializeField] private UnityEvent ON_PLAY;
    [SerializeField] private UnityEvent ON_COMPLETE;
    [SerializeField] private UnityEvent ON_DESTORY;

    [SerializeField] private Sprite PLAYER_A_BANNER;
    [SerializeField] private Sprite PLAYER_B_BANNER;

    [SerializeField] private DirectionUI DARK_DISSOLVE;
    [SerializeField] private DirectionUI WINNER_BANNER;
    [SerializeField] private DirectionUI DARK_TRANSITION;

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
        WINNER_BANNER.Alpha(EaseType.Instant, 0f, 0f, 0f);
        DARK_TRANSITION.Alpha(EaseType.Instant, 0f, 0f, 0f);
    }

    /// <summary>
    /// 인트로 연출을 재생하는 함수
    /// 기존 재생 중이던 연출을 초기화 한다
    /// </summary>
    public void Play(PlayerResult result)
    {
        if (!CanExecute())
            return;

        ON_PLAY.Invoke();

        if (result.Equals(PlayerResult.PLAYER_A))
            WINNER_BANNER.Image(PLAYER_A_BANNER);
        else if (result.Equals(PlayerResult.PLAYER_B))
            WINNER_BANNER.Image(PLAYER_B_BANNER);

        StartCoroutine(PlayerOutroDirection());
    }

    private IEnumerator PlayerOutroDirection()
    {
        _isPlaying = true;

        DARK_DISSOLVE.Alpha(EaseType.Linear, 0.1f, 0f, 1f);

        // 락 앤 롤!
        WINNER_BANNER.Move(EaseType.OutSine, 0.75f, "x", -1080f, -95f);
        WINNER_BANNER.Alpha(EaseType.Linear, 0.75f, 0f, 1f);
        yield return new WaitForSeconds(0.75f);

        WINNER_BANNER.Move(EaseType.Linear, 2f, "x", -95f, 95f);
        yield return new WaitForSeconds(2f);

        WINNER_BANNER.Move(EaseType.OutSine, 0.75f, "x", 95f, 1080f);
        WINNER_BANNER.Alpha(EaseType.Linear, 0.75f, 1f, 0f);
        DARK_TRANSITION.Alpha(EaseType.Linear, 0.1f, 0f, 1f);
        yield return new WaitForSeconds(0.75f);

        ON_COMPLETE.Invoke();
        DARK_DISSOLVE.Alpha(EaseType.Instant, 0f, 0f, 0f);
        yield return new WaitForSeconds(0.5f);

        DARK_TRANSITION.Alpha(EaseType.Linear, 0.1f, 1f, 0f);
        yield return new WaitForSeconds(0.25f);

        ON_DESTORY.Invoke();
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

        if (WINNER_BANNER == null)
        {
            Debug.LogError($"{_className}: WINNER_BANNER가 할당되지 않았습니다!");
            isValid = false;
        }

        if (DARK_TRANSITION == null)
        {
            Debug.LogError($"{_className}: DARK_TRANSITION가 할당되지 않았습니다!");
            isValid = false;
        }

        return isValid;
    }
}
