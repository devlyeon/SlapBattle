using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private List<RectTransform> targets = new List<RectTransform>();

    private Dictionary<RectTransform, Vector2> originalPositions =
        new Dictionary<RectTransform, Vector2>();

    private Coroutine shakeCoroutine;

    private void Awake()
    {
        foreach (var target in targets)
        {
            if (target != null && !originalPositions.ContainsKey(target))
            {
                originalPositions.Add(target, target.anchoredPosition);
            }
        }
    }

    public void ShakeAll()
    {
        if (shakeCoroutine != null)
            StopCoroutine(shakeCoroutine);

        shakeCoroutine = StartCoroutine(ShakeCoroutine(0.5f, 20f));
    }

    private IEnumerator ShakeCoroutine(float duration, float intensity)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float progress = elapsed / duration;
            float currentIntensity = Mathf.Lerp(intensity, 0f, progress);

            foreach (var target in targets)
            {
                if (target == null) continue;

                float x = Random.Range(-1f, 1f) * currentIntensity;
                float y = Random.Range(-1f, 1f) * currentIntensity;

                target.anchoredPosition =
                    originalPositions[target] + new Vector2(x, y);
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        // 원래 위치 복원
        foreach (var target in targets)
        {
            if (target != null)
                target.anchoredPosition = originalPositions[target];
        }
    }
}