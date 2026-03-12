using UnityEngine;

public class TimeSystem : MonoBehaviour
{
    [SerializeField] MonoBehaviour playerReference;

    [Header("TimeSystem Setting")]
    [Range(0f, 1f)] [SerializeField] float minTimeScale = 0.1f;
    [Range(0f, 1f)] [SerializeField] float maxTimeScale = 1f;
    [Range(0f, 10f)] [SerializeField] float timeChangeSpeed = 6f;

    [Header("Time Influence From Player Activity")]
    [Range(0f, 1f)] [SerializeField] float moveTimeForce = 1f;
    [Range(0f, 1f)] [SerializeField] float lookTimeForce = 0.2f;
    [Range(0f, 1f)] [SerializeField] float actionTimeForce = 0.5f;

    IPlayerActivity activity;
    float baseFixedDeltaTime;

    void Awake()
    {
        activity = playerReference.GetComponent<IPlayerActivity>();
        baseFixedDeltaTime = Time.fixedDeltaTime;
    }

    void Update()
    {
        if (activity == null)
        {
            Debug.Log("Player dont Implement with IPlayerActivity!!!");
            return;
        }

        float move = activity.MoveInput.magnitude * moveTimeForce;
        float look = activity.LookInput.magnitude * lookTimeForce;
        float action = activity.IsAction ? actionTimeForce : 0f;

        float activityLevel = Mathf.Clamp01(move + look + action);

        float targetTime = Mathf.Lerp(minTimeScale, maxTimeScale, activityLevel);

        Time.timeScale = Mathf.Lerp(
            Time.timeScale,
            targetTime,
            timeChangeSpeed * Time.unscaledDeltaTime
        );

        Time.fixedDeltaTime = baseFixedDeltaTime * Time.timeScale;
    }
}