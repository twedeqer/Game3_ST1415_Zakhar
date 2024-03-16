using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LookToCamera : MonoBehaviour
{
    private new Camera camera;
    [SerializeField] private Slider hpSlider;

    void Awake()
    {
        camera = Camera.main;
    }

    void LateUpdate()
    {
        transform.LookAt(camera.transform.position);
        hpSlider.direction = Slider.Direction.RightToLeft;
    }
}
