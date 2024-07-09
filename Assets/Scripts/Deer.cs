using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deer : MonoBehaviour {
    bool touchingTree;
    float speed = 900;
    public int strafeSide; // left or right, but it's a bool
    Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();
        strafeSide = Random.Range(0, 2);
    }

    void Update() {
        // move deer forward (set velocity)
        rb.velocity = transform.up * Time.deltaTime * speed;

        // move deer to one side if touching a tree
        if (touchingTree && strafeSide == 0)
            rb.velocity = -transform.right * Time.deltaTime * speed;

        if (touchingTree && strafeSide == 1)
            rb.velocity = transform.right * Time.deltaTime * speed;

        // if past certain z coord, remove deer
        if (transform.position.z < -30)
            Destroy(gameObject);

    }

    void OnCollisionEnter(Collision col) {
        if (col.gameObject.tag == "tree") {
            touchingTree = true;
            strafeSide = Random.Range(0, 2);
        }
        if (col.gameObject.tag == "deer") {
            touchingTree = true;
            speed *= 2;
        }
    }

    void OnCollisionExit(Collision col) {
        touchingTree = false;
    }


}
