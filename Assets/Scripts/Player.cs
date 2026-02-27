using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public enum PlayerAction
{
    NONE = 0, GUARD = 1, AVOID = 2, ATTACK = 3
}

public class Player : MonoBehaviour
{
    [Serializable]
    public struct PlayerAnims
    {
        public Sprite idle, parrying, dodge, attack;
    }

    [SerializeField] private Player anotherPlayer;
    [SerializeField] private PlayerSprite playerSprite;
    [SerializeField] private Image hpGage;
    [SerializeField] private int maxHp = 15;
    [SerializeField] private PlayerAnims anims;
    [SerializeField] private CooldownVisualizer guardCooldown, avoidCooldown, attackCooldown;

    private int guardCount = 0; // 방어 횟수
    private int avoidCount = 0; // 회피 횟수
    private int attackCount = 0; // 공격 횟수
    private int defeatCount = 0; // 맞은 횟수
    private int currentHp = 0;

    private PlayerAction currentAction = PlayerAction.NONE;
    private bool canGuard = true, canAvoid = true, canAttack = true;
    private Coroutine coroutine = null;

    void Start()
    {
        currentHp = maxHp;
    }

    public PlayerAction GetPlayerAction() { return currentAction; }
    public void SetPlayerAction(PlayerAction action) { currentAction = action; }

    /// <summary>
    /// 타인에게 데미지 입을 때 쓰는 함수
    /// </summary>
    public void TryDamagePlayer(int attack = 1)
    {
        // 패링 / 회피 성공
        if (currentAction.Equals(PlayerAction.GUARD))
        {

            canGuard = true;
            // 카운트 하는거 따로 추가하기
        }
        else if (currentAction.Equals(PlayerAction.AVOID))
        {

            canAvoid = true;
            if ((avoidCount % 10) == 0)
            {
                avoidCooldown.Play();
            }
        }
        // 맞았음
        else
        {
            defeatCount += attack;
            currentHp -= attack;
            Debug.Log(gameObject.name + defeatCount.ToString());
        }

        if (currentHp <= 0)
        {
            // 상대 플레이어 승리!!
        }
    }

    void OnGuard(InputValue inputValue)
    {
        if (!Countdown.gameStarted) return;
        if (guardCooldown.IsCooldown) return;

        // 현재 액션을 Guard로 지정
        currentAction = PlayerAction.GUARD;
        playerSprite.SetSprite(currentAction);

        // 쿨타임 체크 및 애니메이션
        canGuard = false;
        coroutine = StartCoroutine(CanActionCoolTime());
        guardCooldown.Play();

        guardCount++;
    }

    void OnAvoid(InputValue inputValue)
    {
        if (!Countdown.gameStarted) return;
        if (avoidCooldown.IsCooldown) return;

        // 현재 액션을 Avoid로 지정
        currentAction = PlayerAction.AVOID;
        playerSprite.SetSprite(currentAction);

        // 쿨타임 체크 및 애니메이션
        canAvoid = false;
        coroutine = StartCoroutine(CanActionCoolTime());
        avoidCooldown.Play();


        avoidCount++;
    }

    void OnAttack(InputValue inputValue)
    {
        if (!Countdown.gameStarted) return;
        if (attackCooldown.IsCooldown) return;

        // 현재 액션을 Attack으로 지정
        currentAction = PlayerAction.ATTACK;
        anotherPlayer.TryDamagePlayer(1);
        playerSprite.SetSprite(currentAction);

        // 동작을 1.0초동안 실행
        coroutine = StartCoroutine(CanActionCoolTime());
        attackCooldown.Play();

        attackCount++;
    }

    /// <summary>
    /// 일정 시간 이후 Action의 상태를 변경합니다.
    /// </summary>
    /// <param name="coolTime"></param>
    IEnumerator CanActionCoolTime()
    {
        yield return new WaitForSeconds(1.0f);
        currentAction = PlayerAction.NONE;
        playerSprite.SetSprite(currentAction);
    }

    void Update()
    {
        if (currentAction != PlayerAction.NONE)
        {
            Debug.Log(gameObject.name + ": " + currentAction);
        }
    }

    void FixedUpdate()
    {
        hpGage.fillAmount = (float)(currentHp / (float)maxHp);
    }
}
