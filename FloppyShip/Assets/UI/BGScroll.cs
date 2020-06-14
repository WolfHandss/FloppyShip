using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroll : MonoBehaviour
{

    public float speed = 0.5f;

    Renderer R;

    // Start is called before the first frame update
    void Start()
    {
        R = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 offset = new Vector2(Time.time * speed, 0);

        R.material.mainTextureOffset = offset;
    }
}
