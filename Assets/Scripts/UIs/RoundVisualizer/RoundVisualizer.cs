using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoundVisualizer : MonoBehaviour
{
    [Header("필수 사전 할당")]
    [SerializeField] private UnityEvent ON_PLAY;
    [SerializeField] private UnityEvent ON_COMPLETE;
    [SerializeField] private List<DirectionUI> SAVED_ROUND_TOKEN = new List<DirectionUI> {};
    private int CURRENT_ROUND = 2;

    public void Initialize()
    {
        CURRENT_ROUND = 2;

        SAVED_ROUND_TOKEN[0].Move(EaseType.Instant, 0f, "y", 0f, 0f);
        SAVED_ROUND_TOKEN[0].Alpha(EaseType.Instant, 0f, 0f, 1f);

        SAVED_ROUND_TOKEN[1].Move(EaseType.Instant, 0f, "y", 0f, 0f);
        SAVED_ROUND_TOKEN[1].Alpha(EaseType.Instant, 0f, 0f, 1f);
    }

    public void SubstractRound()
    {
        CURRENT_ROUND -= 1;
        SAVED_ROUND_TOKEN[CURRENT_ROUND].Move(EaseType.OutCubic, 1f, "y", 0f, -15f);
        SAVED_ROUND_TOKEN[CURRENT_ROUND].Alpha(EaseType.Linear, 1f, 1f, 0f);
    }
}
