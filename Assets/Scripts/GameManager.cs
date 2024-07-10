using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {
    // publics
    public GameObject deerPrefab;
    public int deadDeer;
    public int money;
    public int hunterBase;
    public int deerBase;
    public GameObject shopPanel;
    public Upgrade[] upgrades;

    // deer parameters
    float spawnZ = 20;
    Vector2 spawnBounds = new(-28, 28);
    float deerSpawnTimer;
    float spawnDeerEvery = 2;
    bool shopOpen;

    // hunter tings
    float hunterInterval = 5;
    float hunterShootTimer;

    void Update() {
        // deer spawning
        if (deerSpawnTimer > spawnDeerEvery) {
            // spawn a deer in a random pos at the back line
            GameObject newDeer = Instantiate(deerPrefab);
            newDeer.transform.position = new Vector3(Random.Range(spawnBounds.x, spawnBounds.y), 2.5f, spawnZ);

            // reset timer so another deer can spawn
            deerSpawnTimer = 0;
        } else {
            deerSpawnTimer += Time.deltaTime;
        }

        // hunter shooting
        if (hunterShootTimer > hunterInterval && upgrades[0].ownedCount > 0) {
            money += (int)Mathf.Round(hunterBase + (upgrades[0].ownedCount/2) * (upgrades[2].ownedCount/2));
            hunterShootTimer = 0;
        } else {
            hunterShootTimer += Time.deltaTime;
        }
    }

    public void MouseClick() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100)) {
            GameObject goHit = hit.collider.gameObject;
            if (goHit.tag == "deer") {
                // deer was clicked
                deadDeer++;
                Destroy(goHit);
            }
        }
    }

    public static GameManager Singleton() {
        return FindObjectOfType<GameManager>();
    }

    public void ToggleShop() {
        if (shopOpen) {
            shopPanel.SetActive(false);
            shopOpen = false;
        } else {
            shopPanel.SetActive(true);
            shopOpen = true;
        }
    }

    public void SellDeer() {
        money += deadDeer * (deerBase + upgrades[3].ownedCount);
        deadDeer = 0;
    }
    
    public void BuyUpgrade(int index) {
        if (money >= upgrades[index].cost) {
            money -= upgrades[index].cost;
            upgrades[index].ownedCount++;
            if (upgrades[index].increaseType == PriceIncrease.add)
                upgrades[index].cost += (int)Mathf.Round(upgrades[index].priceMod);
            if (upgrades[index].increaseType == PriceIncrease.multiply)
                upgrades[index].cost = (int)Mathf.Round(upgrades[index].cost * upgrades[index].priceMod);
        }
    }
}

[System.Serializable] public class Upgrade {
    public string name;
    public int cost;
    public int ownedCount;
    public float priceMod;
    public PriceIncrease increaseType;
    public TMP_Text priceText;
    public TMP_Text ownedCountText;
}

public enum PriceIncrease { add, multiply };