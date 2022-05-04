using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    // Start is called before the first frame update
    public float ParallaxFactor = 0.1f;
    public float FramesParllaxFactor = 0.3f;
    public float SmoothX = 4;
    public Transform[] backgrounds;
    private Transform cam;
    private Vector3 camPrePos;
    private void Awake()
    {
        cam = Camera.main.transform;
        camPrePos = cam.position;
    }
    void Start()
    {
        
    }
    void bkParallax()
    {
        float fparallax = (camPrePos.x - cam.position.x) * ParallaxFactor;
        for(int i=0; i<backgrounds.Length;i++)
        {
            float bkNewX = backgrounds[i].position.x + fparallax * (1 + i * FramesParllaxFactor);
            Vector3 bkNewPos = new Vector3(bkNewX, backgrounds[i].position.y, backgrounds[i].position.z);
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, bkNewPos, Time.deltaTime * SmoothX);
            camPrePos = cam.position;
        }
    }
    // Update is called once per frame
    void Update()
    {
        bkParallax();
    }
}
