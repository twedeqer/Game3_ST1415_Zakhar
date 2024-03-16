using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerSettings : MonoBehaviourPunCallbacks
{
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    [SerializeField] private Slider healthBar;
    private PhotonView photonView;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }

    void Update()
    {
        health = maxHealth;
        healthBar.value = health;
    }

    public void TakeDamage(int value)
    {
        photonView.RPC("UpdateHealth", RpcTarget.All, value);
    }

    [PunRPC]
    public void UpdateHealth(int value)
    {
        health -= value;

        if (health <= 0)
        {
            health = maxHealth;
            transform.GetComponent<PlayerControler>().Respawn();
        }
        healthBar.value = health;
    }
}
