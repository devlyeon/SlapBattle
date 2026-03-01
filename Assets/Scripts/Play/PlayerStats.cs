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

    [Header("후딜레이 (After Delay)")]
    [Tooltip("공격 후딜레이 시간 (초)")]
    public float attackRecoveryTime;

    [Tooltip("회피 후딜레이 시간 (초)")]
    public float dodgeRecoveryTime;

    [Tooltip("패리 후딜레이 시간 (초)")]
    public float parryRecoveryTime;

    [Header("쿨타임 (Cooldown)")]
    [Tooltip("공격 쿨타임 (초)")]
    public float attackCooldown;

    [Tooltip("회피 쿨타임 (초)")]
    public float dodgeCooldown;

    [Tooltip("패리 쿨타임 (초)")]
    public float parryCooldown;
}
