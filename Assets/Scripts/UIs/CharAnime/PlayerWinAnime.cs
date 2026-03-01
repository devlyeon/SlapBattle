using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWinAnime : PlayerAnimeBase
{
    [Header("필수 사전 할당")]
    [SerializeField] private DirectionUI _player;

    /// <summary>
    /// 이 클래스 내부의 변수들을 초기화
    /// </summary>
    public override void Initialize()
    {
        if (_isPlaying)
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
    }

    protected override IEnumerator Anime()
    {
        _isPlaying = true;

        // 선딜
        MOTHER_CANVAS.sortingOrder = 1;
        _player.Image(SPRITES_LIBRARY[0]);
        _player.Area(
            EaseType.Instant, 0f, Vector2.zero, new Vector2(SPRITES_LIBRARY[0].rect.width, SPRITES_LIBRARY[0].rect.height)
            );
        yield return null;

        ON_COMPLETE.Invoke();
    }
}
