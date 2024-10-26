using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {
    public int count = 0;
    public float curScale = 1;
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        Time.timeScale = 0;
        curScale = Time.timeScale;
        count++;
    }
}
