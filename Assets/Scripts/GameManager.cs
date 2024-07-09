using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public GameObject deerPrefab;
    public int deadDeer;
    public int money;
    public GameObject shopPanel;
    float spawnZ = 20;
    Vector2 spawnBounds = new(-28, 28);
    float deerSpawnTimer;
    float spawnDeerEvery = 2;
    bool shopOpen;

    void Update() {
        if (deerSpawnTimer > spawnDeerEvery) {
            // spawn a deer in a random pos at the back line
            GameObject newDeer = Instantiate(deerPrefab);
            newDeer.transform.position = new Vector3(Random.Range(spawnBounds.x, spawnBounds.y), 2.5f, spawnZ);

            // reset timer so another deer can spawn
            deerSpawnTimer = 0;
        } else {
            deerSpawnTimer += Time.deltaTime;
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
}
