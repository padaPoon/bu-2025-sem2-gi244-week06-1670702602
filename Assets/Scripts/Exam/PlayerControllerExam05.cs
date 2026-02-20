using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerExam05 : MonoBehaviour
{
    public float speed;
    public float xRange = 10;
    public GameObject projectilePrefab;


    // Exam 05 ...
    public int maxBulletCount = 10;
    public float bulletRegenerateCooldown = 1f;
    // ...

    private float horizontalInput;
    private InputAction moveAction;
    private InputAction shootAction;

    private void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        shootAction = InputSystem.actions.FindAction("Shoot");
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = moveAction.ReadValue<Vector2>().x;
        transform.Translate(horizontalInput * speed * Time.deltaTime * Vector3.right);

        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }

        if (shootAction.triggered)
        {
            Instantiate(projectilePrefab, transform.position, transform.rotation);
        }

        //check if shoot action is triggered and there are bullets available
        if (shootAction.triggered && maxBulletCount > 0)
        {
            Instantiate(projectilePrefab, transform.position, transform.rotation);
            maxBulletCount--;
        }

        // Regenerate bullets over time if the count is zero or less
        if (maxBulletCount <= 0)
        {
            bulletRegenerateCooldown -= Time.deltaTime;
            if (bulletRegenerateCooldown <= 0f)
            {
                maxBulletCount+= 10;
                bulletRegenerateCooldown = 5f; // Reset cooldown
            }
        }
    }
}
