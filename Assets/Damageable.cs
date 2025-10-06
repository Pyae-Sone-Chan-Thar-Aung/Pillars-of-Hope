using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    Animator animator;

    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int _health; // 👈 now visible
    [SerializeField] private bool _isAlive = true; // 👈 now visible

    [SerializeField] private float invincibilityTimer = 0.25f;
    [SerializeField] private bool isInvincible = false; // 👈 now visible
    private float timeSinceHit = 0f;

    public int MaxHealth
    {
        get { return _maxHealth; }
        set { _maxHealth = value; }
    }

    public int Health
    {
        get => _health;
        private set
        {
            _health = Mathf.Clamp(value, 0, MaxHealth);

            if (_health <= 0 && IsAlive)
            {
                IsAlive = false; // trigger death
            }
        }
    }

    public bool IsAlive
    {
        get => _isAlive;
        private set
        {
            _isAlive = value;
            animator.SetBool("isAlive", value); // fixed here
            //Debug.Log("IsAlive set to: " + value);
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        Health = MaxHealth;
        IsAlive = true;
    }

    private void Update()
    {
        // Handle invincibility timer
        if (isInvincible)
        {
            timeSinceHit += Time.deltaTime;

            if (timeSinceHit >= invincibilityTimer)
            {
                isInvincible = false;
                timeSinceHit = 0f;
            }
        }
    }

    public void Hit(int damage)
    {
        if (IsAlive && !isInvincible)
        {
            Health -= damage;
            isInvincible = true;
            timeSinceHit = 0f;
            Debug.Log(gameObject.name + " took " + damage + " damage. Health = " + Health);
        }
    }
}
