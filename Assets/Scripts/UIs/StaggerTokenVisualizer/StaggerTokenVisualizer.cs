using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StaggerTokenVisualizer : MonoBehaviour
{
    [Header("필수 사전 할당")]
    [SerializeField] private UnityEvent ON_PLAY;
    [SerializeField] private UnityEvent ON_COMPLETE;
    private List<StaggerToken> SAVED_STRAGGER_TOKEN = new List<StaggerToken> {};
    [SerializeField] private StaggerToken STRAGGER_TOKEN;

    [Header("수치 사전 설정")]
    private int MAX_SHIELD_COUNT = 3;
    private int _currentShiledCount = 3;

    [Header("디버깅")]
    protected string _className;

    private void Awake()
    {
        this.Initialize();
    }

    /// <summary>
    /// 이 클래스 내부의 변수들을 초기화
    /// </summary>
    public void Initialize()
    {
        _currentShiledCount = MAX_SHIELD_COUNT;
        _className = this.GetType().Name;

        for (int i = 0; i < MAX_SHIELD_COUNT; i++)
        {
            StaggerToken staggerTokenObj = Instantiate(STRAGGER_TOKEN, gameObject.transform);
            SAVED_STRAGGER_TOKEN.Add(staggerTokenObj);
            
            SAVED_STRAGGER_TOKEN[i].Initialize();
        }
    }

    /// <summary>
    /// HP 바에 수치를 더하는 함수
    /// </summary>
    public void RecoveryShieldValue(int value)
    {
        int shieldValue = SanitizeValue(value);

        for (int i = _currentShiledCount; i < _currentShiledCount + shieldValue; i++)
        {
            SAVED_STRAGGER_TOKEN[i - 1].Initialize();
        }

        _currentShiledCount += shieldValue;
    }

    /// <summary>
    /// HP 바에 수치를 빼는 함수
    /// </summary>
    public void BreakShieldValue(int value)
    {
        int shieldValue = SanitizeValue(value);

        for (int i = _currentShiledCount; i > _currentShiledCount - shieldValue; i--)
        {
            SAVED_STRAGGER_TOKEN[i - 1].Play();
        }

        _currentShiledCount -= shieldValue;
    }

    private int SanitizeValue(int value)
    {
        if (value > MAX_SHIELD_COUNT)
            return MAX_SHIELD_COUNT;
        
        if (value < 0)
            return 0;

        return value;
    }
}
