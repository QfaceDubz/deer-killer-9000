using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {
    public Text deadDeer;
    public Text money;
    GameManager gm;

    void Start() {
        gm = GameManager.Singleton();
    }

    void Update() {
        deadDeer.text = "dead deer: " + gm.deadDeer;
        money.text = "money: $" + gm.money;
        foreach (Upgrade upgrade in gm.upgrades) {
            upgrade.priceText.text = "$" + upgrade.cost.ToString();
            upgrade.ownedCountText.text = upgrade.ownedCount.ToString();
        }
    }
}
