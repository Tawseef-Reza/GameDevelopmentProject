using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class dummyHurt : MonoBehaviour
{
    public bool hurtingDone = true;
    public GameObject dummyHurtText;
    public int hurtnumber = 0;
    public TextMeshProUGUI text_dummy;
    public Animator _animation;
    // Start is called before the first frame update
    void Start()
    {

        _animation = GetComponent<Animator>();
        dummyHurtText = GameObject.Find("DummyHurtText");
        text_dummy = dummyHurtText.GetComponent<TextMeshProUGUI>();
        dummyHurtText.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
       
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hurtingDone == true && collision.CompareTag("lightSlash"))
        {
            dummyHurtText.SetActive(true);
            _animation.SetBool("isHurting", true);
            StartCoroutine(finishHurt("light"));
            
        }
        else if (collision.CompareTag("heavySlash") && hurtingDone == true)
        {
            dummyHurtText.SetActive(true);
            _animation.SetBool("isHurting", true);
            StartCoroutine(finishHurt("heavy"));
            
        }
        else if (hurtingDone == false)
        {

        }
        
        
    }
    private IEnumerator finishHurt(string magnitude)
    {
        hurtingDone = false;
        
        if (magnitude == "light") 
        {
            hurtnumber += 1;
            text_dummy.text = $"{hurtnumber}!";
            yield return new WaitForSeconds(0.5f);
            hurtingDone = true;
            _animation.SetBool("isHurting", false);
            dummyHurtText.SetActive(false);
        }
        else if (magnitude == "heavy")
        {
            hurtnumber += 3;
            text_dummy.text = $"{hurtnumber}!";
            yield return new WaitForSeconds(0.5f);
            hurtingDone = true;
            _animation.SetBool("isHurting", false);
            dummyHurtText.SetActive(false);
        }


    }
}
