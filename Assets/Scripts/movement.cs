using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class movement : MonoBehaviour
{
    [SerializeField] private FieldOfView fieldOfView;
    BoxCollider2D boxCollider;

    public GameObject pauseMenu;
    public GameObject Camera;
    public float stamina;
    public float staminaIncreaseRate;
    public float staminaDecreaseRate;
    public Slider staminaBar;

    float mvSpeed = 8f;
    float runSpeed = 12f;

    Rigidbody2D rb;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        stamina = 1f;
    }

    Vector2 mv;
    private void Update() {
        float ad = Input.GetAxisRaw("Horizontal");
        float ws = Input.GetAxisRaw("Vertical");

        if(ad > 0) {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
        } else if (ad < 0) {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z);

        }

        mv = new Vector2(ad, ws);

        if(Input.GetKeyDown(KeyCode.Escape)) {
            if(Time.timeScale == 1) {
                pauseMenu.SetActive(true);
                PauseGame();
            } else {
                pauseMenu.SetActive(false);
                ResumeGame();
            }
        }
    }
    void FixedUpdate()
    {
        

        float speed = mvSpeed;

        if(Input.GetKey(KeyCode.LeftShift)) {
            if(stamina == 0 || staminaIncreaseRate == 0) {
                if(staminaIncreaseRate != 0) {
                    float def = staminaIncreaseRate;
                    staminaIncreaseRate = 0f;
                    StartCoroutine(RestoreStaminaIncreaseRateAfter(0.2f, def));
                }
                
            }
            else {
                speed = runSpeed;

            }
        }
        
        mv = Vector3.Normalize(mv);        

        if(mv != Vector2.zero && speed == runSpeed) {
            staminaDecrease();
        } else if(mv == Vector2.zero){
            staminaIncrease(1.5f);
        } else {
            staminaIncrease(1f);
        }

        updateStaminaBar();
        
        rb.MovePosition(rb.position + mv * speed * Time.fixedDeltaTime);

        fieldOfView.SetOrigin(transform.position);
        fieldOfView.SetAimDirection(transform.position - UnityEngine.Camera.main.ScreenToWorldPoint(Input.mousePosition));
        cameraFollow();
    }
    private void OnCollisionEnter2D(Collision2D collision) {      
        rb.velocity = Vector2.zero;        
    }
    void cameraFollow() {
        float maxDelta = 20f;

        float newx = Mathf.MoveTowards(Camera.transform.position.x, transform.position.x, maxDelta);
        float newy = Mathf.MoveTowards(Camera.transform.position.y, transform.position.y, maxDelta);
        Vector3 newpos = new Vector3(newx, newy, Camera.transform.position.z);

        Camera.transform.position = newpos;        
    }
    void staminaIncrease(float multiplier) {
        if(stamina < 1) {
            stamina += (multiplier * staminaIncreaseRate) * Time.deltaTime;
        } else {
            stamina = 1;
            staminaBar.gameObject.SetActive(false);
        }

    }
    void staminaDecrease() {
        staminaBar.gameObject.SetActive(true);

        if(stamina > 0) {
            stamina -= staminaDecreaseRate * Time.deltaTime;
        } else {
            stamina = 0;
        }

    }
    void updateStaminaBar() {
        staminaBar.value = Mathf.Min(1, Mathf.Max(stamina, 0));
    }
    IEnumerator RestoreStaminaIncreaseRateAfter(float sec, float def) {
        yield return new WaitForSeconds(sec);
        staminaIncreaseRate = def;
    }
    public void PauseGame() {
        Time.timeScale = 0;
    }

    public void ResumeGame() {
        Time.timeScale = 1;
    }
}
