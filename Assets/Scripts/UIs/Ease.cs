using UnityEngine;

public static class Ease
{
    public static float Evaluate(EaseType type, float t)
    {
        t = Mathf.Clamp01(t);

        switch (type)
        {
            case EaseType.Linear: return t;

            #region Quad
            case EaseType.InQuad: return t * t;
            case EaseType.OutQuad: return 1 - (1 - t) * (1 - t);
            case EaseType.InOutQuad:
                return t < 0.5f
                    ? 2 * t * t
                    : 1 - Mathf.Pow(-2 * t + 2, 2) / 2;
            #endregion

            #region Cubic
            case EaseType.InCubic: return t * t * t;
            case EaseType.OutCubic: return 1 - Mathf.Pow(1 - t, 3);
            case EaseType.InOutCubic:
                return t < 0.5f
                    ? 4 * t * t * t
                    : 1 - Mathf.Pow(-2 * t + 2, 3) / 2;
            #endregion

            #region Quart
            case EaseType.InQuart: return t * t * t * t;
            case EaseType.OutQuart: return 1 - Mathf.Pow(1 - t, 4);
            case EaseType.InOutQuart:
                return t < 0.5f
                    ? 8 * Mathf.Pow(t, 4)
                    : 1 - Mathf.Pow(-2 * t + 2, 4) / 2;
            #endregion

            #region Quint
            case EaseType.InQuint: return t * t * t * t * t;
            case EaseType.OutQuint: return 1 - Mathf.Pow(1 - t, 5);
            case EaseType.InOutQuint:
                return t < 0.5f
                    ? 16 * Mathf.Pow(t, 5)
                    : 1 - Mathf.Pow(-2 * t + 2, 5) / 2;
            #endregion

            #region Sine
            case EaseType.InSine:
                return 1 - Mathf.Cos((t * Mathf.PI) / 2);
            case EaseType.OutSine:
                return Mathf.Sin((t * Mathf.PI) / 2);
            case EaseType.InOutSine:
                return -(Mathf.Cos(Mathf.PI * t) - 1) / 2;
            #endregion

            #region Expo
            case EaseType.InExpo:
                return t == 0 ? 0 : Mathf.Pow(2, 10 * t - 10);
            case EaseType.OutExpo:
                return t == 1 ? 1 : 1 - Mathf.Pow(2, -10 * t);
            case EaseType.InOutExpo:
                if (t == 0) return 0;
                if (t == 1) return 1;
                return t < 0.5f
                    ? Mathf.Pow(2, 20 * t - 10) / 2
                    : (2 - Mathf.Pow(2, -20 * t + 10)) / 2;
            #endregion

            #region Circ
            case EaseType.InCirc:
                return 1 - Mathf.Sqrt(1 - t * t);
            case EaseType.OutCirc:
                return Mathf.Sqrt(1 - Mathf.Pow(t - 1, 2));
            case EaseType.InOutCirc:
                return t < 0.5f
                    ? (1 - Mathf.Sqrt(1 - Mathf.Pow(2 * t, 2))) / 2
                    : (Mathf.Sqrt(1 - Mathf.Pow(-2 * t + 2, 2)) + 1) / 2;
            #endregion

            #region Back
            case EaseType.InBack:
                {
                    const float c1 = 1.70158f;
                    const float c3 = c1 + 1;
                    return c3 * t * t * t - c1 * t * t;
                }
            case EaseType.OutBack:
                {
                    const float c1 = 1.70158f;
                    const float c3 = c1 + 1;
                    return 1 + c3 * Mathf.Pow(t - 1, 3) + c1 * Mathf.Pow(t - 1, 2);
                }
            case EaseType.InOutBack:
                {
                    const float c1 = 1.70158f;
                    const float c2 = c1 * 1.525f;
                    return t < 0.5f
                        ? (Mathf.Pow(2 * t, 2) * ((c2 + 1) * 2 * t - c2)) / 2
                        : (Mathf.Pow(2 * t - 2, 2) * ((c2 + 1) * (2 * t - 2) + c2) + 2) / 2;
                }
            #endregion

            #region Elastic
            case EaseType.InElastic:
                if (t == 0) return 0;
                if (t == 1) return 1;
                return -Mathf.Pow(2, 10 * t - 10) *
                       Mathf.Sin((t * 10 - 10.75f) * (2 * Mathf.PI / 3));

            case EaseType.OutElastic:
                if (t == 0) return 0;
                if (t == 1) return 1;
                return Mathf.Pow(2, -10 * t) *
                       Mathf.Sin((t * 10 - 0.75f) * (2 * Mathf.PI / 3)) + 1;

            case EaseType.InOutElastic:
                if (t == 0) return 0;
                if (t == 1) return 1;
                return t < 0.5f
                    ? -(Mathf.Pow(2, 20 * t - 10) *
                       Mathf.Sin((20 * t - 11.125f) * (2 * Mathf.PI / 4.5f))) / 2
                    : (Mathf.Pow(2, -20 * t + 10) *
                       Mathf.Sin((20 * t - 11.125f) * (2 * Mathf.PI / 4.5f))) / 2 + 1;
            #endregion

            #region Bounce
            case EaseType.OutBounce:
                return BounceOut(t);
            case EaseType.InBounce:
                return 1 - BounceOut(1 - t);
            case EaseType.InOutBounce:
                return t < 0.5f
                    ? (1 - BounceOut(1 - 2 * t)) / 2
                    : (1 + BounceOut(2 * t - 1)) / 2;
            #endregion
        }

        return t;
    }

    private static float BounceOut(float t)
    {
        const float n1 = 7.5625f;
        const float d1 = 2.75f;

        if (t < 1 / d1)
            return n1 * t * t;
        else if (t < 2 / d1)
            return n1 * (t -= 1.5f / d1) * t + 0.75f;
        else if (t < 2.5f / d1)
            return n1 * (t -= 2.25f / d1) * t + 0.9375f;
        else
            return n1 * (t -= 2.625f / d1) * t + 0.984375f;
    }
}