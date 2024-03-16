using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestView : MonoBehaviour
{
    void Start()
    {
        TestFactory factory = new TestFactory();
        GameObject cube = factory.CreateCube(transform);
        factory.DeleteCube(cube);
    }

    void Update()
    {
        
    }
}
