using UnityEngine;

public class Mover : MonoBehaviour
{
    [Header("Settings")]
    public float maxMoveSpeed;
    public float minMoveSpeed;
    private float currentSpeed;
    void Start()
    {
        currentSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * GameManager.Instance.CalculateGameSpeed() * currentSpeed * Time.deltaTime;
    }
}
