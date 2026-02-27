using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class HealthBarBase : MonoBehaviour
{
    [Header("필수 사전 할당")]
    [SerializeField] protected UnityEvent ON_PLAY;
    [SerializeField] protected UnityEvent ON_COMPLETE;
    [SerializeField] protected Image HP_BAR_IMAGE;

    [Header("수치 사전 설정")]
    [SerializeField] protected float _maxHP = 100f;
    [SerializeField] protected float _currentHP = 100f;

    [Header("디버깅")]
    [SerializeField] protected bool _isExclusive = false;
    protected string _className;

    /// <summary>
    /// 이 클래스 내부의 변수들을 초기화
    /// </summary>
    public virtual void Initialize(float? maxHP)
    {
        if (maxHP != null)
            _maxHP = maxHP ?? _maxHP;

        _currentHP = _maxHP;
        _className = this.GetType().Name;
    }

    public abstract void SetHpValue(float hpValue); // HP 값 설정하기

    /// <summary>
    /// 이 클래스 내부의 함수를 실행할 수 있는 지를 검사하는 함수
    /// </summary>
    /// <returns>가능, 불가능</returns>
    protected bool CanExecute()
    {
        bool isValid = true;

        if (HP_BAR_IMAGE == null)
        {
            Debug.LogError($"{_className}: HP_BAR_IMAGE가 할당되지 않았습니다!");
            isValid = false;
        }

        if (_maxHP < 0f)
        {
            Debug.LogError($"{_className}: HP 최대값은 음수가 될 수 없습니다!");
            isValid = false;
        }

        if (_currentHP < 0f)
        {
            Debug.LogError($"{_className}: HP 현재값은 음수가 될 수 없습니다!");
            isValid = false;
        }

        if (_currentHP > _maxHP)
        {
            Debug.LogError($"{_className}: HP 현재값은 최대값보다 클 수 없습니다!");
            isValid = false;
        }

        return isValid;
    }

    /// <summary>
    /// 최대값 0 ~ M 범위의 어떤 값을 0 ~ 1의 값으로 정규화하는 함수
    /// </summary>
    /// <param name="value">정규화하고 싶은 값 (음수 X)</param>
    /// <returns>0 ~ 1 범위로 정규화된 값</returns>
    protected float NormalizeToRange(float value)
    {
        if (value < 0f)
        {
            Debug.LogError($"{_className}: value는 음수가 될 수 없습니다!");
            return 0f;
        }

        return value / _maxHP;
    }
}
