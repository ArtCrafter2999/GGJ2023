using System.Linq;
using UnityEngine;

public class UpgradesManager : MonoBehaviour
{
    public UpgradeBase[] upgrades;

    public void Upgrade<T>() where T : UpgradeBase
    {
        upgrades.First(u => u is T).ApplyUpgrade();
    }
}
