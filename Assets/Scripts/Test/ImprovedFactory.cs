using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImprovedFactory
{
    public void CreateCube(CubeType cubeType)
    {
        switch (cubeType)
        {
            case CubeType.Default:
                MonoBehaviour.print("Create Default cube");
                break;
            case CubeType.Mega:
                MonoBehaviour.print("Create Mega cube");
                break;
            case CubeType.Giga:
                MonoBehaviour.print("Create Giga cube");
                break;
            case CubeType.Black:
                MonoBehaviour.print("Create Black cube");
                break;
            case CubeType.MyCube:
                MonoBehaviour.print("Create My cube");
                break;
        }
    }
}
public enum CubeType
{
    Default,
    Mega,
    Giga,
    Black,
    MyCube
}
