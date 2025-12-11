using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class PlungerScript : MonoBehaviour
{
    float power;
    float minPower = 0f;
    public float maxPower = 100f;
    public Slider powerSlider;
    List<Rigidbody> ballList;
    bool ballReady;

    void Start()
    {
        powerSlider.minValue = 0f;
        powerSlider.maxValue = maxPower;
        powerSlider.enabled = true;
        ballList = new List<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ballReady)
        {
            powerSlider.gameObject.SetActive(true);
        }
        else
        {
            powerSlider.gameObject.SetActive(false);
        }

        powerSlider.value = power;
        if (ballList != null && ballList.Count > 0)
        {
            ballReady = true;
            if (Input.GetKey(KeyCode.Space))
            {
                if (power < maxPower)
                {
                    power += 50f * Time.deltaTime;
                }
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                foreach (Rigidbody ball in ballList)
                {
                    ball.AddForce(-transform.right * power);
                }
                power = 0f;
            }
        }
        else
        {
            ballReady = false;
            power = 0f;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("TriggerEnter with: " + other.name);

        if (other.CompareTag("Ball"))
        {
            Debug.Log("Ball detected !");
            ballList.Add(other.attachedRigidbody);
        }


        if (other.gameObject.CompareTag("Ball"))
        {
            if (ballList == null)
            {
                ballList = new List<Rigidbody>();
            }
            ballList.Add(other.gameObject.GetComponent<Rigidbody>());
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            ballList.Remove(other.gameObject.GetComponent<Rigidbody>());
            power = 0f;
        }
    }


}
