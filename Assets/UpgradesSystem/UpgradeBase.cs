using UnityEngine;

public abstract class UpgradeBase : MonoBehaviour
{
    public abstract void ApplyUpgrade();
}


public abstract class UpgradeBase<T> : UpgradeBase
{
    GameObject playerRoot;

    private void Awake()
    {
        playerRoot = GameObject.Find("Player"); // replace to ref
    }

    public override void ApplyUpgrade()
    {
        var upgradable = playerRoot.GetComponentInChildren<T>();
        OnApplyUpgrade(upgradable);
    }

    protected abstract void OnApplyUpgrade(T upgradable);
}
