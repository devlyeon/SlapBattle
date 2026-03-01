using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHitAnime : PlayerAnimeBase
{
    [Header("필수 사전 할당")]
    [SerializeField] private DirectionUI _player;

    [Header("수치 사전 설정")]
    [SerializeField] private float _startUpTime;
    [SerializeField] private float _recoveryTime;

    /// <summary>
    /// 이 클래스 내부의 변수들을 초기화
    /// </summary>
    public override void Initialize()
    {
        if (_isPlaying)
        {
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

        // 후딜
        MOTHER_CANVAS.sortingOrder = 1;
        _player.Image(SPRITES_LIBRARY[0]);
        _player.Area(
            EaseType.Instant, 0f, Vector2.zero, new Vector2(SPRITES_LIBRARY[0].rect.width, SPRITES_LIBRARY[0].rect.height)
            );
        yield return null;

        _player.Position(
            EaseType.OutQuart, _startUpTime / 2, new Vector3(Reposition(-60f), 0f, 0f), new Vector3(Reposition(-140f), 40f, 0f)
        );
        _player.Rotation(
            EaseType.OutQuart, _startUpTime / 2, new Vector3(0f, 0f, Reposition(-5f)), new Vector3(0f, 0f, Reposition(0f))
        );
        yield return new WaitForSeconds(_startUpTime / 2);

        _player.Position(
            EaseType.InQuart, _startUpTime / 2, new Vector3(Reposition(-140f), 40f, 0f), new Vector3(Reposition(-220f), 0f, 0f)
        );
        _player.Rotation(
            EaseType.InQuart, _startUpTime / 2, new Vector3(0f, 0f, Reposition(0f)), new Vector3(0f, 0f, Reposition(5f))
        );
        yield return new WaitForSeconds(_startUpTime / 2);

        _player.Position(
            EaseType.InQuart, _recoveryTime, new Vector3(Reposition(-220f), 0f, 0f), new Vector3(Reposition(-60f), 0f, 0f)
        );
        _player.Rotation(
            EaseType.Linear, _recoveryTime, new Vector3(0f, 0f, Reposition(5f)), new Vector3(0f, 0f, Reposition(0f))
        );
        yield return new WaitForSeconds(_recoveryTime);

        // 초기화
        this.Initialize();
        _isPlaying = false;

        ON_COMPLETE.Invoke();
    }
}
