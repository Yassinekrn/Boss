using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChase : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public float health = 10f; // Boss health value
    public float enragedSpeedMultiplier = 1.5f; // Speed multiplier when boss is enraged
    public float enragedThrowRateMultiplier = 2f; // Throw rate multiplier when boss is enraged
    public float throwForce; // Adjust this value to control the force with which the object is thrown
    public float throwInterval; // Adjust this value to control the time interval between throws
    public GameObject objectPrefab; // Reference to the prefab of the object to be thrown
    private float distance;
    private float throwTimer;
    private bool SecondFaze;

    void Start()
    {
        throwTimer = throwInterval;
        SecondFaze= false;

    }

    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Adjust boss speed based on enraged state

        
        if (health <= 5f && !SecondFaze )
        {
            speed *= enragedSpeedMultiplier;
            throwInterval /= enragedThrowRateMultiplier;
            SecondFaze= true;
        }

        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);

        throwTimer -= Time.deltaTime;
        if (throwTimer <= 0)
        {
            ThrowObject();
            throwTimer = throwInterval;
        }

        // Check if the boss's health is depleted
        if (health <= 0)
        {
            DefeatBoss();
        }
    }

    // Method to handle boss defeat
    void DefeatBoss()
    {
        // Add code to perform actions when the boss is defeated
        // For example, play a defeat animation, give rewards, etc.
        Destroy(gameObject);
    }

    // Method to handle boss taking damage
    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    void ThrowObject()
    {
        // Instantiate the object prefab
        GameObject thrownObject = Instantiate(objectPrefab, transform.position, Quaternion.identity);

        // Calculate the direction towards the player
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();

        // Apply force to the thrown object in the calculated direction
        Rigidbody2D objectRigidbody = thrownObject.GetComponent<Rigidbody2D>();
        objectRigidbody.AddForce(direction * throwForce, ForceMode2D.Impulse);
    }
}
