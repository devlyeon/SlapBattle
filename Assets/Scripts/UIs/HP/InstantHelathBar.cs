using UnityEngine;

public class InstantHelathBar : HealthBarBase
{
    private void Awake()
    {
        if (_isExclusive)
            this.Initialize(null);
    }

    /// <summary>
    /// HP 바에 수치를 바로 설정하는 함수
    /// </summary>
    /// <param name="hpValue">설정하고 싶은 HP 값 (음수 X)</param>
    public override void SetHpValue(float hpValue)
    {
        if (!CanExecute())
            return;
        
        if (hpValue < 0f)
        {
            Debug.LogError($"{_className}: hpValue는 음수가 될 수 없습니다!");
            return;
        }

        ON_PLAY.Invoke();

        _currentHP = hpValue;
        HP_BAR_IMAGE.fillAmount = NormalizeToRange(_currentHP);

        ON_COMPLETE.Invoke();
    }
}
