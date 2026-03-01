using UnityEngine;

public class HealthBarHandler : MonoBehaviour
{
    [Header("필수 사전 할당")]
    [SerializeField] private DelayedHealthBar DELAYED_HEALTH_BAR;
    [SerializeField] private InstantHelathBar INSTANT_HEALTH_BAR;
    [SerializeField] private Player PLAYER;

    [Header("수치 사전 설정")]
    private float _maxHP = 100f;
    private float _currentHP = 100f;

    [Header("디버깅")]
    protected string _className;

    private void Start()
    {
        this.Initialize();
        
        DELAYED_HEALTH_BAR.Initialize(_maxHP);
        INSTANT_HEALTH_BAR.Initialize(_maxHP);
    }

    /// <summary>
    /// 이 클래스 내부의 변수들을 초기화
    /// </summary>
    public void Initialize()
    {
        _maxHP = PLAYER.MaxHp;
        _currentHP = _maxHP;
        _className = this.GetType().Name;
    }

    /// <summary>
    /// HP 바에 수치를 더하는 함수
    /// </summary>
    public void AddHpValue(float value)
    {
        float resultValue = SanitizeValue(_currentHP + value);
        SetHealthBar(resultValue);
    }

    /// <summary>
    /// HP 바에 수치를 빼는 함수
    /// </summary>
    public void SubstractHpValue(float value)
    {
        float resultValue = SanitizeValue(_currentHP - value);
        SetHealthBar(resultValue);
    }

    /// <summary>
    /// HP 바의 수치를 설정하는 함수
    /// </summary>
    public void SetHpValue(float value)
    {
        float resultValue = SanitizeValue(value);
        SetHealthBar(resultValue);
    }

    private float SanitizeValue(float value)
    {
        if (value > _maxHP)
            return _maxHP;
        
        if (value < 0f)
            return 0f;

        return value;
    }

    private void SetHealthBar(float value)
    {
        _currentHP = value;
        DELAYED_HEALTH_BAR.SetHpValue(_currentHP);
        INSTANT_HEALTH_BAR.SetHpValue(_currentHP);
    }
}
