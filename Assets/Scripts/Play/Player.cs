using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerAction
{
    PARRYING = 0, DODGE = 1, ATTACK = 2, KNOCKBACK = 3, DEFEAT = 4, WIN = 5, NONE = 6
}

public class Player : MonoBehaviour
{
    // 인스펙터에서 CooldownVisualizer를 쉽게 설정하기 위한 struct
    [Serializable]
    public struct CooldownPreset
    {
        public CooldownVisualizer parrying, dodge, attack;
    }
    
    [Header("Player Components")]
    [Tooltip("플레이어 구분을 위한 ID입니다. A는 1, B는 2로 설정해주시기 바랍니다.")]
    [SerializeField] private int playerId = 0;
    [Tooltip("플레이어 애니메이션 컨트롤을 위한 PlayerSprite class입니다.")]
    [SerializeField] private PlayerSprite playerSprite;
    [Tooltip("플레이어 체력 표시를 위한 HealthBarHandler class입니다.")]
    [SerializeField] private HealthBarHandler health;
    [Tooltip("플레이어 동작 피드백을 위한 CooldownVisualizer Preset입니다.")]
    [SerializeField] private CooldownPreset cooldownPreset;

    [Header("Another Object Components")]
    [Tooltip("다른 Player class의 function을 호출하기 위한 Player class입니다.")]
    [SerializeField] private Player anotherPlayer;
    [Tooltip("게임 결과를 표시하기 위한 OutroDirection class입니다.")]
    [SerializeField] private OutroDirection outro;

    // 현재 Player의 action입니다.
    private PlayerAction currentAction = PlayerAction.NONE;
    public PlayerAction CurrentAction => currentAction;

    // Player의 체력입니다. maxHp는 체력의 최댓값, currentHp는 현재 체력값입니다.
    private int maxHp = 100, currentHp = 100;
    public int MaxHp => maxHp;

    // 통계 및 상태 확인을 위한 값입니다.
    private bool[] actionChecker = { true, true, true }; // Parrying, Dodge, Attack -> PlayerAction과 동일
    private int[] actionCounter = { 0, 0, 0, 0 }; // Parrying, Dodge, Attack, Knockback -> PlayerAction과 동일
    private Coroutine coroutine = null;

    /// <summary>
    /// 타인에게 데미지 입을 때 쓰는 함수
    /// </summary>
    public void TryDamagePlayer(int damage = 5, bool isFatal = false)
    {
        // 예외: 패링 3번 당함
        if (isFatal)
        {
            // 상대 플레이어 승리!!
            actionCounter[(int)PlayerAction.KNOCKBACK]++;
            health.SubstractHpValue(damage);
            FinishGame(false);
            anotherPlayer.FinishGame(true);
            return;
        }

        // 패링 성공
        if (currentAction.Equals(PlayerAction.PARRYING))
        {
            actionChecker[(int)PlayerAction.PARRYING] = true;
            if (++actionCounter[(int)PlayerAction.PARRYING] >= 3)
                anotherPlayer.TryDamagePlayer(100, true);
        }

        // 회피 성공
        else if (currentAction.Equals(PlayerAction.DODGE))
        {
            actionChecker[(int)PlayerAction.DODGE] = true;
            // 매 10회 사용 시 쿨타임이 다시 돌아요
            if ((++actionCounter[(int)PlayerAction.DODGE] % 10) == 0)
                cooldownPreset.dodge.Play();
        }

        // 맞았음
        else
        {
            actionCounter[(int)PlayerAction.KNOCKBACK]++;
            currentHp -= damage;
            health.SubstractHpValue(damage);
        }

        if (currentHp <= 0)
        {
            // 상대 플레이어 승리!!
            FinishGame(false);
            anotherPlayer.FinishGame(true);
        }
    }

    void OnParrying(InputValue inputValue)
    {
        if (!GameManager.gameStarted) return;
        if (cooldownPreset.parrying.IsCooldown) return;
        SetAction(PlayerAction.PARRYING);
    }

    void OnDodge(InputValue inputValue)
    {
        if (!GameManager.gameStarted) return;
        if (cooldownPreset.dodge.IsCooldown) return;
        SetAction(PlayerAction.DODGE);
    }

    void OnAttack(InputValue inputValue)
    {
        if (!GameManager.gameStarted) return;
        if (cooldownPreset.attack.IsCooldown) return;
        anotherPlayer.TryDamagePlayer();
        SetAction(PlayerAction.ATTACK);
    }

    void SetAction(PlayerAction action)
    {
        // 현재 액션을 action으로 지정
        currentAction = action;
        playerSprite.SetSprite(currentAction);
        if (coroutine != null) {
            StopCoroutine(coroutine);
            coroutine = null;
        }

        // 쿨타임 체크 및 애니메이션
        actionChecker[(int)action] = false;
        coroutine = StartCoroutine(CanActionCoolTime());
        switch (action)
        {
            case PlayerAction.PARRYING:
                cooldownPreset.parrying.Play();
                break;
            case PlayerAction.DODGE:
                cooldownPreset.dodge.Play();
                break;
            case PlayerAction.ATTACK:
                cooldownPreset.attack.Play();
                break;
        }
    }

    /// <summary>
    /// 일정 시간 이후 Action의 상태를 변경합니다.
    /// </summary>
    IEnumerator CanActionCoolTime()
    {
        yield return new WaitForSeconds(1.0f);
        currentAction = PlayerAction.NONE;
        playerSprite.SetSprite(currentAction);
        coroutine = null;
    }

    public void FinishGame(bool isWinner)
    {
        // 승리 / 패배 모션 출력
        if (isWinner) {
            currentAction = PlayerAction.WIN;
            playerSprite.SetSprite(currentAction);
            return;
        }
        currentAction = PlayerAction.DEFEAT;
        playerSprite.SetSprite(currentAction);

        // 그리고 남은 로직은 패배 플레이어 로직에서 처리
        // 이 스크립트 실행하는게 패배 플레이어
        GameManager.gameStarted = false;

        outro.gameObject.SetActive(true);
        Debug.Log(playerId);
        if (playerId == 1)
        {
            outro.Play(PlayerResult.PLAYER_B);
        }
        else if (playerId == 2)
        {
            outro.Play(PlayerResult.PLAYER_A);
        }
    }
}
