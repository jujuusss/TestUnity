using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public Rigidbody rb;
    [SerializeField]
    float power;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
          if(Input.GetKeyDown(KeyCode.Return))
        {
            rb.AddForce(Vector3.forward * power);
            AudioSource source = GetComponent<AudioSource>();
            source.Play();
        }
    }
    }