using TMPro;
using UnityEngine;

public class gameController : MonoBehaviour
{

    theBall theBall;

    public GameObject startTextObject;

    void Start()
    {
        theBall = GameObject.FindAnyObjectByType<theBall>();
        theBall.rotationSpeed = 0f;
        theBall.rotationForce = 0f;
    }

    void Update()
    {
        
    }

    public void gameStarter()
    {
        startTextObject.SetActive(false);
        theBall.gameStarted = true;
        theBall.rotationSpeed = -100f;
        theBall.rotationForce = -50f;
    }
}
