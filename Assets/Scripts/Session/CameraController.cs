using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject mainCharacter;
    [SerializeField] private float returnSpeed;
    [SerializeField] private float height;
    [SerializeField] private float rearDistance;
    private Vector3 cameraOffset;
    private Vector3 currentVector;

    void Start()
    {
        transform.position = mainCharacter.transform.position + new Vector3(0, height, -rearDistance);
        transform.rotation = Quaternion.LookRotation(mainCharacter.transform.position - transform.position);
    }

    void Update()
    {
        CameraMove();
    }

    public void SetOffset(Vector3 offset)
    {
        if (offset.z < 0)
            cameraOffset = offset * 10;
        else if (offset.z > 0)
            cameraOffset = offset * 3;
        else
            cameraOffset = offset * 8;
    }

    private void CameraMove()
    {
        currentVector = mainCharacter.transform.position + new Vector3(cameraOffset.x, height, cameraOffset.z - rearDistance);
        transform.position = Vector3.Lerp(transform.position, currentVector, returnSpeed * Time.deltaTime);
    }
}
