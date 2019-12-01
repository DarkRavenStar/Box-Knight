using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISystem : MonoBehaviour {

    public CanvasGroup ArmourGain;
    public CanvasGroup ArmourBreak;
    public CanvasGroup Attack;
    public CanvasGroup Heal;

    //Status Ailment
    public CanvasGroup PoisonIcon;
    public CanvasGroup BleedIcon;
    public CanvasGroup StunIcon;
    public CanvasGroup RegenIcon;
    public CanvasGroup CounterIcon;
    public CanvasGroup ResistIcon;
    public CanvasGroup FatigueIcon;
    public CanvasGroup ElementlessIcon;
    public bool Poisoned;
    public bool Bleeding;
    public bool Stunned;
    public bool Regenning;
    public bool Countered;
    public bool Resisted;
    public bool Elementless;

    public Vector3 TargetScale;
    public Vector3 ResetScale;
    public float FracJourney;
    public float Speed;

    // Use this for initialization
    void Start () {
        ArmourGain.alpha = 0.0f;
        Attack.alpha = 0.0f;
        Heal.alpha = 0.0f;

        PoisonIcon.alpha = 0.0f;
        BleedIcon.alpha = 0.0f;
        StunIcon.alpha = 0.0f;
        RegenIcon.alpha = 0.0f;
        CounterIcon.alpha = 0.0f;
        ResistIcon.alpha = 0.0f;
        ElementlessIcon.alpha = 0.0f;

        Poisoned = false;
        Bleeding = false;
        Stunned = false;
        Regenning = false;
        Countered = false;
        Resisted = false;
        Elementless = false;
}

    [ContextMenu("TestArmourAppear")]
    public void ArmourAppear()
    {
        ArmourGain.alpha = 0.5f;
    }

    [ContextMenu("TestArmourBreak")]
    public void ArmourDestroy()
    {
        ArmourBreak.alpha = 0.5f;
    }

    [ContextMenu("TestAttackAppear")]
    public void AttackAppear()
    {
        Attack.alpha = 0.5f;
    }

    [ContextMenu("TestHealAppear")]
    public void HealAppear()
    {
        Heal.alpha = 0.5f;
    }

    [ContextMenu("TestPoisonOn")]
    public void PoisonOn()
    {
        PoisonIcon.gameObject.SetActive(true);
        PoisonIcon.alpha = 1.0f;
    }

    [ContextMenu("TestPoisonOff")]
    public void PoisonOff()
    {
        PoisonIcon.alpha = 0.0f;
        Poisoned = false;
        PoisonIcon.gameObject.SetActive(false);
    }

    [ContextMenu("TestBleedOn")]
    public void BleedOn()
    {
        BleedIcon.gameObject.SetActive(true);
        BleedIcon.alpha = 0.1f;
    }

    [ContextMenu("TestBleedOff")]
    public void BleedOff()
    {
        BleedIcon.alpha = 0.0f;
        Bleeding = false;
        BleedIcon.gameObject.SetActive(false);
    }

    [ContextMenu("TestStunOn")]
    public void StunOn()
    {
        StunIcon.gameObject.SetActive(true);
        StunIcon.alpha = 0.1f;
    }

    [ContextMenu("TestStunOff")]
    public void StunOff()
    {
        StunIcon.alpha = 0.0f;
        Stunned = false;
        StunIcon.gameObject.SetActive(false);
    }

    [ContextMenu("TestRegenOn")]
    public void RegenOn()
    {
        RegenIcon.gameObject.SetActive(true);
        RegenIcon.alpha = 0.1f;
    }

    [ContextMenu("TestRegenOff")]
    public void RegenOff()
    {
        RegenIcon.alpha = 0.0f;
        Regenning = false;
        RegenIcon.gameObject.SetActive(false);
    }

    [ContextMenu("TestCounterOn")]
    public void CounterOn()
    {
        CounterIcon.gameObject.SetActive(true);
        CounterIcon.alpha = 0.1f;
    }

    [ContextMenu("TestCounterOff")]
    public void CounterOff()
    {
        CounterIcon.alpha = 0.0f;
        Countered = false;
        CounterIcon.gameObject.SetActive(false);
    }

    [ContextMenu("TestResistOn")]
    public void ResistOn()
    {
        ResistIcon.gameObject.SetActive(true);
        ResistIcon.alpha = 0.1f;
    }

    [ContextMenu("TestResistOff")]
    public void ResistOff()
    {
        ResistIcon.alpha = 0.0f;
        Resisted = false;
        ResistIcon.gameObject.SetActive(false);
    }
    [ContextMenu("TestElementlessOn")]
    public void ElementlessOn()
    {
        ElementlessIcon.gameObject.SetActive(true);
        ElementlessIcon.alpha = 0.1f;
    }

    [ContextMenu("TestElementlessOff")]
    public void ElementlessOff()
    {
        ElementlessIcon.alpha = 0.0f;
        Elementless = false;
        ElementlessIcon.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
        if (ArmourGain.alpha > 0)
        {
            ArmourGain.alpha -= 0.01f;
        }
        if (Attack.alpha > 0)
        {
            Attack.alpha -= 0.01f;
        }
        if(Heal.alpha > 0)
        {
            Heal.alpha -= 0.01f;
        }
        if(ArmourBreak.alpha > 0)
        {
            FracJourney += Speed;
            ArmourBreak.transform.localScale = Vector3.Lerp(ArmourBreak.transform.localScale, TargetScale, FracJourney);
            ArmourBreak.alpha -= 0.01f;
            if (ArmourBreak.transform.localScale == TargetScale && ArmourBreak.alpha == 0.0f)
            {
                ArmourBreak.transform.localScale = ResetScale;
                FracJourney = 0.0f;
            }
        }
        //Status
        if(PoisonIcon.alpha > 0)
        {
            if (Poisoned == false)
            {
                FracJourney += Speed;
                if (FracJourney < 0.5f)
                {
                    PoisonIcon.transform.localScale = Vector3.Lerp(PoisonIcon.transform.localScale, TargetScale, FracJourney);
                }
                else if (FracJourney > 0.5f)
                {
                    PoisonIcon.transform.localScale = Vector3.Lerp(PoisonIcon.transform.localScale, ResetScale, FracJourney);
                }
                PoisonIcon.alpha += 0.01f;
                if (PoisonIcon.transform.localScale == ResetScale && PoisonIcon.alpha >= 1.0f)
                {
                    PoisonIcon.transform.localScale = ResetScale;
                    Poisoned = true;
                    FracJourney = 0.0f;
                }
            }
        }
        if (BleedIcon.alpha > 0)
        {
            if (Bleeding == false)
            {
                FracJourney += Speed;
                if (FracJourney < 0.5f)
                {
                    BleedIcon.transform.localScale = Vector3.Lerp(BleedIcon.transform.localScale, TargetScale, FracJourney);
                }
                else if (FracJourney > 0.5f)
                {
                    BleedIcon.transform.localScale = Vector3.Lerp(BleedIcon.transform.localScale, ResetScale, FracJourney);
                }
                BleedIcon.alpha += 0.01f;
                if (BleedIcon.transform.localScale == ResetScale && BleedIcon.alpha >= 1.0f)
                {
                    BleedIcon.transform.localScale = ResetScale;
                    Bleeding = true;
                    FracJourney = 0.0f;
                }
            }
        }
        if (StunIcon.alpha > 0)
        {
            if (Stunned == false)
            {
                FracJourney += Speed;
                if (FracJourney < 0.5f)
                {
                    StunIcon.transform.localScale = Vector3.Lerp(StunIcon.transform.localScale, TargetScale, FracJourney);
                }
                else if (FracJourney > 0.5f)
                {
                    StunIcon.transform.localScale = Vector3.Lerp(StunIcon.transform.localScale, ResetScale, FracJourney);
                }
                StunIcon.alpha += 0.01f;
                if (StunIcon.transform.localScale == ResetScale && StunIcon.alpha >= 1.0f)
                {
                    StunIcon.transform.localScale = ResetScale;
                    Stunned = true;
                    FracJourney = 0.0f;
                }
            }
        }
        if (RegenIcon.alpha > 0)
        {
            if (Regenning == false)
            {
                FracJourney += Speed;
                if (FracJourney < 0.5f)
                {
                    RegenIcon.transform.localScale = Vector3.Lerp(RegenIcon.transform.localScale, TargetScale, FracJourney);
                }
                else if (FracJourney > 0.5f)
                {
                    RegenIcon.transform.localScale = Vector3.Lerp(RegenIcon.transform.localScale, ResetScale, FracJourney);
                }
                RegenIcon.alpha += 0.01f;
                if (RegenIcon.transform.localScale == ResetScale && RegenIcon.alpha >= 1.0f)
                {
                    RegenIcon.transform.localScale = ResetScale;
                    Regenning = true;
                    FracJourney = 0.0f;
                }
            }
        }
        if (CounterIcon.alpha > 0)
        {
            if (Countered == false)
            {
                FracJourney += Speed;
                if (FracJourney < 0.5f)
                {
                    CounterIcon.transform.localScale = Vector3.Lerp(CounterIcon.transform.localScale, TargetScale, FracJourney);
                }
                else if (FracJourney > 0.5f)
                {
                    CounterIcon.transform.localScale = Vector3.Lerp(CounterIcon.transform.localScale, ResetScale, FracJourney);
                }
                CounterIcon.alpha += 0.01f;
                if (CounterIcon.transform.localScale == ResetScale && CounterIcon.alpha >= 1.0f)
                {
                    CounterIcon.transform.localScale = ResetScale;
                    Countered = true;
                    FracJourney = 0.0f;
                }
            }
        }
        if (ResistIcon.alpha > 0)
        {
            if (Resisted == false)
            {
                FracJourney += Speed;
                if (FracJourney < 0.5f)
                {
                    ResistIcon.transform.localScale = Vector3.Lerp(ResistIcon.transform.localScale, TargetScale, FracJourney);
                }
                else if (FracJourney > 0.5f)
                {
                    ResistIcon.transform.localScale = Vector3.Lerp(ResistIcon.transform.localScale, ResetScale, FracJourney);
                }
                ResistIcon.alpha += 0.01f;
                if (ResistIcon.transform.localScale == ResetScale && ResistIcon.alpha >= 1.0f)
                {
                    ResistIcon.transform.localScale = ResetScale;
                    Resisted = true;
                    FracJourney = 0.0f;
                }
            }
        }
        if (ElementlessIcon.alpha > 0)
        {
            if (Elementless == false)
            {
                FracJourney += Speed;
                if (FracJourney < 0.5f)
                {
                    ElementlessIcon.transform.localScale = Vector3.Lerp(ElementlessIcon.transform.localScale, TargetScale, FracJourney);
                }
                else if (FracJourney > 0.5f)
                {
                    ElementlessIcon.transform.localScale = Vector3.Lerp(ElementlessIcon.transform.localScale, ResetScale, FracJourney);
                }
                ElementlessIcon.alpha += 0.01f;
                if (ElementlessIcon.transform.localScale == ResetScale && ElementlessIcon.alpha >= 1.0f)
                {
                    ElementlessIcon.transform.localScale = ResetScale;
                    Elementless = true;
                    FracJourney = 0.0f;
                }
            }
        }
    }
}
