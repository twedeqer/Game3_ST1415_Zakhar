using UnityEngine;
using Photon.Pun;

//[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private BulletInfo bulletInfo;
    private Rigidbody rb;
    private PhotonView pv;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        pv = GetComponent<PhotonView>();
        bulletInfo.render = gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!pv.IsMine) return;

        if (other.TryGetComponent(out PlayerSettings ps))
        {
            ps.TakeDamage(bulletInfo.damage);
            PhotonNetwork.Destroy(gameObject);
        }
    }

    public void StartMove(Vector3 dir)
    {
        rb.velocity = dir * bulletInfo.speed;
    }
}
