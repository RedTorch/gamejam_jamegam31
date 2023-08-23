using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    // textbox showing percent!
    [SerializeField] private TMP_Text text_percent;
    [SerializeField] private GameObject textHolder;
    [SerializeField] private TMP_Text text_dialogue;
    [SerializeField] private TMP_Text text_timer;
    // [SerializeField] private TMP_Text time;
    private int totalPaintNumber;
    private int currentPaintNumber = 0;

    private bool hideDialogue = false;
    private float dialogueDuration = 0f;
    private string dialogueText = "Default";

    private float timeRemaining = 60f;

    private float scoref = 0f;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] paints = GameObject.FindGameObjectsWithTag("PaintCollider");
        foreach(GameObject po in paints)
        {
            po.GetComponent<PaintTrigger>().SetGameManager(gameObject.GetComponent<GameManager>());
        }
        totalPaintNumber = paints.Length;
        textHolder.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        text_percent.text = (Mathf.Floor((currentPaintNumber * 100f)/ totalPaintNumber)).ToString() + "%";
        if(dialogueDuration>0f && !hideDialogue)
        {
            textHolder.SetActive(true);
            dialogueDuration -= Time.deltaTime;
            if(dialogueDuration <= 0f)
            {
                textHolder.SetActive(false);
            }
        }
        text_timer.text = formatTime(timeRemaining);
        // update score
        // check for gameComplete
    }

    public void OnPaintDestroyed()
    {
        currentPaintNumber += 1;
    }

    public void ShowMessage(string messageText, float duration)
    {
        dialogueText = messageText;
        dialogueDuration = duration;
    }

    private string formatTime(float timeInFloat)
    {
        string minutes = (Mathf.Floor(timeInFloat/60)).ToString();
        string seconds = (Mathf.Floor(timeInFloat%60)).ToString();
        if(timeInFloat%60<10f)
        {
            seconds = "0" + seconds;
        }
        return (minutes + ":" + seconds);
    }

    public bool isLevelComplete()
    {
        return ((currentPaintNumber >= totalPaintNumber) || (timeRemaining <= 0f));
    }
}
