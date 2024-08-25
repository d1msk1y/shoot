using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraMovement : MonoBehaviour
{
    public Rigidbody2D player;

    public float speed;
    public Vector2 velSpeed, offset;

    private float defaultZoom = 6f;
    [SerializeField]private float zoomSpeed = 0.2f;
    private float velZoom;
    public float zoomModifier;

    private float height = 0f;

    private float yMin = 0f;

    [HideInInspector] public float velocity;
    public static CameraMovement instance;


    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.SmoothDamp(transform.position, player.position, ref velSpeed, speed) + offset;
        transform.position = new Vector3(transform.position.x, transform.position.y, -20);
        if (PlayerController.instance.isActiveAndEnabled)
        {
            velocity = player.velocity.x * player.velocity.y;
            if (velocity <= 0)
                velocity = Mathf.Abs(velocity);

            float desiredZoom = defaultZoom + velocity * zoomModifier;

            if (desiredZoom > 15)
                desiredZoom = 9;


            Camera.main.orthographicSize =
                Mathf.SmoothDamp(Camera.main.orthographicSize, desiredZoom, ref velZoom, zoomSpeed);

            if(transform.position.y < yMin)
            {
                transform.position = new Vector3(transform.position.x, yMin, transform.position.z);
            }
        }
    }
}
