using UnityEngine;

public class Berserker : MonoBehaviour, IBerserk
{
    public Growther growther;
    public float intervalToEffect;
    public float movespeedMultiplier;
    public float effectDuration;

    IMovement movement;

    bool didGrowthOnce;
    float lastGrowthTime;
    bool hasEffect;

    public bool IsEnabled { get; set; }

    private void Start()
    {
        movement = GetComponentInParent<IMovement>();
        growther.DidGrowth += OnDidGrowth;
    }

    private void OnDidGrowth()
    {
        if (!IsEnabled) return;

        lastGrowthTime = Time.time;
        if (didGrowthOnce)
        {
            if (Time.time - lastGrowthTime <= intervalToEffect)
            {
                if (!hasEffect)
                {
                    ApplyEffect();
                    hasEffect = true;
                }
            }
        }
        else
        {
            didGrowthOnce = true;
        }
    }

    void Update()
    {
        if (!IsEnabled) return;

        if (hasEffect)
        {
            if (Time.time - lastGrowthTime > effectDuration)
            {
                RevertEffect();
                hasEffect = false;
            }
        }
        else if (didGrowthOnce)
        {
            if (Time.time - lastGrowthTime > intervalToEffect)
            {
                didGrowthOnce = false;
            }
        }

    }

    private void ApplyEffect()
    {
        movement.MoveSpeed *= movespeedMultiplier;
    }

    private void RevertEffect()
    {
        movement.MoveSpeed /= movespeedMultiplier;
    }
}
