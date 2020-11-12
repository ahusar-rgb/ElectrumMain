using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float zoomfactor = 5;
    private float cameraZoom;
    private Transform player;
    void Start()
    {
        cameraZoom = Camera.main.orthographicSize;
        player = GameObject.FindWithTag("Player").transform;
    }
     
    void Update()
    {
        cameraZoom = zoomfactor;
        cameraZoom = Mathf.Clamp(cameraZoom, 4.5f, 7f);
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, cameraZoom, Time.deltaTime * 10f);
        Camera.main.transform.position = new Vector3(player.position.x, player.position.y, Camera.main.transform.position.z);
    }
}
