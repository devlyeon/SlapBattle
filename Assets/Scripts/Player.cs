using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private Player anotherPlayer;

    private int guardCount = 0; // 방어 횟수
    private int avoidCount = 0; // 회피 횟수
    private int attackCount = 0; // 공격 횟수
    private int defeatCount = 0; // 맞은 횟수

    private bool isGuard = false;
    private bool isAvoid = false, canAvoid = true;
    private bool isAttack = false;

    /// <summary>
    /// 타인에게 데미지 입을 때 쓰는 함수
    /// </summary>
    public void TryDamagePlayer(int attack = 1)
    {
        // 방어 / 회피 성공
        if (isGuard)
        {
            isGuard = false;
            anotherPlayer.TryDamagePlayer(2);
        }
        else if (isAvoid)
        {
            if ((avoidCount % 10) == 0)
            {
                canAvoid = false;
                StartCoroutine(AvoidCoolTime(3.0f));
            }
        }
        // 맞았음
        else
        {
            defeatCount += attack;
            Debug.Log(gameObject.name + defeatCount.ToString());
        }
    }

    public bool GetIsGuard() { return isGuard; }
    private bool GetIsAvoid() { return isAvoid; }

    void OnGuard(InputValue inputValue)
    {
        isGuard = inputValue.isPressed;
        if (isGuard) guardCount++;
    }

    void OnAvoid(InputValue inputValue)
    {
        if (canAvoid) isAvoid = inputValue.isPressed;
        if (isAvoid) {
            avoidCount++;
            StartCoroutine(AvoidTimeOver());
        }
    }

    void OnAttack(InputValue inputValue)
    {
        isAttack = inputValue.isPressed;
        if (isAttack) {
            attackCount++;
            anotherPlayer.TryDamagePlayer();
        }
    }

    IEnumerator AvoidTimeOver()
    {
        yield return new WaitForSeconds(1.0f);
        isAvoid = false;
    }

    IEnumerator AvoidCoolTime(float coolTime)
    {
        yield return new WaitForSeconds(coolTime);
        canAvoid = true;
    }

    void Update()
    {
        if (isGuard)
        {
            // 가드 이미지
            Debug.Log("가드" + gameObject.name);
        }
        else if (isAvoid)
        {
            // 회피 이미지
            Debug.Log("회피" + gameObject.name);
        }
    }
}
