using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Player : MonoBehaviour
{
    [Header("이동 설정")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    [Header("지면 감지")]
    public Transform groundCheck;
    public Vector2 groundCheckSize = new Vector2(0.8f, 0.1f);
    public LayerMask groundLayer;

    [Header("공격")]
    public Transform attackPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public int maxAmmo = 30;
    public float attackCooldown = 1.5f;
    public TextMeshProUGUI ammoText;

    private int currentAmmo;
    private float lastAttackTime = 0f;
    private float shootDirection = 1f;

    private Rigidbody2D rb;
    private bool isGrounded;
    private float moveInput;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentAmmo = maxAmmo;
        UpdateAmmoText();
    }

    void Update()
    {
        UpdateAmmoText();
        // 지면 감지
        isGrounded = Physics2D.OverlapBox(
            groundCheck.position,
            groundCheckSize,
            0f,
            groundLayer
        );
    
        // 좌우 이동 입력 (A, D)
        moveInput = 0f;
        if (Keyboard.current.aKey.isPressed)
            moveInput = -1f;
        else if (Keyboard.current.dKey.isPressed)
            moveInput = 1f;

        // 점프 (SpaceBar)
        if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        // 방향키 꾹 눌러도 발사
        if (Keyboard.current.rightArrowKey.isPressed)
        {
            shootDirection = 1f;
            TryShoot(Vector2.right);
        }
        else if (Keyboard.current.leftArrowKey.isPressed)
        {
            shootDirection = -1f;
            TryShoot(Vector2.left);
        }

        // 캐릭터 방향 전환
        if (moveInput > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (moveInput < 0)
            transform.localScale = new Vector3(-1, 1, 1);

        anim.SetFloat("Speed", Mathf.Abs(moveInput));
        anim.SetBool("IsGrounded", isGrounded);
    }
    void UpdateAmmoText()
    {
        if (ammoText != null)
            ammoText.text =  currentAmmo + " / " + maxAmmo;
    }
    void TryShoot(Vector2 direction)
    {
        // 쿨타임 체크
        if (Time.time - lastAttackTime < attackCooldown) return;

        // 탄약 체크
        if (currentAmmo <= 0)
        {
            Debug.Log("탄약 없음!");
            return;
        }

        Shoot(direction);
    }

    void Shoot(Vector2 direction)
    {
        if (bulletPrefab == null || attackPoint == null) return;

        GameObject bullet = Instantiate(bulletPrefab, attackPoint.position, Quaternion.identity);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
            bulletScript.Init(direction, bulletSpeed);

        currentAmmo--;
        lastAttackTime = Time.time;


        Debug.Log("남은 탄약: " + currentAmmo);
    }

    // 탄약 충전 (필요시 외부에서 호출)
    public void ReloadAmmo()
    {
        currentAmmo = maxAmmo;
    }

    // 현재 탄약 반환
    public int GetCurrentAmmo()
    {
        return currentAmmo;
    }

    void FixedUpdate()
    {
        if (isGrounded)
            rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
        else
            rb.linearVelocity = new Vector2(moveInput * moveSpeed * 0.8f, rb.linearVelocity.y);
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(groundCheck.position, groundCheckSize);
        }
    }
}