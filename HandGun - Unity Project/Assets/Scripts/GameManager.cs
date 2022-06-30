using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

enum Difficulty { None, Hard, Medium, Easy}

public class GameManager : MonoBehaviour
{
    [SerializeField] ObjectSpawner spawner;
    [SerializeField] Player player;
    public Music music;
    AudioSource audioSource;

    [SerializeField] Difficulty difficulty;
    [SerializeField] int delayStart = 5;
    bool startedPlaying = false;

    [SerializeField]int currentScore, scorePerTarget = 10;

    int currentMultiplier, multiplierTracker;
    [SerializeField] int[] multiplierThreshold;

    [SerializeField] TextMeshPro scoreText;
    [SerializeField] TextMeshPro multiplierText;

    int totalTargets, targetHits, targetMisses;
    
    [SerializeField] GameObject scoreBoard;
    [SerializeField] GameObject resultScreen;
    [SerializeField] TextMeshProUGUI hitsText, missesText, percentageText, finalScoreText;

    [SerializeField] GameObject chargesHolder;
    [SerializeField] GameObject chargePrefab;
    [SerializeField] GameObject outOfChargesText;

    public static GameManager Instance;

    // Start is called before the first frame update
    void Start()
    {

        currentScore = 0;
        currentMultiplier = 1;
        totalTargets = targetHits = targetMisses = 0;

        Instance = this;

        audioSource = music.audioSource;

        StartCoroutine("StartMusic");

        spawner.Activate(music.beatTempo, (int)difficulty);

        for(int i = 0; i < player.getBulletCount(); i++)
        {
            Instantiate(chargePrefab, Vector3.zero, Quaternion.identity, chargesHolder.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
        CheckMusicState();
        CheatCodes();
    }

    IEnumerator StartMusic(){
        yield return new WaitForSeconds(music.beatTempo * delayStart);

        startedPlaying = true;
        audioSource.Play();
    }

    public void TargetDestroyed()
    {
        if(currentMultiplier - 1 < multiplierThreshold.Length)
        {
            multiplierTracker++;

            if(multiplierThreshold[currentMultiplier -1] <= multiplierTracker)
            {
                multiplierTracker = 0;
                currentMultiplier++;
            }
        }

        currentScore += scorePerTarget * currentMultiplier;

        targetHits++;
    }

    public void TargetMissed()
    {
        currentMultiplier = 1;
        multiplierTracker = 0;

        targetMisses++;
    }

    void UpdateUI()
    {
        scoreText.text = "Score: " + currentScore;
        multiplierText.text = "Multiplier: " + currentMultiplier + "x";
        UpdateCharges();
    }

    void UpdateCharges()
    {
        int charges = player.getBulletCount();

        outOfChargesText.SetActive(charges == 0);

        for(int i = 0; i < chargesHolder.transform.childCount; i++)
        {
            chargesHolder.transform.GetChild(i).gameObject.SetActive(i+1 <= charges);
        }
    }

    public void AddTarget()
    {
        totalTargets++;
    }

    void CheckMusicState()
    {
        if(!startedPlaying)
            return;

        if(!audioSource.isPlaying && !resultScreen.activeInHierarchy)
        {
            spawner.Deactivate();
            scoreBoard.SetActive(false);
            resultScreen.SetActive(true);

            hitsText.text = "" + targetHits;
            missesText.text = "" + targetMisses;
            percentageText.text = "" + (int)(targetHits * 100.0f/totalTargets);
            finalScoreText.text = "" + currentScore;
        }
    }

    void CheatCodes()
    {
        if(Input.GetKeyDown(KeyCode.Keypad7))
            audioSource.Stop();
    }
}
