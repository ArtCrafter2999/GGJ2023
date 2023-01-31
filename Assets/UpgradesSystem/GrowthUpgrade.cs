public class GrowthUpgrade : UpgradeBase<IGrowth>
{
    public float growthTimeMultiplier;

    protected override void OnApplyUpgrade(IGrowth upgradable)
    {
        upgradable.GrowthTime /= growthTimeMultiplier;
    }
}
