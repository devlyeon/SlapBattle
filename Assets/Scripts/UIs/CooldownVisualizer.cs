using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class CooldownVisualizer : MonoBehaviour
{
    [Header("필수 사전 할당")]
    [SerializeField] private Image COOLDOWN_COVER_IMAGE;
    [SerializeField] private FeedbackVisualizer FEEDBACK_VISUALIZER;
    [SerializeField] private TMP_Text COOLDOWN_TEXT;
    [SerializeField] private CanvasGroup COOLDOWN_TEXT_CANVASGROUP;

    [Header("유니티 이벤트 시스템")]
    [SerializeField] private UnityEvent ON_PLAY_UNITYEVENT;
    [SerializeField] private UnityEvent ON_COMPLETE_UNITYEVENT;

    [Header("쿨다운 수치 사전 설정")]
    [SerializeField] private float _totalCooldownTime = 1.0f;
    [SerializeField] private bool _isCooldown = false;

    public float TotalCooldownTime => _totalCooldownTime;
    public bool IsCooldown => _isCooldown;

    private Coroutine _cooldownAnimeCoroutine;

    private void Start()
    {
        this.Initialize();
    }

    // 사전 초기화가 필요할 때 사용
    public void Initialize()
    {
        COOLDOWN_COVER_IMAGE.fillAmount = 0f;
        COOLDOWN_TEXT.text = _totalCooldownTime.ToString();
        COOLDOWN_TEXT_CANVASGROUP.alpha = 0f;
        _isCooldown = false;
    }

    // 쿨다운 연출을 정지
    public void Stop()
    {
        if (_cooldownAnimeCoroutine != null)
            StopCoroutine(_cooldownAnimeCoroutine);
    }

    // 쿨다운 연출을 초기화
    public void Reset()
    {
        this.Stop();
        this.Initialize();
    }

    // 쿨다운 연출을 재생
    public void Play()
    {
        if (_isCooldown)
            this.Reset();
        _cooldownAnimeCoroutine = StartCoroutine(CooldownAnime());
    }

    // 쿨다운 도는 애니메이션
    private IEnumerator CooldownAnime()
    {
        // 현출 시작
        _isCooldown = true;
        ON_PLAY_UNITYEVENT.Invoke();
        COOLDOWN_COVER_IMAGE.fillAmount = 1f;
        COOLDOWN_TEXT_CANVASGROUP.alpha = 1f;

        // 쿨타임 돌리기
        float timer = 0;
        while (timer < _totalCooldownTime)
        {
            timer += Time.deltaTime;
            COOLDOWN_COVER_IMAGE.fillAmount = 1 - (timer / _totalCooldownTime);

            if ((int)_totalCooldownTime - timer >= 1f)
                COOLDOWN_TEXT.text = ((int)(_totalCooldownTime - timer)).ToString();
            else
                COOLDOWN_TEXT.text = Math.Round(_totalCooldownTime - timer, 1).ToString();

            yield return null;
        }

        // 연출 초기화
        ON_COMPLETE_UNITYEVENT.Invoke();
        COOLDOWN_COVER_IMAGE.fillAmount = 0f;
        FEEDBACK_VISUALIZER.Play();
        COOLDOWN_TEXT_CANVASGROUP.alpha = 0f;
        _isCooldown = false;
        this.Reset();
        yield return new WaitForSeconds(FEEDBACK_VISUALIZER.TotalFeedbackTime);

        FEEDBACK_VISUALIZER.Reset();
        yield return null;
    }
}
