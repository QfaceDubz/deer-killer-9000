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
        money.text = "money: " + gm.money;
    }
}
