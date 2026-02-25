using UnityEngine;
using UnityEngine.UI;

public class UICooldownChecker : MonoBehaviour
{
    [Header("필수 사전 할당")]
    [SerializeField] private Image COOLDOWN_IMAGE; // 쿨타임 시계 그거

    [Header("쿨다운 수치 사전 설정")]
    private float _totalCooldownTime = 5.0f;
    private float _currentCooldownTime = 0.0f;
    private bool _isCooldown = false;

    private Coroutine _cooldownAnimeCoroutine;

    // 사전 초기화가 필요할 때 사용.
    // 아직 구현은 없는데, 만약 스크립터블로 스킬을 관리한다면,
    // 그때, 그거 받아오게 구현하면 될 듯.
    public void InitializeStats()
    {
        
    }

    // 쿨다운 도는 애니메이션 실행.
    // 총 쿨다운 시간을 넣어도 되고, 안 넣어도 됨.
    // 안넣으면 사전 설정 값을 가져옴.
    public void StartAnime(float? cooldownTime)
    {
        
    }
}
