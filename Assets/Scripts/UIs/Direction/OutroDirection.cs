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
    [SerializeField] private UnityEvent ON_WINNER;
    [SerializeField] private UnityEvent ON_PLAYER;

    [SerializeField] private TMP_Text PLAYER_TEXT;
    [SerializeField] private TMP_Text WINNER_TEXT;

    [SerializeField] private DirectionUI WINNER_PANEL;
    [SerializeField] private DirectionUI PLAYER_TEXT_DIRECTION;
    [SerializeField] private DirectionUI WINNER_TEXT_DIRECTION;

    [SerializeField] private string _nameA = "BOOGIE";
    [SerializeField] private string _nameB = "SANGZZI";

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

        WINNER_PANEL.Alpha("Instant", 0f, 0f, 0f);
        WINNER_TEXT_DIRECTION.Alpha("Instant", 0f, 0f, 0f);
        PLAYER_TEXT_DIRECTION.Alpha("Instant", 0f, 0f, 0f);
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
            StartCoroutine(PlayerAOutroDirection());
        else if (result.Equals(PlayerResult.PLAYER_B))
            StartCoroutine(PlayerBOutroDirection());
        else if (result.Equals(PlayerResult.DRAW))
            StartCoroutine(PlayerAOutroDirection());
    }

    private IEnumerator PlayerAOutroDirection()
    {
        _isPlaying = true;

        WINNER_PANEL.Anchor(new Vector2(1f, 1f), new Vector2(1f, 0f));
        PLAYER_TEXT_DIRECTION.Anchor(new Vector2(1f, 0f), new Vector2(0f, 0f));
        WINNER_TEXT_DIRECTION.Anchor(new Vector2(1f, 0f), new Vector2(1f, 0f));

        PLAYER_TEXT.text = _nameA;

        PLAYER_TEXT.alignment = TextAlignmentOptions.Right;
        WINNER_TEXT.alignment = TextAlignmentOptions.Left;

        // PLAYER...
        ON_WINNER.Invoke();
        WINNER_PANEL.Move("Smooth", 0.5f, "x", 650f, 545f);
        WINNER_PANEL.Alpha("Lerp", 0.5f, 0f, 1f);
        yield return new WaitForSeconds(0.25f);

        PLAYER_TEXT_DIRECTION.Alpha("Lerp", 0.25f, 0f, 1f);
        yield return new WaitForSeconds(0.5f);

        // WIN!!!
        ON_PLAYER.Invoke();
        WINNER_TEXT_DIRECTION.Move("Smooth", 0.5f, "x", -425f, -520f);
        WINNER_TEXT_DIRECTION.Alpha("Lerp", 0.5f, 0f, 1f);
        yield return new WaitForSeconds(0.5f);

        ON_COMPLETE.Invoke();

        _isPlaying = false;
    }

    private IEnumerator PlayerBOutroDirection()
    {
        _isPlaying = true;

        WINNER_PANEL.Anchor(new Vector2(0f, 1f), new Vector2(0f, 0f));
        PLAYER_TEXT_DIRECTION.Anchor(new Vector2(1f, 0f), new Vector2(0f, 0f));
        WINNER_TEXT_DIRECTION.Anchor(new Vector2(0f, 0f), new Vector2(0f, 0f));

        PLAYER_TEXT.text = _nameB;

        PLAYER_TEXT.alignment = TextAlignmentOptions.Left;
        WINNER_TEXT.alignment = TextAlignmentOptions.Right;

        // PLAYER...
        ON_WINNER.Invoke();
        WINNER_PANEL.Move("Smooth", 0.5f, "x", -650f, -545f);
        WINNER_PANEL.Alpha("Lerp", 0.5f, 0f, 1f);
        yield return new WaitForSeconds(0.25f);

        PLAYER_TEXT_DIRECTION.Alpha("Lerp", 0.25f, 0f, 1f);
        yield return new WaitForSeconds(0.5f);

        // WIN!!!
        ON_PLAYER.Invoke();
        WINNER_TEXT_DIRECTION.Move("Smooth", 0.5f, "x", 425f, 520f);
        WINNER_TEXT_DIRECTION.Alpha("Lerp", 0.5f, 0f, 1f);
        yield return new WaitForSeconds(0.5f);

        ON_COMPLETE.Invoke();

        _isPlaying = false;
    }

    private bool CanExecute()
    {
        bool isValid = true;

        if (WINNER_TEXT == null)
        {
            Debug.LogError($"{_className}: WINNER_TEXT가 할당되지 않았습니다!");
            isValid = false;
        }

        if (PLAYER_TEXT == null)
        {
            Debug.LogError($"{_className}: PLAYER_TEXT가 할당되지 않았습니다!");
            isValid = false;
        }

        if (WINNER_PANEL == null)
        {
            Debug.LogError($"{_className}: WINNER_PANEL가 할당되지 않았습니다!");
            isValid = false;
        }

        if (PLAYER_TEXT_DIRECTION == null)
        {
            Debug.LogError($"{_className}: PLAYER_TEXT_DIRECTION가 할당되지 않았습니다!");
            isValid = false;
        }

        if (WINNER_TEXT_DIRECTION == null)
        {
            Debug.LogError($"{_className}: WINNER_TEXT_DIRECTION가 할당되지 않았습니다!");
            isValid = false;
        }

        return isValid;
    }
}
