using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatDisplay : MonoBehaviour
{
    public Kaiju kaiju;
    public Slider hungerSlider;
    public Slider happinessSlider;
    public GameObject poopIcon;
    public GameObject needsAttentionIcon;
    public GameObject sickIcon;

    public Text weightText;
    public Text ageText;
    public Slider ageSlider;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (kaiju != null)
        {
            if (hungerSlider != null)
            {
                hungerSlider.value = kaiju.getHunger();
            }

            if (happinessSlider != null)
            {
                happinessSlider.value = kaiju.getHappiness();
            }

            if (poopIcon != null)
            {
                poopIcon.SetActive(kaiju.getPoop());
            }

            if (ageText != null)
            {
                ageText.text = kaiju.getAge().ToString();
                ageSlider.value = kaiju.getAge();
            }

            if (weightText != null)
            {
                weightText.text = kaiju.getWeight();
            }

            if (needsAttentionIcon != null)
            {
                needsAttentionIcon.SetActive(kaiju.getAttention());
            }
            
            if (sickIcon != null)
            {
                sickIcon.SetActive(kaiju.getSickness());
            }
        }
    }
}
