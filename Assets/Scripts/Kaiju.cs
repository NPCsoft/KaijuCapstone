using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kaiju : MonoBehaviour
{

    public Animator kaijuAnimator;

    private bool isEgg = true;
    public int eggTimerThreshold = 60;
    public int lifeSpan = 60000;
    public float lifeTimer = 0;

    private int hungerCurrent = 100;
    private int hungerMax = 100;
    public int hungerTimerThreshold = 60;
    public float hungerTimer = 0;
    private int weight = 1;

    private int happinessCurrent = 100;
    private int happinessMax = 100;
    public int happinessTimerThreshold = 60;
    public float happinessTimer = 0;

    private int poopCurrent = 0;
    private int poopMax = 3;
    public int poopTimerThreshold = 60;
    public float poopTimer = 0;

    public bool isSick = false;
    public int sickTimerThreshold = 60;
    public float sickTimer = 0;

    private bool isFussy = false;
    public int fussyTimerThreshold = 60;
    public float fussyTimer = 0;

    private bool needsAttention = false;
    public int attentionTimerThreshold = 300;
    public float attentionTimer = 0;
    public int raisingMistakes = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        lifeStep();

        if (!isEgg)
        {
            hungerStep();
            happinessStep();
            poopStep();
            fussyStep();
            attentionStep();
            sicknessStep();
        }

    }

    //Runs the life timer and evolves into a new form or dies after a certain time
    public void lifeStep()
    {
        lifeTimer += 1 * Time.deltaTime;
        if (isEgg && lifeTimer > eggTimerThreshold)
        {
            hatch();
        }

        if (lifeTimer > lifeSpan)
        {
            //gameOver();
        }
    }

    //changes sprite (and later animations), sets isEgg to false, and sets all stats to very low as babies are needy!
    public void hatch()
    {
        kaijuAnimator.SetBool("isHatched", true);
        isEgg = false;
        happinessCurrent = 60;
        hungerCurrent = 0;
    }

    //Runs the hunger timer and drains hunger
    public void hungerStep()
    {
        hungerTimer += 1 * Time.deltaTime;

        if (hungerTimer > hungerTimerThreshold)
        {
            setHunger(-1);
            hungerTimer = 0;

            if (happinessCurrent > hungerCurrent)
            {
                setWeight(-1);
            }
            else
            {
                setWeight(1);
            }
        }
    }

    public int getHunger()
    {
        return hungerCurrent;
    }

    public void setHunger(int hungerChange)
    {
        hungerCurrent += hungerChange;
        setWeight(hungerChange);
        if (hungerCurrent > hungerMax)
        {
            hungerCurrent = hungerMax;
        }
        else if (hungerCurrent < 0)
        {
            hungerCurrent = 0;
        }
    }

    //Runs the happiness timer and drains happiness
    public void happinessStep()
    {
        happinessTimer += 1 * Time.deltaTime;

        if (happinessTimer > happinessTimerThreshold)
        {
            setHappiness(-1);
            happinessTimer = 0;
        }

    }

    public int getHappiness()
    {
        return happinessCurrent;
    }

    public void setHappiness(int happinessChange)
    {
        happinessCurrent += happinessChange;
        setWeight(-1);
        if (happinessCurrent > happinessMax)
        {
            happinessCurrent = happinessMax;
        }
        else if (happinessCurrent < 0)
        {
            happinessCurrent = 0;
        }
    }

    //Runs poop timer and poops
    public void poopStep()
    {
        poopTimer += 1 * Time.deltaTime;

        if (poopTimer > poopTimerThreshold)
        {
            setPoop(1);
            poopTimer = 0;
        }
    }

    public bool getPoop()
    {
        if (poopCurrent > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void setPoop(int poopChange)
    {
        poopCurrent += poopChange;
        setWeight(-2);
        if (poopCurrent > poopMax)
        {
            poopCurrent = poopMax;
        }
        else if (poopCurrent < 0)
        {
            poopCurrent = 0;
        }
    }

    //SICKNESS FUNCTIONALITY
    public void sicknessStep()
    {
        sickTimer += 1 * Time.deltaTime;
        if (sickTimer > sickTimerThreshold)
        {
            int sickChancePercent = 5;
            for (int i = 1; i <= poopMax; i++)
            {
                if (poopCurrent == i)
                {
                    sickChancePercent += 20 * i;
                }
            }
            int chance = Random.Range(0, 100);
            for (int i = 0; i < 100; i++)
            {
                if (chance < sickChancePercent)
                {
                    setSickness(true);
                }
            }

            sickTimer = 0;
        }
            

    }

    public bool getSickness()
    {
        return isSick;
    }

    public void setSickness(bool sicknessChange)
    {
        isSick = sicknessChange;
    }

    //Runs fussy timer and fusses
    public void fussyStep()
    {
        fussyTimer += 1 * Time.deltaTime;

        if (fussyTimer > fussyTimerThreshold)
        {
            setFussy(true);
            fussyTimer = 0;
        }
    }

    public string getFussy()
    {
        return isFussy.ToString();
    }

    public void setFussy(bool fussyChange)
    {
        isFussy = fussyChange;
    }


    //Runs needs timer and sets needs attention icon to display if needs are not met OR fussy is active
    public void attentionStep()
    {
        checkNeeds();
        if (needsAttention)
        {
            attentionTimer += 1 * Time.deltaTime;

            if (attentionTimer > attentionTimerThreshold)
            {
                raisingMistakes += 1;
                attentionTimer = 0;
            }
        }

    }

    public void checkNeeds()
    {
        if (isFussy || poopCurrent > 0 || happinessCurrent < happinessMax / 2 || hungerCurrent < hungerMax / 2 || isSick)
        {
            needsAttention = true;
        }
        else
        {
            needsAttention = false;
            attentionTimer = 0;
        }
    }

    public bool getAttention()
    {
        return needsAttention;
    }

    public void discipline()
    {
        if (isFussy)
        {
            isFussy = false;
            fussyTimer = 0;
        }
        else
        {
            raisingMistakes += 1;
            setHappiness(-20);
        }

        setHappiness(-10);
    }


    //DERIVATIVE STATS
    public float getAge()
    {
        return Mathf.Floor(lifeTimer/60);
    }

    public string getWeight()
    {
        return weight.ToString();
    }

    public void setWeight(int weightChange)
    {
        weight += weightChange;
        if (weight < 0)
        {
            weight = 0;
        }
    }


}
