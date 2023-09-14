using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float xOffSetFromPlayer;
    [SerializeField] private float yOffSetFromPlayer;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position = new Vector3(player.position.x + xOffSetFromPlayer, player.position.y + yOffSetFromPlayer, transform.position.z);
    }
}
