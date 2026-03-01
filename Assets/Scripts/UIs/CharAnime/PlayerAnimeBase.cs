using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public abstract class PlayerAnimeBase : MonoBehaviour
{
    [Header("필수 사전 할당")]
    [SerializeField] protected UnityEvent ON_PLAY;
    [SerializeField] protected UnityEvent ON_COMPLETE;
    [SerializeField] protected Sprite[] SPRITES_LIBRARY = new Sprite[] {};
    [SerializeField] protected Canvas MOTHER_CANVAS;

    [Header("수치 사전 설정")]
    [SerializeField] private PlayerType player;
    private Coroutine _animeCoroutine;

    [Header("디버깅")]
    protected bool _isPlaying = false;
    public bool IsPlaying => _isPlaying;
    protected string _className;

    /// <summary>
    /// 이 클래스 내부의 변수들을 초기화
    /// </summary>
    public virtual void Initialize()
    {
        _className = this.GetType().Name;
        _isPlaying = false;
        Stop();
        MOTHER_CANVAS.sortingOrder = 1;
    }

    /// <summary>
    /// 연출을 즉시 정지하는 함수
    /// </summary>
    public void Stop()
    {
        if (!_isPlaying)
            return;

        if (_animeCoroutine != null)
            StopCoroutine(_animeCoroutine);
    }

    /// <summary>
    /// 연출을 초기화하는 함수
    /// </summary>
    public void Reset()
    {
        this.Stop();
        this.Initialize();
    }

    /// <summary>
    /// 연출을 재생하는 함수
    /// 기존 재생 중이던 연출을 초기화 한다
    /// </summary>
    public void Play()
    {
        ON_PLAY.Invoke();

        if (_isPlaying)
            this.Reset();

        _animeCoroutine = StartCoroutine(Anime());
    }

    protected virtual IEnumerator Anime()
    {
        yield return null;
    }

    /// <summary>
    /// 어떤 수를 양수로 정규화하는 함수
    /// </summary>
    /// <param name="value">정규화하고 싶은 값</param>
    /// <returns>0 또는 양수로 정규화된 값</returns>
    protected float Normalize(float value)
    {
        if (value < 0f)
        {
            Debug.LogWarning($"{_className}: value는 음수가 될 수 없습니다!");
            return 0f;
        }

        return value;
    }

    /// <summary>
    /// 좌표의 X값 수치를 플레이어의 위치에 맞게 조정하는 함수.
    /// </summary>
    /// <param name="value">조정하고 싶은 값</param>
    /// <returns>조정된 값</returns>
    protected float Reposition(float value)
    {
        if (player == PlayerType.PLAYER_B)
            return -value;

        return value;
    }
}

public enum PlayerType
{
    PLAYER_A = 1, PLAYER_B = 2
}