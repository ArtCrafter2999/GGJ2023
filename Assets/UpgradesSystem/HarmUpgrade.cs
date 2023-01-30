public class HarmUpgrade : UpgradeBase<IHarm>
{
    public float harmMultiplier;

    protected override void OnApplyUpgrade(IHarm upgradable)
    {
        upgradable.LostHealth /= harmMultiplier;
    }
}
