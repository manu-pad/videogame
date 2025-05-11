using UnityEngine;

public class NPCAnimation : MonoBehaviour
{
    public Sprite[] walkFrames;
    public float frameRate = 0.1f;

    private SpriteRenderer spriteRenderer;
    private int currentFrame;
    private float timer;

    private Transform playerTransform;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentFrame = 0;
        timer = 0f;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogWarning("Player não encontrado! Certifique-se de que ele tem a tag 'Player'.");
        }
    }

    void Update()
    {
        if (walkFrames.Length == 0) return;

        timer += Time.deltaTime;
        if (timer >= frameRate)
        {
            timer = 0f;
            currentFrame = (currentFrame + 1) % walkFrames.Length;
            spriteRenderer.sprite = walkFrames[currentFrame];
        }

        if (playerTransform != null)
        {
            if (playerTransform.position.x > transform.position.x)
                spriteRenderer.flipX = false;
            else
                spriteRenderer.flipX = true;
        }
    }
}
