using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerStats", menuName = "Game/Player Stats")]
public class PlayerStats : ScriptableObject
{
    [Header("기본 능력치")]
    [Tooltip("캐릭터의 최대 체력")]
    public int maxHealth;

    [Tooltip("캐릭터의 최대 쉴드량")]
    public int shieldAmount;

    [Tooltip("기본 공격력")]
    public int attackPower;

    [Header("선딜레이 (Before Delay)")]
    [Tooltip("공격 선딜레이 시간 (초)")]
    public float attackStartupTime;

    [Tooltip("회피 선딜레이 시간 (초)")]
    public float dodgeStartupTime;

    [Tooltip("패리 선딜레이 시간 (초)")]
    public float parryStartupTime;

    [Header("판정 시간 (Active)")]
    [Tooltip("공격 판정 (초)")]
    public float attackActive;

    [Tooltip("회피 판정 (초)")]
    public float dodgeActive;

    [Tooltip("패리 판정 (초)")]
    public float parryActive;

    [Header("후딜레이 (After Delay)")]
    [Tooltip("공격 후딜레이 시간 (초)")]
    public float attackRecoveryTime;

    [Tooltip("회피 후딜레이 시간 (초)")]
    public float dodgeRecoveryTime;

    [Tooltip("패리 후딜레이 시간 (초)")]
    public float parryRecoveryTime;

    [Tooltip("넉백 후딜레이 시간 (초)")]
    public float knockbackRecoveryTime;

    [Header("쿨타임 (Cooldown)")]
    [Tooltip("공격 쿨타임 (초)")]
    public float attackCooldown;

    [Tooltip("회피 쿨타임 (초)")]
    public float dodgeCooldown;

    [Tooltip("패리 쿨타임 (초)")]
    public float parryCooldown;
}
