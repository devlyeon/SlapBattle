using System.Collections;
using UnityEngine;

public class PlayerIdleAnime : PlayerAnimeBase
{
    [Header("필수 사전 할당")]
    [SerializeField] private DirectionUI _player;

    /// <summary>
    /// 이 클래스 내부의 변수들을 초기화
    /// </summary>
    public override void Initialize()
    {
        Stop();
        base.Initialize();

        _player.Position(
            EaseType.Instant, 0f, new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f)
        );
        _player.Rotation(
            EaseType.Instant, 0f, new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f)
        );
        _player.Image(SPRITES_LIBRARY[1]);
        _player.Area(
            EaseType.Instant, 0f, Vector2.zero, new Vector2(SPRITES_LIBRARY[1].rect.width, SPRITES_LIBRARY[1].rect.height)
            );
    }

    protected override IEnumerator Anime()
    {
        _isPlaying = true;
        yield return null;

        // 초기화
        this.Initialize();

        ON_COMPLETE.Invoke();
    }
}
