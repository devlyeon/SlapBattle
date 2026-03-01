using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackAnime : PlayerAnimeBase
{
    [Header("필수 사전 할당")]
    [SerializeField] private DirectionUI _player;
    [SerializeField] private DirectionUI _effect;

    [Header("수치 사전 설정")]
    [SerializeField] private float _startUpTime;
    [SerializeField] private float _activeTime;
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
            _player.Image(SPRITES_LIBRARY[2]);
            _player.Area(
                EaseType.Instant, 0f, Vector2.zero, new Vector2(SPRITES_LIBRARY[2].rect.width, SPRITES_LIBRARY[2].rect.height)
                );
            _effect.Alpha(
                EaseType.Instant, 0f, 0f, 0f
            );
        }
    }

    protected override IEnumerator Anime()
    {
        _isPlaying = true;

        // 선딜
        MOTHER_CANVAS.sortingOrder = 2;
        _player.Image(SPRITES_LIBRARY[0]);
        _player.Area(
            EaseType.Instant, 0f, Vector2.zero, new Vector2(SPRITES_LIBRARY[0].rect.width, SPRITES_LIBRARY[0].rect.height)
            );
        yield return null;

        _player.Position(
            EaseType.Linear, _startUpTime, new Vector3(Reposition(42f), 25f, 0f), new Vector3(Reposition(475f), -75f, 0f)
        );
        _player.Rotation(
            EaseType.Linear, _startUpTime, new Vector3(0f, 0f, Reposition(15f)), new Vector3(0f, 0f, Reposition(-5f))
        );
        _effect.Position(
            EaseType.OutQuart, _startUpTime + _activeTime + 0.1f, new Vector3(Reposition(-465f), 130f, 0f), new Vector3(Reposition(700f), -125f, 0f)
        );
        _effect.Alpha(
            EaseType.InOutQuart, _startUpTime + _activeTime + 0.1f, 1f, 0f
        );
        yield return new WaitForSeconds(_startUpTime);

        // 판정
        _player.Image(SPRITES_LIBRARY[1]);
        _player.Area(
            EaseType.Instant, 0f, Vector2.zero, new Vector2(SPRITES_LIBRARY[1].rect.width, SPRITES_LIBRARY[1].rect.height)
            );
        yield return null;

        _player.Position(
            EaseType.OutQuart, _activeTime, new Vector3(Reposition(475f), -75f, 0f), new Vector3(Reposition(540f), -125f, 0f)
        );
        _player.Rotation(
            EaseType.OutQuart, _activeTime, new Vector3(0f, 0f, Reposition(-5f)), new Vector3(0f, 0f, Reposition(-10f))
        );
        yield return new WaitForSeconds(_activeTime);

        // 후딜
        _player.Position(
            EaseType.InQuart, _recoveryTime, new Vector3(Reposition(540f), -125f, 0f), new Vector3(Reposition(42f), 25f, 0f)
        );
        _player.Rotation(
            EaseType.InQuart, _recoveryTime, new Vector3(0f, 0f, Reposition(-10f)), new Vector3(0f, 0f, Reposition(15f))
        );
        yield return new WaitForSeconds(_recoveryTime);

        // 초기화
        this.Initialize();
        _isPlaying = false;

        ON_COMPLETE.Invoke();
    }
}
