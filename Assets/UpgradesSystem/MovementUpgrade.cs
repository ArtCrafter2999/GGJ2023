public class MovementUpgrade : UpgradeBase<IMovement>
{
    public float movespeedMultiplier;

    protected override void OnApplyUpgrade(IMovement upgradable)
    {
        upgradable.MoveSpeed *= movespeedMultiplier;
    }
}
