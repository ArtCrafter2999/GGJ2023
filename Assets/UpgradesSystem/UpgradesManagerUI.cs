using UnityEngine;

public class UpgradesManagerUI : MonoBehaviour
{
    UpgradesManager manager;

    private void Awake()
    {
        manager = FindObjectOfType<UpgradesManager>(); // Replace to singleton
    }

    public void OnMovementUpgrage() => manager.Upgrade<MovementUpgrade>();
    public void OnHarmUpgrage() => manager.Upgrade<HarmUpgrade>();
    public void OnGrowthUpgrage() => manager.Upgrade<GrowthUpgrade>();
    public void OnBerserkUpgrage() => manager.Upgrade<BerserkUpgrade>();

}
