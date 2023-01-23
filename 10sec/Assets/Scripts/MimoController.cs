using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class MimoController : MonoBehaviour
{
    Vector3 mousePosition;
   
    Vector2 position = new Vector2(0f, 0f);
    Rigidbody2D rigidbody2d;

    public float maxTurnSpeed = 50;
   public float smoothTime = 0.3f;
    float angle;
    float currentVelocity;
   public float speed;
   public float rotationOffset;
   public ParticleSystem PodSmashParticle;

  
   public TextMeshProUGUI scoreText;
   public TextMeshProUGUI timeText;
   float currentTime = 0f;
   float startingTime = 10f;
   private int score;

  /// 

  public GameObject loseText;
  public GameObject winText;
  public GameObject commandText;

 public float waitTime = 2.0f;
  //public TextMeshProUGUI command;

  AudioSource audioSource;
  public AudioClip chompSound;
  public GameObject winSound;
  public GameObject loseSound;
  public GameObject bgMusic;
  public GameObject announceMusic;



    private void Start(){
        StartCoroutine(Command(2.0f));
        rigidbody2d = GetComponent<Rigidbody2D>();
        transform.position = new Vector3(0, 0, 0);
        score = 0;
        SetScoreText();
        currentTime = startingTime;
        loseText.SetActive(false);
        winText.SetActive(false);
        
        audioSource = GetComponent<AudioSource>();
        bgMusic.SetActive(true);
        winSound.SetActive(false);
        loseSound.SetActive(false);
         

    }
IEnumerator Command(float waitTime){
    announceMusic.SetActive(true);
    Time.timeScale = 0f;
    commandText.SetActive(true);
    yield return new WaitForSecondsRealtime(waitTime);
    Time.timeScale = 1.0f;
    commandText.SetActive(false);
}
 
  private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Pod"))
        {
            Destroy(other.gameObject);
            PodSmashParticle.Play();
            GetComponent<PolygonCollider2D>().enabled = false;
            PlaySound(chompSound);
            score ++;
            SetScoreText();
}

    }

    void SetScoreText(){
        scoreText.text = "Score: " + score.ToString ();
    }

    private void Update(){
        currentTime -= 1 * Time.deltaTime;
        timeText.text = currentTime.ToString("0");
        if(currentTime <= 0){
            currentTime = 0;
            loseSound.SetActive(true);
            loseText.SetActive(true);
            bgMusic.SetActive(false);
            Time.timeScale = 0;
            if(Input.GetKeyDown(KeyCode.R)){
                Restart();
            }
            
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        if(score >=5)
        {
            winText.SetActive(true);
            winSound.SetActive(true);
            bgMusic.SetActive(false);
            Time.timeScale = 0;
            if(Input.GetKeyDown(KeyCode.R)){
                    Restart();
            }
        }


        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePosition - transform.position;
        float targetAngle = Vector2.SignedAngle(Vector2.up, direction);
        angle = Mathf.SmoothDampAngle(angle, targetAngle, ref currentVelocity, smoothTime, maxTurnSpeed);
        transform.eulerAngles = new Vector3(0, 0, angle);

       
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0;
        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);

        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        Vector3 targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPos.z = 0;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        /////

        //commandTime -= Time.deltaTime;
        //command.text = (commandTime).ToString("0");

        //if (commandTime > 0){
          //  command.text = "Collect 5 bugs before the time runs out!";
          //  Time.timeScale = 0;
       // }
       // if (commandTime < 0){
       //     command.text = "";
         //   Time.timeScale = 1;
      //  }

    }


    void Restart(){
        SceneManager.LoadScene("mainscene");
        Time.timeScale = 1;
    }
    private void FixedUpdate(){

        Vector2 position = rigidbody2d.position;
        rigidbody2d.MovePosition(position);
    }

    public void PlaySound(AudioClip clip){
        audioSource.PlayOneShot(clip);
        
    }
}
