using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.InputSystem;
using System.IO;
using DG.Tweening;

public class Aim : MonoBehaviour
{
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private List<GameObject> targets;
    [SerializeField] private GameObject targetCylinder;
    [SerializeField] private float range;
    private PhotonView photonView;
    private PlayerInput inputs;
    private CharacterController characterController;
    private GameObject targetObj;
    private bool canSearch = true;
    private int targetCount;

    private void Awake()
    {
        inputs = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        photonView = GetComponent<PhotonView>();
    }

    private void Start()
    {
        if (!photonView.IsMine) return;
        targetCylinder.SetActive(false);
        inputs.CharacterControls.ChangeTarget.started += SelectNewTarget;
        inputs.CharacterControls.Attack.started += OnFire;
    }

    private void OnEnable()
    {
        inputs.CharacterControls.Enable();
    }

    private void OnDisable()
    {
        inputs.CharacterControls.Disable();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    private void FixedUpdate()
    {
        if (!photonView.IsMine) return;
        SelectTarget();
    }

    public void SetTargetStatus(bool isTarget)
    {
        targetCylinder.SetActive(isTarget);
    }

    private void SelectTarget()
    {
        if (characterController.velocity == Vector3.zero)
        {
            if (canSearch)
                InvokeRepeating("Calculate", 0, 0.5f);
        }
        else
        {
            try
            {
                targetObj?.GetComponent<Aim>().SetTargetStatus(false);
            }
            catch (MissingReferenceException)
            {

            }
            canSearch = true;
            CancelInvoke();
        }
    }

    private void Calculate()
    {
        canSearch = false;
        targets.Clear();
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, range, transform.position, range);
        foreach (RaycastHit hit in hits)
        {
            GameObject tempObj = hit.collider.gameObject;
            if (tempObj.GetComponent<CharacterController>() &&
                !tempObj.GetComponentInParent<PhotonView>().IsMine)
            {
                targets.Add(tempObj);
            }
            else continue;
        }
        SelectNewTarget();
    }
    private void SelectNewTarget()
    {
        foreach (GameObject obj in targets)
        {
            obj.GetComponent<Aim>().SetTargetStatus(false);
        }
        if (targetCount >= targets.Count)
        {
            targetCount = 0;
        }

        if (targets.Count == 0) return;

        targetObj = targets[targetCount];
        targetObj.GetComponent<Aim>().SetTargetStatus(true);
    }

    private void SelectNewTarget(InputAction.CallbackContext context)
    {
        targetCount++;
        SelectNewTarget();
    }

    private void OnFire(InputAction.CallbackContext context)
    {
        if (targetObj != null)
        {
            Vector3 dir = (targetObj.transform.position - transform.position).normalized;

            GameObject temp = PhotonNetwork.Instantiate(Path.Combine("Bullet"),
            spawnPosition.position, Quaternion.identity);

            temp.GetComponent<Bullet>().StartMove(dir);
            Physics.IgnoreCollision(temp.GetComponent<Collider>(), transform.GetComponent<Collider>());
        }
    }
}
