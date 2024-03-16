using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ImprovedView : MonoBehaviour
{
    /*[SerializeField] private Button defaultCube;
    [SerializeField] private Button megaCube;
    [SerializeField] private Button gigaCube;
    [SerializeField] private Button blackCube;
    [SerializeField] private Button myCube;*/
    [Inject] ImprovedFactory improvedFactory;

    void Start()
    {
        /*improvedFactory = FindAnyObjectByType<ImprovedFactory>();
        defaultCube.onClick.AddListener(() => improvedFactory.CreateCube(CubeType.Default));
        megaCube.onClick.AddListener(() => improvedFactory.CreateCube(CubeType.Mega));
        gigaCube.onClick.AddListener(() => improvedFactory.CreateCube(CubeType.Giga));
        blackCube.onClick.AddListener(() => improvedFactory.CreateCube(CubeType.Black));
        myCube.onClick.AddListener(() => improvedFactory.CreateCube(CubeType.MyCube));*/
        improvedFactory.CreateCube(CubeType.MyCube);
    }
}
