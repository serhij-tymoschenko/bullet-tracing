using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 1F;
    private Rigidbody player;

    private void Awake()
    {
        player = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        var movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        player.MovePosition(player.position + movement * movementSpeed * Time.deltaTime);
    }
}