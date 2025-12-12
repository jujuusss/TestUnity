using UnityEngine;

public class Molemove : MonoBehaviour
{
    public float visibleYHeight = 3.0f;
    public float hiddenYHeight = 0.22f;
    private Vector3 myNewXYZPosition;
    public float speed = 4f;
    public float hideMoleTimer = 1.5f;

    private AudioSource soundEffectShowMole;

    void Awake()
    {
        HideMole();
        transform.localPosition = myNewXYZPosition;


        soundEffectShowMole = GetComponent<AudioSource>();

    }




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = Vector3.Lerp(
            transform.localPosition,
            myNewXYZPosition,
            Time.deltaTime * speed
        );

            hideMoleTimer -= Time.deltaTime;
        if (hideMoleTimer < 0 )
        {
        HideMole();
        }

    }
    
    
    public void HideMole()

    {
    myNewXYZPosition = new Vector3 (
    transform.localPosition.x,
    hiddenYHeight,
    transform.localPosition.z
                                );
    
    }



    public void ShowMole()
    {
        myNewXYZPosition = new Vector3(
            transform.localPosition.x,
            visibleYHeight,
            transform.localPosition.z
        );


        hideMoleTimer = 1.5f; 

        soundEffectShowMole.Play();
    }
}
