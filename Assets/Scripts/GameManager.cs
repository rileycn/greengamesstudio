using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager main;



    public float seed;
    public float water;
    public float fert;

    public float cropYield;
    public float crop;
    public float maxcrop;

    public Transform cropValue;
    public Transform seedValue;
    public Transform waterValue;
    public Transform fertValue;
    public Transform dialObject;
    public TMP_Text monthText;
    public TMP_Text yearText;
    public TMP_Text seedText;
    public TMP_Text waterText;
    public TMP_Text fertText;

    public Image seasonSplash;
    public TMP_Text splashText;

    public GameObject cloudObj;
    public GameObject windObj;
    public GameObject droughtObj;

    public FieldManager field;

    private float maxTime = 240f;
    private float startTime;

    private float maxValue = 50f;

    public int year = 3;

    private float nextDisaster = -999f;

    public bool splashUp = false;

    public GameObject plantSet;
    public GameObject maintainSet;
    public GameObject harvestSet;

    public Image endGameSplash;
    public TMP_Text endYearText;
    public TMP_Text cropsProducedText;
    public TMP_Text cashPerCropText;
    public TMP_Text cashEarnedText;
    public GameObject endNextButton;

    public GameObject farmIntroDialogue;
    public GameObject endYear1Dialogue;
    public GameObject startYear2Dialogue;
    public GameObject endYear2Dialogue;
    public GameObject startYear3Dialogue;
    public GameObject endYear3Dialogue;

    public enum SeasonValue
    {
        Planting = 0,
        Rainy = 1,
        Harvest = 2,
        Dry = 3
    };

    public enum MonthValue
    {
        August = 0,
        September = 1,
        October = 2,
        November = 3,
        December = 4,
        January = 5,
        February = 6,
        March = 7,
        April = 8,
        May = 9,
        June = 10,
        July = 11
    };

    public MonthValue month;
    public SeasonValue season;

    public GameObject rainstormMinigame;
    public GameObject windstormMinigame;
    public GameObject droughtMinigame;

    public Transform gamePutIn;
    public GameObject warningScreen;
    public TMP_Text warningText;

    public bool inWarning = false;

    private float season1val = 1f / 3f;
    private float season2val = 1f / 3f + 1f/4f;
    private float season3val = 1f / 3f + 1f / 4f + 1/4f;

    private int firstDroughtIndex = 0;
    private int firstWindstormIndex = 0;
    private int firstRainstormIndex = 0;

    public bool isStarted  = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        main = this;
    }

    private void Start()
    {
        seed = GameInfo.seed;
        water = GameInfo.water;
        fert = GameInfo.fert;
        year = GameInfo.year;
        crop = 0;
        cropYield = 0;
        yearText.text = "Year " + year;
        nextDisaster = Time.time + 10f + Random.value * 5f;
        UpdateMeterVisuals();
        if (year == 1)
        {
            farmIntroDialogue.SetActive(true);
        } else if (year == 2)
        {
            startYear2Dialogue.SetActive(true);
        }
        else if (year == 3)
        {
            startYear3Dialogue.SetActive(true);
        }
        //StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (isStarted) {
            UpdateDial();
            CalculateMonth();
            DisasterCheck();
        }
    }

    public void StartGame()
    {
        startTime = Time.time;
        maxcrop = seed;
        season = SeasonValue.Planting;
        isStarted = true;
        SplashScreen();
    }

    private void DisasterCheck()
    {
        if (Time.time > nextDisaster) {
            switch (season)
            {
                case SeasonValue.Planting:
                    if (year == 1) {
                        Instantiate(windObj);
                    } else if (year == 2)
                    {
                        Instantiate(windObj);
                        firstWindstormIndex++;
                        if (Random.value < 0.5f)
                        {
                            Instantiate(cloudObj);
                        }
                        if (firstWindstormIndex == 2)
                        {
                            Warning(1); //windstorm warning
                        }
                    } else if (year == 3)
                    {
                        Instantiate(windObj);
                        if (Random.value < 0.5f)
                        {
                            Instantiate(cloudObj);
                        }
                        firstWindstormIndex++;
                        if (firstWindstormIndex % 3 == 0)
                        {
                            if (Random.value < 0.5f)
                            {
                                Warning(1);
                            } else
                            {
                                Warning(0);
                            }
                        }
                        //Instantiate(droughtObj);
                        //Instantiate(cloudObj);
                        //rain warning?
                        //Warning(2);
                    }
                    if (Time.time - startTime > (season1val * maxTime) - 30f)
                    {
                        nextDisaster = Time.time + 9999f;
                    } else {
                        nextDisaster = Time.time + Random.value * (5f) + (15f / year);
                    }
                    break;
                case SeasonValue.Rainy:
                    if (year == 1) {
                        Instantiate(cloudObj);
                    } else if (year == 2)
                    {
                        if (Random.value < 0.75f)
                        {
                            Instantiate(cloudObj);
                        } else if (Random.value < 0.25f)
                        {
                            Instantiate(droughtObj);
                        }
                    }
                    else if (year == 3)
                    {
                        firstDroughtIndex++;
                        if (Random.value < 0.5f)
                        {
                            Instantiate(cloudObj);
                        }
                        else if (Random.value < 0.5f)
                        {
                            Instantiate(droughtObj);
                        }
                        if (firstDroughtIndex == 4)
                        {
                            Warning(2);
                        } else if (firstDroughtIndex >= 5)
                        {
                            if (Random.value < 0.25f)
                            {
                                Warning(2);
                            }
                        }
                    }
                    if (Time.time - startTime > (season2val * maxTime) - 15f)
                    {
                        nextDisaster = Time.time + 9999f;
                    }
                    else
                    {
                        nextDisaster = Time.time + Random.value * (2f) + 5f;
                    }
                    break;
                case SeasonValue.Harvest:
                    if (year == 1)
                    {
                        Instantiate(droughtObj);
                    }
                    else if (year == 2)
                    {
                        Instantiate(droughtObj);
                        firstRainstormIndex++;
                        if (Random.value < 0.25f)
                        {
                            Instantiate(cloudObj);
                        }
                        if (firstRainstormIndex == 2)
                        {
                            Instantiate(cloudObj);
                            Warning(0);
                        }
                    }
                    else if (year == 3)
                    {
                        Instantiate(droughtObj);
                        if (Random.value < 0.5f)
                        {
                            Instantiate(cloudObj);
                        } else
                        {
                            if (Random.value < 0.75f && !splashUp)
                            {
                                Warning(2);
                            }
                        }
                        
                    }
                    nextDisaster = Time.time + Random.value * (5f) + (15f / year);
                    break;
                case SeasonValue.Dry:
                    nextDisaster = Time.time + 9999f;
                    //no disasters?
                    break;
            }
        }
    }

    private void UpdateDial()
    {
        float alpha = (Time.time - startTime) / maxTime;
        switch (season)
        {
            case SeasonValue.Planting:
                if (alpha >= season1val)
                {
                    NextSeason();
                }
                break;
            case SeasonValue.Rainy:
                if (alpha >= season2val)
                {
                    NextSeason();
                }
                break;
            case SeasonValue.Harvest:
                if (alpha >= season3val)
                {
                    NextSeason();
                }
                break;
            case SeasonValue.Dry:
                //shop time :)
                break;
        }
        dialObject.rotation = Quaternion.Euler(new Vector3(0f, 0f, 360f * alpha + 145f));
    }

    private void NextSeason()
    {
        season++;
        nextDisaster = Time.time + (5f / year) + Random.value * 2f;
        field.ResetHealth();
        SplashScreen();
        plantSet.SetActive(false);
        maintainSet.SetActive(false);
        harvestSet.SetActive(false);
        switch (season)
        {
            case SeasonValue.Planting:
                plantSet.SetActive(true);
                break;
            case SeasonValue.Rainy:
                maintainSet.SetActive(true);
                break;
            case SeasonValue.Harvest:
                harvestSet.SetActive(true);
                break;
        }
        DraggableObject.select = null;
    }

    private void CalculateMonth()
    {
        month = (MonthValue) (Mathf.FloorToInt((Time.time - startTime) / maxTime * 12f) % 12);
        switch (month)
        {
            case MonthValue.August:
                monthText.text = "August";
                break;
            case MonthValue.September:
                monthText.text = "September";
                break;
            case MonthValue.October:
                monthText.text = "October";
                break;
            case MonthValue.November:
                monthText.text = "November";
                break;
            case MonthValue.December:
                monthText.text = "December";
                break;
            case MonthValue.January:
                monthText.text = "January";
                break;
            case MonthValue.February:
                monthText.text = "February";
                break;
            case MonthValue.March:
                monthText.text = "March";
                break;
            case MonthValue.April:
                monthText.text = "April";
                break;
            case MonthValue.May:
                monthText.text = "May";
                break;
            case MonthValue.June:
                monthText.text = "June";
                break;
            case MonthValue.July:
                monthText.text = "July";
                break;
        }
    }

    public void UpdateMeterVisuals()
    {
        cropValue.transform.localPosition = new Vector3(-0.5f * (1f - crop / Mathf.Max(1f, maxcrop)), 0f, 0f);
        cropValue.transform.localScale = new Vector3(crop / Mathf.Max(1f, maxcrop), 1f, 1f);
        seedValue.transform.localPosition = new Vector3(-0.5f * (1f - seed / maxValue), 0f, 0f);
        seedValue.transform.localScale = new Vector3(seed / maxValue, 1f, 1f);
        waterValue.transform.localPosition = new Vector3(-0.5f * (1f - water / maxValue), 0f, 0f);
        waterValue.transform.localScale = new Vector3(water / maxValue, 1f, 1f);
        fertValue.transform.localPosition = new Vector3(-0.5f * (1f - fert / maxValue), 0f, 0f);
        fertValue.transform.localScale = new Vector3(fert / maxValue, 1f, 1f);
        seedText.text = "x" + Mathf.FloorToInt(seed);
        waterText.text = "x" + Mathf.FloorToInt(water);
        fertText.text = "x" + Mathf.FloorToInt(fert);
    }

    public void SplashScreen()
    {
        string seasonName = "Unknown";
        switch (season)
        {
            case SeasonValue.Planting:
                seasonName = "Planting";
                break;
            case SeasonValue.Rainy:
                seasonName = "Rainy";
                break;
            case SeasonValue.Harvest:
                seasonName = "Harvest";
                break;
            case SeasonValue.Dry:
                seasonName = "Dry";
                break;
        }
        splashText.text = seasonName + " Season";
        StartCoroutine(SplashAnimation());
    }

    private IEnumerator SplashAnimation()
    {
        splashUp = true;
        seasonSplash.color = new Color(seasonSplash.color.r, seasonSplash.color.g, seasonSplash.color.b, 0);
        splashText.color = new Color(splashText.color.r, splashText.color.g, splashText.color.b, 0);
        seasonSplash.gameObject.SetActive(true);
        float startTime = Time.time;
        while (Time.time - startTime < 4f)
        {
            float alpha = 0f;
            if (Time.time - startTime < 0.5f)
            {
                alpha = 2f * (Time.time - startTime);
            } else if (Time.time - startTime < 3.5f)
            {
                alpha = 1f;
            } else
            {
                alpha = 2f * (4f - (Time.time - startTime));
            }
            seasonSplash.color = new Color(seasonSplash.color.r, seasonSplash.color.g, seasonSplash.color.b, alpha);
            splashText.color = new Color(splashText.color.r, splashText.color.g, splashText.color.b, alpha);
            yield return null;
        }
        seasonSplash.gameObject.SetActive(false);
        yield return null;
        if (season != SeasonValue.Dry) {
            splashUp = false;
        } else
        {
            //end game
            StartCoroutine(EndGameSequence());
        }
    }

    private IEnumerator EndGameSequence()
    {
        cropsProducedText.gameObject.SetActive(false);
        cashPerCropText.gameObject.SetActive(false);
        cashEarnedText.gameObject.SetActive(false);
        endNextButton.SetActive(false);
        endGameSplash.color = new Color(endGameSplash.color.r, endGameSplash.color.g, endGameSplash.color.b, 0);
        endYearText.color = new Color(endGameSplash.color.r, endGameSplash.color.g, endGameSplash.color.b, 0);
        endGameSplash.gameObject.SetActive(true);
        endYearText.gameObject.SetActive(true);
        endYearText.text = "End of Year " + year + "!";
        float startTime = Time.time;
        while (Time.time - startTime < 0.5f)
        {
            float alpha = 2f * (Time.time - startTime);
            endGameSplash.color = new Color(endGameSplash.color.r, endGameSplash.color.g, endGameSplash.color.b, alpha);
            endYearText.color = new Color(endYearText.color.r, endYearText.color.g, endYearText.color.b, alpha);
            yield return null;
        }
        endGameSplash.color = new Color(endGameSplash.color.r, endGameSplash.color.g, endGameSplash.color.b, 1);
        yield return new WaitForSeconds(1f);
        cropsProducedText.text = "Crops Produced: " + cropYield + "/" + maxcrop;
        yield return Punch(cropsProducedText.transform);
        yield return new WaitForSeconds(1f);
        cashPerCropText.text = "Cash Per Unit: " + 20 + "sol";
        yield return Punch(cashPerCropText.transform);
        yield return new WaitForSeconds(1f);
        cashEarnedText.text = "Total Earnings: " + (20 * cropYield) + "sol";
        GameInfo.cash += (20 * cropYield);
        yield return Punch(cashEarnedText.transform);
        yield return new WaitForSeconds(1f);
        if (year == 1) {
            endYear1Dialogue.SetActive(true);
        } else if (year == 2)
        {
            endYear2Dialogue.SetActive(true);
        }
        else if (year == 3)
        {
            endYear3Dialogue.SetActive(true);
        }
    }

    public void AfterYear1EndDialogue()
    {
        StartCoroutine(Punch(endNextButton.transform));
    }
    
    public IEnumerator Punch(Transform obj)
    {
        Vector3 ogsize = obj.localScale;
        obj.transform.localScale = ogsize * 5;
        obj.gameObject.SetActive(true);
        float startTime = Time.time;
        while (Time.time - startTime < 0.25f)
        {
            float alpha = 4f * (Time.time - startTime);
            obj.localScale = Vector3.Lerp(ogsize * 5f, ogsize, alpha);
            yield return null;
        }
        obj.transform.localScale = ogsize;
    }

    public void Warning(int type)
    {
        if (inWarning) return;
        inWarning = true;
        StartCoroutine(WarningSequence(type));
    }

    public IEnumerator WarningSequence(int type)
    {
        if (type == 0)
        {
            warningText.text = "Weather Alert: Rainstorm!";
        } else if (type == 1)
        {
            warningText.text = "Weather Alert: Windstorm!";
        }
        else if (type == 2)
        {
            warningText.text = "Weather Alert: Severe Drought!";
        }
        warningScreen.SetActive(true);
        for (int i = 0; i < 3; i++)
        {
            warningText.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.4f);
            warningText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.1f);
        }
        if (type == 0)
        {
            GameObject newGame = Instantiate(rainstormMinigame, gamePutIn);
        } else if (type == 1)
        {

            GameObject newGame = Instantiate(windstormMinigame, gamePutIn);
        }
        else if (type == 2)
        {
            GameObject newGame = Instantiate(droughtMinigame, gamePutIn);
        }
    }

    public void ExitWarning()
    {
        warningScreen.SetActive(false);
        inWarning = false;
    }

    public void ShopTime()
    {
        GameInfo.seed = (int)seed;
        GameInfo.water = (int)water;
        GameInfo.fert = (int)fert;
        if (year == 3)
        {
            SceneManager.LoadScene("outro screen");
        }
        else
        {
            SceneManager.LoadScene("shop");
        }
    }
}

public class GameInfo
{
    public static int year = 1;
    public static int seed = 0;
    public static int water = 0;
    public static int fert = 0;
    public static float cash = 0f;
}