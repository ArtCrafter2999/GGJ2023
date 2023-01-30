using UnityEngine;

public abstract class UpgradeBase<T> : MonoBehaviour
{
    public Player playerRoot;

    public void ApplyUpgrade()
    {
        var upgradable = playerRoot.GetComponentInChildren<T>();
        OnApplyUpgrade(upgradable);
    }

    protected abstract void OnApplyUpgrade(T upgradable);
}
