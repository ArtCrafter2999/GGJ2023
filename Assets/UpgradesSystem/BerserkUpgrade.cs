public class BerserkUpgrade : UpgradeBase<IBerserk>
{
    protected override void OnApplyUpgrade(IBerserk upgradable)
    {
        upgradable.IsEnabled = true;
    }
}
