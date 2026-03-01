using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;

public class ResultDirection : MonoBehaviour
{
    [Header("필수 사전 할당")]
    [SerializeField] private UnityEvent ON_PLAY;
    [SerializeField] private UnityEvent ON_COMPLETE;
    [SerializeField] private UnityEvent ON_WINNERBANNER;

    [SerializeField] private Sprite PLAYER_A_BANNER;
    [SerializeField] private Sprite PLAYER_B_BANNER;

    [SerializeField] private DirectionUI WINNER_BANNER;

    [SerializeField] private Image TITLE_COVER_IMAGE;
    [SerializeField] private RectTransform TITLE_CONTENTS;
    [SerializeField] private RandomTitleText RANDOM_TITLE_TEXT;

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

        if (IsPlaying)
        {
            _isPlaying = false;
            WINNER_BANNER.Alpha(EaseType.Linear, 0.25f, 1f, 0f);
        }
        else
        {
            _isPlaying = false;
            WINNER_BANNER.Alpha(EaseType.Instant, 0f, 0f, 0f);
        }
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

        StartCoroutine(PlayerResultDirection());
    }

    private IEnumerator PlayerResultDirection()
    {
        _isPlaying = true;

        // 락 앤 롤!
        WINNER_BANNER.Scale(EaseType.Linear, 0.1f, Vector3.one * 2f, Vector3.one);
        WINNER_BANNER.Alpha(EaseType.Instant, 0f, 0f, 1f);
        yield return new WaitForSeconds(0.1f);

        ON_WINNERBANNER.Invoke();

        RANDOM_TITLE_TEXT.Initialize();

        ON_COMPLETE.Invoke();
    }

    private bool CanExecute()
    {
        bool isValid = true;

        if (WINNER_BANNER == null)
        {
            Debug.LogError($"{_className}: WINNER_BANNER가 할당되지 않았습니다!");
            isValid = false;
        }

        return isValid;
    }
}
