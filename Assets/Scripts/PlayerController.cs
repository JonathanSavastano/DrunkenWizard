using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public int maxHealth = 6;
    private int currentHealth;
    public Transform projectileSpawnPoint;
    public GameObject projectilePrefab;
    public float moveSpeed = 5.0f;
    public Animator animator;
    private bool isMoving = false;

    public Text healthText;
    public Slider healthSlider;

    private bool canAttack = true;
    public float attackCooldown = 1f;
    private float lastAttackTime;

    public AudioSource shootAudioSource;
    public AudioSource damageAudioSource;

    private bool hasReachedCrystalBall =  false;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = 4;
    }
    // Update is called once per frame
    void Update()
    {

        // Get input for movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate separate horizontal and vertical movement values
        float moveX = 0.0f;
        float moveY = 0.0f;

        if (Mathf.Abs(horizontalInput) > Mathf.Abs(verticalInput))
        {
            moveX = Mathf.Sign(horizontalInput);
        }
        else
        {
            moveY = Mathf.Sign(verticalInput);
        }

        // Create the movement vector
        Vector3 movement = new Vector3(moveX, moveY, 0.0f) * moveSpeed * Time.deltaTime;

        // Update animator parameters
        animator.SetFloat("Horizontal", moveX);
        animator.SetFloat("Vertical", moveY);

        // Only move when there's actual input
        if (Mathf.Abs(horizontalInput) > 0.1f || Mathf.Abs(verticalInput) > 0.1f)
        {
            isMoving = true;
            // Update the player's position
            transform.Translate(movement);
        }
        else
        {
            isMoving = false;
        }

        // handle attack input
        if (Input.GetMouseButtonDown(0) && isMoving && canAttack)
        {
            // determine attack direction based on movement
            float attackX = Mathf.Sign(horizontalInput);
            float attackY = Mathf.Sign(verticalInput);

            // set animator parameters for attack direction
            animator.SetFloat("AttackX", attackX);
            animator.SetFloat("AttackY", attackY);

            // Trigger attack animation
            animator.SetTrigger("Attack");

            // spawn projectile in attack direction
            Vector3 attackDirection = new Vector3(attackX, attackY, 0.0f);
            SpawnProjectile(attackDirection);

            // set time of last attack
            lastAttackTime = Time.time;

            // start cooldown coroutine
            StartCoroutine(AttackCooldown());
        }
    }

    private IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private void SpawnProjectile(Vector3 direction)
    {
        GameObject newProjectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
        Projectile projectileScript = newProjectile.GetComponent<Projectile>();

        if (projectileScript != null)
        {
            // Set the projectile's direction based on player's movement
            projectileScript.transform.right = direction.normalized;
            projectileScript.speed = 10.0f;

            // play sound  effect
            shootAudioSource.Play();
        }
    }

    public void TakeDamage()
    {
        currentHealth--;

        damageAudioSource.Play();

        healthText.text = "Health: " + currentHealth + "/" + maxHealth;
        healthSlider.value = (float)currentHealth / maxHealth;
    }

    public bool HasReachedCrystalBall()
    {
        return hasReachedCrystalBall;
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("CrystalBall"))
        {
            hasReachedCrystalBall = true;
        }
    }

    public void RestoreHealth(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        healthText.text = "Health: " + currentHealth + "/" + maxHealth;
        healthSlider.value = (float)currentHealth / maxHealth;
    }
}
