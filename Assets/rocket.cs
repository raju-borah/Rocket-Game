using UnityEngine;
using UnityEngine.SceneManagement;

public class rocket : MonoBehaviour
{
    Rigidbody rigidBody;
    AudioSource audioSource;

    [SerializeField] float rcsThrust = 100f;//reaction control system
    [SerializeField] float mainThrust = 100f;//reaction control system
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip death;
    
    enum State {Alive,Dead,Transending}
    State state = State.Alive;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidBody=GetComponent<Rigidbody>();
        audioSource= GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {if(state==State.Alive)
        {
            RespondToThrust();
            RespondToRotate();
        }
        
    }
    private void RespondToThrust()
    {
        if (Input.GetKey(KeyCode.Space)) //thrust
        {
            ApplyThrust();
        } 
        else
        {
            audioSource.Stop();
        }
    }
    private void ApplyThrust()
    {
            rigidBody.AddRelativeForce(Vector3.up*mainThrust);
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }
    }
    private void RespondToRotate()
    {
        rigidBody.freezeRotation = true;  //take manual control of rotation
      
        float rotationThisFrame = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A)) //left
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))    //right
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }

        rigidBody.freezeRotation = false;//resume physics control rotation
    }

    void OnCollisionEnter(Collision collision)
    {
        if(state!=State.Alive){      return;     }
        switch (collision.gameObject.tag)
        {
            case "Friendly":print("OK");
                break;
            case "Finish":
               StartSuccessSequence();
                break;
            default:
                StartDeathSequence();
                break;
        }
    }

    private void StartDeathSequence()
    {
                print("DEAD");
                state=State.Dead;
                audioSource.Stop();
                audioSource.PlayOneShot(death);
                Invoke("LoadFirstLevel", 1f);
    }
    private void StartSuccessSequence()
    {
                state = State.Transending;
                audioSource.Stop();
                audioSource.PlayOneShot(success);
                Invoke("LoadNextLevel", 1f);//parameterise time
    }
    private void LoadNextLevel()
    {
        SceneManager.LoadScene(1);
    }
    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }
}
