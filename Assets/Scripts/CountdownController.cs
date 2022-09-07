using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountdownController : MonoBehaviour
{
    public int countdownTime;
    private int roundTime;

    public TextMeshProUGUI countdownDisplay;
    public TextMeshProUGUI roundTimeDisplay;
    public Slider roundTimeSlider;

    private ParticleSystem particleSys;

    private void Awake()
    {
        particleSys = GameObject.FindGameObjectWithTag("Particles_Progress_Bar").GetComponent<ParticleSystem>();    
    }

    private void Start()
    {
        roundTimeDisplay.gameObject.SetActive(false);
        roundTimeSlider.gameObject.SetActive(false);

        GroundAnimate.current.onNewRoundTime += OnNewRoundTime;
        AnimationAndMovementController.current.onGroundRotationStart += OnStartRotating;
        StartCoroutine(CountDownToStart());

        roundTime = GroundAnimate.current.roundTime;
        roundTimeSlider.value = 0;
        roundTimeSlider.maxValue = roundTime;
    }

    private void OnNewRoundTime(int time)
    {
        roundTimeDisplay.text = "Time: " + time + " / " + roundTime;
        roundTimeSlider.value = time;
        if (time == roundTime && particleSys.isPlaying == true)
        {
            particleSys.Stop();
        }
    }

    private void OnStartRotating()
    {
        roundTimeDisplay.gameObject.SetActive(true);
        roundTimeSlider.gameObject.SetActive(true);
        particleSys.Play();
    }

    IEnumerator CountDownToStart()
    {
        while(countdownTime > 0)
        {
            countdownDisplay.text = countdownTime.ToString();

            yield return new WaitForSeconds(1f);

            countdownTime--;
        }

        countdownDisplay.text = "GO!";

        AnimationAndMovementController.current.BeginGame();

        yield return new WaitForSeconds(1f);

        countdownDisplay.gameObject.SetActive(false);
    }


}
