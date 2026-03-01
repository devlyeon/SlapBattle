using System.Collections;
using UnityEngine;

public class DelayedHealthBar : HealthBarBase
{
    [Header("수치 사전 설정")]
    [SerializeField] private float _delayedTime = 0.25f;
    [SerializeField] private float _delayingSpeed = 0.25f;
    
    private Coroutine _valueChangeAnimeCoroutine;

    private void Awake()
    {
        if (_isExclusive)
            this.Initialize(null);
    }

    /// <summary>
    /// HP 바의 수치를 바로 설정하는 함수
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

        if (_valueChangeAnimeCoroutine != null)
            StopCoroutine(_valueChangeAnimeCoroutine);

        _valueChangeAnimeCoroutine = StartCoroutine(ValueChangeAnime(hpValue));
    }

    private IEnumerator ValueChangeAnime(float hpValue)
    {
        // 줄어든 수치 피드백용 딜레이
        yield return new WaitForSeconds(_delayedTime);

        float initialHp = _currentHP;
        float resultHP = hpValue;

        // 딜레이 연출
        float timer = 0;
        while (timer < _delayingSpeed)
        {
            timer += Time.deltaTime;

            _currentHP = Mathf.Lerp(
                    initialHp,
                    resultHP,
                    timer / _delayingSpeed);

            HP_BAR_IMAGE.fillAmount = NormalizeToRange(_currentHP);
            yield return null;
        }

        // 마지막 값 보정
        _currentHP = resultHP;
        HP_BAR_IMAGE.fillAmount = NormalizeToRange(_currentHP);

        ON_COMPLETE.Invoke();
    }
}
