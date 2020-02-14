using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boneFire : MonoBehaviour
{
    public ParticleSystem fire;
    public Light light;
    public float woodRemaining = 0;
    public float heat = 0;
    public float intensity = 0;
    public float burningSpeed = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void addWood(float amount) 
    {
        intensity = 5;
        woodRemaining += amount;
        fire.startLifetime = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        light.intensity = intensity;
        if (woodRemaining <= 150 && intensity > 0)
        {
            fire.startLifetime = 1;
            intensity -= 0.04f * Time.deltaTime;
        }
        else if (intensity < 3f)
        {
            fire.startLifetime = 2;
        }
        if (intensity > 2.5f)
            intensity -= 0.6f * Time.deltaTime;
        
        heat = woodRemaining / 80;
        if (heat > 50)
            heat = 50;
        if (heat < 3)
            heat = 5;
        if (woodRemaining > 0)
            woodRemaining -= burningSpeed * Time.deltaTime;
        else {
            woodRemaining = 0;
            fire.startLifetime = 0;
            intensity = 0;
        }
    }
}
