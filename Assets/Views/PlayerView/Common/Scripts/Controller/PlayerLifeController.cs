using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;


public class PlayerLifeController : MonoBehaviour
{
    [SerializeField] private float size;
    [SerializeField] private float hitInvincibilityDurationSeconds;
    [SerializeField] private float invincibananeDurationSeconds;
    public float spriteBlinkingTimer = 0.0f;
    public float spriteBlinkingMiniDuration = 0.3f;
    private bool isInvincible = false;
    private int lives = 1;

    private Rigidbody2D rb;
    private Animator anim;
    private Transform trans;
    private PlayerMovementController pm;
    private SpriteRenderer sprite;
    private PlayerModel model;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        trans = GetComponent<Transform>();
        pm = GetComponent<PlayerMovementController>();
        trans.localScale = new Vector3(size, size, size);
        sprite = GetComponent<SpriteRenderer>();
        model = pm.model;
    }

    private void Update()
    {
        if (isInvincible)
        {
            Blink();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Melon"))
        {
            lives += 1;
            Destroy(collision.gameObject);
            trans.localScale = new Vector3(size, (float)(size * 1.4), size);
        }
        if (collision.gameObject.CompareTag("Invincibanane"))
        {
            Destroy(collision.gameObject);
            StartCoroutine(BecomeTemporarilyInvincible(invincibananeDurationSeconds));
        }

        if (collision.gameObject.CompareTag("Trap"))
        {
            LoseHealth();
        }

        if (collision.gameObject.CompareTag("Rino"))
        {
            LoseHealth();
        }

        if (collision.gameObject.CompareTag("Bat"))
        {
            LoseHealth();
        }

        if (collision.gameObject.CompareTag("Trunk"))
        {
            LoseHealth();
        }
    }

    public void LoseHealth()
    {
        if (isInvincible)
        {
            return;
        }
        lives -= 1;
        if (lives == 0)
        {
            Die();
            return;
        }
        else
        {
            trans.localScale = new Vector3(size, size, size);
        }
        StartCoroutine(BecomeTemporarilyInvincible(hitInvincibilityDurationSeconds));
    }

    public IEnumerator BecomeTemporarilyInvincible(float invincibilityDurationSeconds)
    {
        if (!isInvincible)
        {
            isInvincible = true;
            yield return new WaitForSeconds(invincibilityDurationSeconds);
            isInvincible = false;
            Color ColorToAdjust = sprite.material.color;
            ColorToAdjust.a = 1f;
            sprite.material.color = ColorToAdjust;
        }
    }

    private void Die()
    {
        PlayerPrefs.SetInt("Coin", 0);
        model.JumpForce = 0;
        model.MovementSpeed = 0;
        anim.SetTrigger("death");
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Blink()
    {
        Color ColorToAdjust = sprite.material.color;
        if (isInvincible)
        {
            spriteBlinkingTimer += Time.deltaTime;
            if (spriteBlinkingTimer >= spriteBlinkingMiniDuration)
            {
                spriteBlinkingTimer = 0.0f;
                if (ColorToAdjust.a == 1f)
                {
                    ColorToAdjust.a = 0.5f;
                    sprite.material.color = ColorToAdjust;
                }
                else
                {
                    ColorToAdjust.a = 1f;
                    sprite.material.color = ColorToAdjust;
                }
            }
        }
    }
}
