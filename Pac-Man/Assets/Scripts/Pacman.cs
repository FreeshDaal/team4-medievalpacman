using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Pacman : MonoBehaviour
{
    public Movement movement { get; private set; }
    
    public AudioClip PacNoise;
   
    private void Awake()
    {
        this.movement = GetComponent<Movement>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            this.movement.SetDirection(Vector2.up);
            GetComponent<AudioSource>().clip = PacNoise;
            GetComponent<AudioSource>().Play();
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            this.movement.SetDirection(Vector2.down);
             GetComponent<AudioSource>().clip = PacNoise;
            GetComponent<AudioSource>().Play();
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            this.movement.SetDirection(Vector2.left);
             GetComponent<AudioSource>().clip = PacNoise;
            GetComponent<AudioSource>().Play();
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            this.movement.SetDirection(Vector2.right);
             GetComponent<AudioSource>().clip = PacNoise;
            GetComponent<AudioSource>().Play();
        }

        float angle = Mathf.Atan2(this.movement.direction.y, this.movement.direction.x);
        this.transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }

    public void ResetState()
    {
        this.gameObject.SetActive(true);
        this.movement.ResetState();
    }
}
