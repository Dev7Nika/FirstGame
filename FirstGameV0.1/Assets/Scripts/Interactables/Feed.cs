using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feed : Interactable
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject ball;
    public float growthrate;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void Interact()
    {
        ball.transform.localScale *= growthrate;
        Debug.Log("Interacted with " + gameObject.name);
    }
}
