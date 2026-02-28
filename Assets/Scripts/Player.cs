using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerAction
{
    NONE = 0, GUARD = 1, AVOID = 2, ATTACK = 3, DEFEAT = 4, WIN = 5
}

public class Player : MonoBehaviour
{
    
    [SerializeField] private int playerId = 0;
    [SerializeField] private Player anotherPlayer;
    [SerializeField] private PlayerSprite playerSprite;
    [SerializeField] private HealthBarHandler health;
    [SerializeField] private CooldownVisualizer guardCooldown, avoidCooldown, attackCooldown;
    [SerializeField] private OutroDirection outro;

    private int maxHp = 100, currentHp = 100;

    public int MaxHp => maxHp;

    private int guardCount = 0; // 방어 횟수
    private int avoidCount = 0; // 회피 횟수
    private int attackCount = 0; // 공격 횟수
    private int defeatCount = 0; // 맞은 횟수

    private PlayerAction currentAction = PlayerAction.NONE;
    private bool canGuard = true, canAvoid = true, canAttack = true;
    private Coroutine coroutine = null;

    void Start()
    {
        
    }

    public PlayerAction GetPlayerAction() { return currentAction; }
    public void SetPlayerAction(PlayerAction action) { currentAction = action; }

    /// <summary>
    /// 타인에게 데미지 입을 때 쓰는 함수
    /// </summary>
    public void TryDamagePlayer(int attack = 5, bool isFatal = false)
    {
        // 예외: 패링 3번 당함 ㅋ
        if (isFatal)
        {
            // 상대 플레이어 승리!!
            defeatCount++;
            health.SubstractHpValue(attack);
            FinishGame(false);
            anotherPlayer.FinishGame(true);
            return;
        }

        // 패링 성공
        if (currentAction.Equals(PlayerAction.GUARD))
        {
            canGuard = true;
            guardCount++;

            if (guardCount >= 3)
            {
                anotherPlayer.TryDamagePlayer(100, true);
            }
        }
        // 회피 성공
        else if (currentAction.Equals(PlayerAction.AVOID))
        {
            canAvoid = true;
            avoidCount++;

            if ((avoidCount % 10) == 0)
            {
                avoidCooldown.Play();
            }
        }
        // 맞았음
        else
        {
            defeatCount++;
            currentHp -= attack;
            health.SubstractHpValue(attack);
        }

        if (currentHp <= 0)
        {
            // 상대 플레이어 승리!!
            FinishGame(false);
            anotherPlayer.FinishGame(true);
        }
    }

    void OnGuard(InputValue inputValue)
    {
        if (!Countdown.gameStarted) return;
        if (guardCooldown.IsCooldown) return;

        // 현재 액션을 Guard로 지정
        currentAction = PlayerAction.GUARD;
        playerSprite.SetSprite(currentAction);
        if (coroutine != null) {
            StopCoroutine(coroutine);
            coroutine = null;
        }

        // 쿨타임 체크 및 애니메이션
        canGuard = false;
        coroutine = StartCoroutine(CanActionCoolTime());
        guardCooldown.Play();
    }

    void OnAvoid(InputValue inputValue)
    {
        if (!Countdown.gameStarted) return;
        if (avoidCooldown.IsCooldown) return;

        // 현재 액션을 Avoid로 지정
        currentAction = PlayerAction.AVOID;
        playerSprite.SetSprite(currentAction);
        if (coroutine != null) {
            StopCoroutine(coroutine);
            coroutine = null;
        }

        // 쿨타임 체크 및 애니메이션
        canAvoid = false;
        coroutine = StartCoroutine(CanActionCoolTime());
        avoidCooldown.Play();
    }

    void OnAttack(InputValue inputValue)
    {
        if (!Countdown.gameStarted) return;
        if (attackCooldown.IsCooldown) return;

        // 현재 액션을 Attack으로 지정
        currentAction = PlayerAction.ATTACK;
        anotherPlayer.TryDamagePlayer();
        playerSprite.SetSprite(currentAction);
        if (coroutine != null) {
            StopCoroutine(coroutine);
            coroutine = null;
        }

        // 동작을 1.0초동안 실행
        coroutine = StartCoroutine(CanActionCoolTime());
        attackCooldown.Play();
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

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        
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
        Countdown.gameStarted = false;

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
