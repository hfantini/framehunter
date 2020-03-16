
/*

	+ =====================================================================
	|                                                            
	|	== CODECASTLE STUDIOS @ 2016                              
	|   == FRAME HUNTER - GAME                                   
	|	====================================                     
	|   ==    FILE: GameplayScript.cs           						 
	|	==    DATE (YYYY-MM-DD): 2017-06-12 | TIME (HH:MM:SS): 09:33:00 AM              
	|   ==   SINCE: 0.0.1a				                         
	|	==  AUTHOR: Henrique Fantini                             
	|   == CONTACT: henrique@codecitadel.com                        
	|   ====================================                     
	|   ==                                                       

*/

// == IMPORT LIBS
// ==========================================================================================

using System;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

// == CLASS
// ==========================================================================================

public class GameplayScript : MonoBehaviour
{
    // == DECLARATION
    // ======================================================================================

    // == CONST =============================================================================

    private const float FILM_VEL_X = 0.33f;
    private const float MAX_SCORE_PLACES = 8;

    // == VAR ===============================================================================

    Vector2 cameraBounds;
    private EnumGameState lastGameState;
    private EnumGameState currentGameState;
    private EnumGameMenuState currentMenuState;
    private EnumGamePresentationState currentPresentationState;
    private EnumGameCheckSnapState currentCheckSnapState;
    private float timeCountUtil;
    private GameObject sprFilm1;
    private GameObject sprFilm2;
    private GameObject sprShadowRect;
    private GameObject sprBtnTakePhoto;
    private GameObject sprCameraAim;
    private GameObject sprObjTarget;
    private GameObject sprObjCollision;
    private GameObject sprCorrect;
    private GameObject sprWrong;
    private GameObject sprBtnTryAgain;
    private GameObject sprBtnNextLevel;
    private GameObject sprMenuButton;
    private GameObject sprMnuOptions;
    private GameObject btnMnuResume;
    private GameObject btnMnuExit;
    private GameObject btnScoreSend;
    private GameObject btnScoreCancel;
    private GameObject sprLoadingArrow;
	private GameObject sprScoreSent;
    private GameObject sprLevelBackground;
    private GameObject btnBackToMenu;
    private Text txtLevelName;
    private Text txtCountDown;
    private Text txtLives;
    private Text txtScore;
    private Text txtCrono;
    private Text txtTarget;
    private Text txtPrecision;
    private Text txtProgressionPercent;
    private Text txtSuccess;
    private Text txtLevelCompleted;
    private Text txtScoreDetail;
    private Text txtGamePaused;
    private Text txtConfirm;
    private Text txtTapToConfirm;
    private Text txtFinalStatistics;
    private Text txtFinalTime;
    private Text txtFinalTimeValue;
    private Text txtFinalScore;
    private Text txtFinalScoreValue;
    private Text txtFinalLevel;
    private Text txtFinalLevelValue;
    private Text txtSendScore;
    private Text txtYourName;
    private Text txtProcessing;
    private Text txtConnected;
    private Text txtFPS;
    private GameObject txtScoreName;
    private GameObject sprTargetHUD;
    private GameObject sprFlash;
    private float levelElapsedTime;
    private bool invertedFilmOrder = false;
    private int currentLevel = 1;
    private Level level;
    private int lives = 2;
    private int score = 10000;
    private int scoreInitial = 0;
    private int scoreGained = 0;
    private Boolean percentCalc = false;
    private int percentCalcValue = 0;
    private int percentCalcAux = 0;
    private Boolean scoreCalculated = false;
    private Boolean bonusCalculated = false;
    private int bonusScore = 0;
    private Vector3 originalTargetPos;
    private Boolean sendScore = false;
    private GameObject sprScoreWarning;
    private GameObject btnScoreTryAgain;
    private GameObject btnScoreSendCancel;
    private Vector3 defaultAimPosition;
    private Text txtYouWon;

    // == CLASS CONSTRUCTOR(S)
    // ======================================================================================

    // == CLASS METHODS
    // ======================================================================================

    void Start ()
    {
        Application.targetFrameRate = 60;
	QualitySettings.vSyncCount = 0;

        // == INITIALIZING OBJECTS AND VALUES

        currentGameState = EnumGameState.GAMESTATE_PRESENTATION;
        currentPresentationState = EnumGamePresentationState.GAMEPRES_FADEIN_LEVELNAME;
        currentCheckSnapState = EnumGameCheckSnapState.GAMESNAPCHECK_FLASH_FADEIN;
        cameraBounds = Camera.main.ScreenToWorldPoint( new Vector3( Camera.main.pixelWidth, Camera.main.pixelHeight, 0 ) );

        sprFilm1 = GameObject.Find("sprFilm1");
        sprFilm2 = GameObject.Find("sprFilm2");

        sprShadowRect = GameObject.Find("sprShadowRect");
        SpriteRenderer sprDarknessRender = (SpriteRenderer)sprShadowRect.GetComponent<SpriteRenderer>();
        Color sprDarknessColor = sprDarknessRender.color;
        sprDarknessColor.a = 1f;
        sprDarknessRender.color = sprDarknessColor;

        sprBtnTakePhoto = GameObject.Find("sprBtnTakePhoto");
        sprTargetHUD = GameObject.Find("sprHUDTarget");
        sprFlash = GameObject.Find("sprFlash");
        sprObjTarget = GameObject.Find("sprLevelTarget");
        sprCameraAim = GameObject.Find("sprCameraAim");
        defaultAimPosition = sprCameraAim.transform.position;
        sprObjCollision = GameObject.Find("sprTargetCollision");
        sprCorrect = GameObject.Find("sprCorrect");
        sprWrong = GameObject.Find("sprWrong");
        sprBtnTryAgain = GameObject.Find("sprBtnTryAgain");
        sprBtnNextLevel = GameObject.Find("sprBtnNextLevel");
        btnBackToMenu = GameObject.Find("btnBackToMenu");
        btnScoreSend = GameObject.Find("btnScoreSend");
        btnScoreCancel = GameObject.Find("btnScoreCancel");
        sprMenuButton = GameObject.Find("sprBtnMenu");
        sprMnuOptions = GameObject.Find("sprGameMenuOpt");
        btnMnuResume = GameObject.Find("btnMnuResume");
        btnMnuExit = GameObject.Find("btnMnuExit");
        txtLevelName = GameObject.Find("canvasMenu/txtLevelName").GetComponent<Text>();
        txtCountDown = GameObject.Find("canvasGamePlay/txtCountDown").GetComponent<Text>();
        txtSuccess = GameObject.Find("canvasGamePlay/txtSuccess").GetComponent<Text>();
        txtLives = GameObject.Find("canvasGamePlay/txtLives").GetComponent<Text>();
        txtScore = GameObject.Find("canvasGamePlay/txtScore").GetComponent<Text>();
        txtCrono = GameObject.Find("canvasGamePlay/txtCrono").GetComponent<Text>();
        txtTarget = GameObject.Find("canvasGamePlay/txtTarget").GetComponent<Text>();
        txtPrecision = GameObject.Find("canvasGamePlay/txtPrecision").GetComponent<Text>();
        txtLevelCompleted = GameObject.Find("canvasGamePlay/txtLevelCompleted").GetComponent<Text>();
        txtScoreDetail = GameObject.Find("canvasGamePlay/txtScoreDetail").GetComponent<Text>();
        txtProgressionPercent = GameObject.Find("canvasGamePlay/txtPrecisionValue").GetComponent<Text>();
        txtGamePaused = GameObject.Find("canvasMenu/txtGamePaused").GetComponent<Text>();
        txtConfirm = GameObject.Find("canvasMenu/txtConfirm").GetComponent<Text>();
        txtTapToConfirm = GameObject.Find("canvasMenu/txtTapToConfirm").GetComponent<Text>();
        txtFinalStatistics = GameObject.Find("canvasScore/txtFinalStatistics").GetComponent<Text>();
        txtFinalTime = GameObject.Find("canvasScore/txtFinalTime").GetComponent<Text>();
        txtFinalTimeValue = GameObject.Find("canvasScore/txtFinalTimeValue").GetComponent<Text>();
        txtFinalScore = GameObject.Find("canvasScore/txtFinalScore").GetComponent<Text>();
        txtFinalScoreValue = GameObject.Find("canvasScore/txtFinalScoreValue").GetComponent<Text>();
        txtFinalLevel = GameObject.Find("canvasScore/txtFinalLevel").GetComponent<Text>();
        txtFinalLevelValue = GameObject.Find("canvasScore/txtFinalLevelValue").GetComponent<Text>();
        txtYouWon = GameObject.Find("canvasGamePlay/txtYouWon").GetComponent<Text>();
        txtSendScore = GameObject.Find("canvasScore/txtSendScore").GetComponent<Text>();
        txtYourName = GameObject.Find("canvasScore/txtYourName").GetComponent<Text>();
        txtConnected = GameObject.Find("canvasScore/txtConnected").GetComponent<Text>();
        txtScoreName = GameObject.Find("canvasScore/txtScoreName");
        txtScoreName.GetComponent<InputField>().enabled = false;
        //txtFPS = GameObject.Find("canvasGamePlay/txtFPS").GetComponent<Text>();
        txtProcessing = GameObject.Find("canvasScore/txtProcessing").GetComponent<Text>();
        sprLoadingArrow = GameObject.Find("canvasScore/sprLoadingArrow");
		sprScoreSent = GameObject.Find("canvasScore/sprScoreSent");
        sprScoreWarning = GameObject.Find("canvasScore/sprScoreFail");
        btnScoreTryAgain = GameObject.Find("canvasScore/btnScoreTryAgain");
        btnScoreSendCancel = GameObject.Find("canvasScore/btnScoreSendCancel");
        sprLevelBackground = GameObject.Find("canvasGamePlay/sprLevelBackground");

        initLevel();
        txtScoreDetail.text = "";
    }

    private void initLevel()
    {
        level = (Level) System.Activator.CreateInstance(Type.GetType(("Level" + currentLevel)));
        level.loadContent(this);
        sprObjTarget.GetComponent<SpriteRenderer>().sprite = level.targetSprite;
        sprTargetHUD.GetComponent<SpriteRenderer>().sprite = level.targetSprite;
        sprLevelBackground.GetComponent<SpriteRenderer>().sprite = level.levelBackground;
        txtLevelName.text = "Level " + currentLevel;
        originalTargetPos = level.startPosition;
        scoreInitial = score;
        setLevelDefaultValues();
        currentGameState = EnumGameState.GAMESTATE_PRESENTATION;
        currentPresentationState = EnumGamePresentationState.GAMEPRES_FADEIN_LEVELNAME;
        txtScoreDetail.text = "";
        sprCameraAim.transform.position = defaultAimPosition;
    }

    private void resetLevel()
    {
        level.resetLevel();
        setLevelDefaultValues();
        sprTargetHUD.GetComponent<SpriteRenderer>().color = new Color(sprTargetHUD.GetComponent<SpriteRenderer>().color.r, sprTargetHUD.GetComponent<SpriteRenderer>().color.g, sprTargetHUD.GetComponent<SpriteRenderer>().color.b, 0f);
        txtTarget.color = new Color(txtTarget.color.r, txtTarget.color.g, txtTarget.color.b, 0f);
        sprTargetHUD.GetComponent<Renderer>().enabled = true;
        txtTarget.enabled = true;
        sprCameraAim.transform.position = defaultAimPosition;

        currentGameState = EnumGameState.GAMESTATE_COUNTDOWN;
    }

    private void setLevelDefaultValues()
    {
        percentCalc = false;
        percentCalcAux = 0;
        percentCalcValue = 0;
        txtProgressionPercent.text = "0%";
        txtPrecision.color = new Color(txtPrecision.color.r, txtPrecision.color.g, txtPrecision.color.b, 0);
        txtProgressionPercent.color = new Color(txtProgressionPercent.color.r, txtProgressionPercent.color.g, txtProgressionPercent.color.b, 0);
        sprCorrect.GetComponent<SpriteRenderer>().color = new Color(sprCorrect.GetComponent<SpriteRenderer>().color.r, sprCorrect.GetComponent<SpriteRenderer>().color.g, sprCorrect.GetComponent<SpriteRenderer>().color.b, 0);
        sprWrong.GetComponent<SpriteRenderer>().color = new Color(sprWrong.GetComponent<SpriteRenderer>().color.r, sprWrong.GetComponent<SpriteRenderer>().color.g, sprWrong.GetComponent<SpriteRenderer>().color.b, 0);
        sprBtnTryAgain.GetComponent<SpriteRenderer>().color = new Color(sprBtnTryAgain.GetComponent<SpriteRenderer>().color.r, sprBtnTryAgain.GetComponent<SpriteRenderer>().color.g, sprBtnTryAgain.GetComponent<SpriteRenderer>().color.b, 0);
        txtSuccess.color = new Color(txtSuccess.color.r, txtSuccess.color.g, txtSuccess.color.b, 0);
        scoreCalculated = false;
        scoreGained = 0;
        bonusCalculated = false;
        bonusScore = 0;
        sprObjTarget.transform.position = originalTargetPos;
        currentCheckSnapState = EnumGameCheckSnapState.GAMESNAPCHECK_FLASH_FADEIN;
        txtCountDown.text = "3";
    }

    public void destroySprite(GameObject sprite)
    {
        Destroy(sprite);
    }

    void Update()
    {
        // == GENERAL UPDATES

        // = CRONOMETER

        TimeSpan tSpan = TimeSpan.FromSeconds(levelElapsedTime);
        String min = tSpan.Minutes.ToString("D2");
        String sec = tSpan.Seconds.ToString("D2");
        String millis = tSpan.Milliseconds.ToString();

        if(millis.Length == 1)
        {
            millis += "0";
        }

        // = LIVES
        txtLives.text = "x" + lives;

        // = SCORE
        txtScore.text = score.ToString("00000000");
        txtCrono.text = min + ":" + sec + ":" + millis.Substring(0,2);

        // = FPS
        //txtFPS.text = "FPS: " + (1.0f / Time.deltaTime).ToString("00");

        // = MENU BUTTON
        if (sprShadowRect.GetComponent<SpriteRenderer>().color.a <= 0f)
        {
            if (currentGameState != EnumGameState.GAMESTATE_MENU)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Vector3 touchPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Collider2D btnCollider = sprMenuButton.GetComponent<Collider2D>();

                    if (btnCollider.OverlapPoint(touchPoint))
                    {
                        lastGameState = currentGameState;
                        currentMenuState = EnumGameMenuState.GAMEMENU_STATE_FADEIN;
                        currentGameState = EnumGameState.GAMESTATE_MENU;
                    }
                }

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    lastGameState = currentGameState;
                    currentMenuState = EnumGameMenuState.GAMEMENU_STATE_FADEIN;
                    currentGameState = EnumGameState.GAMESTATE_MENU;
                }
            }
        }

        // == UPDATING FILM MOVEMENT EFFECT

        if (invertedFilmOrder == false)
        {
            // FILM 1
            Vector3 sprFilm1Pos = sprFilm1.transform.position;
            sprFilm1Pos.x -= FILM_VEL_X * Time.deltaTime;
            sprFilm1.transform.position = sprFilm1Pos;

            // FILM 2
            Vector3 sprFilm2Pos = sprFilm2.transform.position;
            sprFilm2Pos.x = sprFilm1Pos.x + sprFilm1.GetComponent<SpriteRenderer>().bounds.size.x;
            sprFilm2.transform.position = sprFilm2Pos;

            if (sprFilm1Pos.x < -cameraBounds.x)
            {
                sprFilm1Pos.x = sprFilm2Pos.x + sprFilm1.GetComponent<SpriteRenderer>().bounds.size.x;
                invertedFilmOrder = true;
            }
        }
        else
        {
            // FILM 2
            Vector3 sprFilm2Pos = sprFilm2.transform.position;
            sprFilm2Pos.x -= FILM_VEL_X * Time.deltaTime;
            sprFilm2.transform.position = sprFilm2Pos;

            // FILM 1
            Vector3 sprFilm1Pos = sprFilm1.transform.position;
            sprFilm1Pos.x = sprFilm2Pos.x + sprFilm2.GetComponent<SpriteRenderer>().bounds.size.x;
            sprFilm1.transform.position = sprFilm1Pos;

            if (sprFilm2Pos.x < -cameraBounds.x)
            {
                sprFilm2Pos.x = sprFilm1Pos.x + sprFilm2.GetComponent<SpriteRenderer>().bounds.size.x;
                invertedFilmOrder = false;
            }
        }
        
        // == SPECIFIC UPDATES

        if (currentGameState == EnumGameState.GAMESTATE_PRESENTATION)
        {
            if (currentPresentationState == EnumGamePresentationState.GAMEPRES_FADEIN_LEVELNAME)
            {
                SpriteRenderer sprDarknessRender = (SpriteRenderer)sprShadowRect.GetComponent<SpriteRenderer>();
                Color sprDarknessColor = sprDarknessRender.color;

                if (sprDarknessColor.a > 0.7f)
                {
                    sprDarknessColor.a -= 0.7f * Time.deltaTime;
                }
                else
                {
                    txtLevelName.text = "LEVEL " + level.levelNumber;

                    Color txtColor = txtLevelName.color;
                    txtColor.a += 2f * Time.deltaTime;

                    if (txtColor.a >= 0.7f)
                    {
                        timeCountUtil = Time.time;
                        currentPresentationState = EnumGamePresentationState.GAMEPRES_FADEOUT_DARKNESS;
                    }

                    txtLevelName.color = txtColor;                    
                }

                sprDarknessRender.color = sprDarknessColor;
            }
            else if (currentPresentationState == EnumGamePresentationState.GAMEPRES_FADEOUT_DARKNESS)
            {
                // DELAY
                float timing = Time.time - timeCountUtil;

                if (timing > 2)
                {
                    // FADE-OUT EFFECT

                    SpriteRenderer sprDarknessRender = (SpriteRenderer)sprShadowRect.GetComponent<SpriteRenderer>();
                    Color sprDarknessColor = sprDarknessRender.color;
                    Color txtColor = txtLevelName.color;

                    sprDarknessColor.a -= 0.7f * Time.deltaTime;
                    txtColor.a -= 0.7f * Time.deltaTime;

                    sprDarknessRender.color = sprDarknessColor;
                    txtLevelName.color = txtColor;

                    if (sprDarknessColor.a <= 0 && txtColor.a <= 0)
                    {
                        currentGameState = EnumGameState.GAMESTATE_COUNTDOWN;
                    }
                }
            }
        }
        else if (currentGameState == EnumGameState.GAMESTATE_COUNTDOWN)
        {
            sprBtnTakePhoto.GetComponent<Renderer>().enabled = false;

            Color colorAuxTextCountDown = txtCountDown.color;
            Color colorAuxText = txtCountDown.color;
            Color colorAuxSprite = sprTargetHUD.GetComponent<SpriteRenderer>().color;

            if (colorAuxText.a < 1)
            {
                colorAuxText.a += 1.3f * Time.deltaTime;
                colorAuxTextCountDown.a += 1.3f * Time.deltaTime;
                colorAuxSprite.a += 1.3f * Time.deltaTime;
                timeCountUtil = Time.time;
            }
            else if (colorAuxText.a >= 1)
            {
                float timing = Time.time - timeCountUtil;

                if (timing >= 1)
                {
                    if (timing > 3)
                    {
                        txtCountDown.text = timing.ToString("GO!");

                        if (timing > 5)
                        {
                            colorAuxTextCountDown.a = 0;
                            currentGameState = EnumGameState.GAMESTATE_PLAY;
                        }
                    }
                    else
                    {
                        txtCountDown.text = (4f - timing).ToString("0");
                    }
                }
            }

            txtCountDown.color = colorAuxTextCountDown;
            txtTarget.color = colorAuxText;
            sprTargetHUD.GetComponent<SpriteRenderer>().color = new Color(sprTargetHUD.GetComponent<SpriteRenderer>().color.r, sprTargetHUD.GetComponent<SpriteRenderer>().color.g, sprTargetHUD.GetComponent<SpriteRenderer>().color.b, colorAuxSprite.a);
        }
        else if (currentGameState == EnumGameState.GAMESTATE_PLAY)
        {
            Color sprBtnTakePhotoColor = sprBtnTakePhoto.GetComponent<SpriteRenderer>().color;
            sprBtnTakePhotoColor.a = 1f;
            sprBtnTakePhoto.GetComponent<SpriteRenderer>().color = sprBtnTakePhotoColor;

            // == UPDATING HUD
            sprBtnTakePhoto.GetComponent<Renderer>().enabled = true;

            // = TIME
            levelElapsedTime += Time.deltaTime;

            // == UPDATING LEVEL

            if (score > 0)
            {
                score--;
            }
            else
            {
                score = 0;
            }

            level.update();

            // == TAKE PHOTO BUTTON TREATMENT

            if (Input.GetMouseButtonDown(0))
            {

                Vector3 touchPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Collider2D btnCollider = sprBtnTakePhoto.GetComponent<Collider2D>();

                if (btnCollider.OverlapPoint(touchPoint))
                {
                    currentGameState = EnumGameState.GAMESTATE_CHECKSNAP;
                }
            }
        }
        else if (currentGameState == EnumGameState.GAMESTATE_CHECKSNAP)
        {
            sprObjCollision.transform.position = sprCameraAim.transform.position;

            if (currentCheckSnapState == EnumGameCheckSnapState.GAMESNAPCHECK_FLASH_FADEIN)
            {
                if (level.levelFailed == false)
                {
                    sprBtnTakePhoto.GetComponent<SpriteRenderer>().color = new Color(sprBtnTakePhoto.GetComponent<SpriteRenderer>().color.r, sprBtnTakePhoto.GetComponent<SpriteRenderer>().color.g, sprBtnTakePhoto.GetComponent<SpriteRenderer>().color.b, 0f);
                    sprTargetHUD.GetComponent<SpriteRenderer>().color = new Color(sprBtnTakePhoto.GetComponent<SpriteRenderer>().color.r, sprBtnTakePhoto.GetComponent<SpriteRenderer>().color.g, sprBtnTakePhoto.GetComponent<SpriteRenderer>().color.b, 0f);
                    txtTarget.color = new Color(txtTarget.color.r, txtTarget.color.g, txtTarget.color.b, 0f);

                    Color sprFlashColor = sprFlash.GetComponent<SpriteRenderer>().color;
                    sprFlashColor.a += 10.0f * Time.deltaTime;
                    sprFlash.GetComponent<SpriteRenderer>().color = sprFlashColor;

                    if (sprFlashColor.a > 1f)
                    {
                        currentCheckSnapState = EnumGameCheckSnapState.GAMESNAPCHECK_FLASH_FADEOUT;
                    }
                }
                else
                {
                    Color colorsprBtnTakePhoto = sprBtnTakePhoto.GetComponent<SpriteRenderer>().color;
                    colorsprBtnTakePhoto.a -= 1.3f * Time.deltaTime;

                    sprBtnTakePhoto.GetComponent<SpriteRenderer>().color = new Color(sprBtnTakePhoto.GetComponent<SpriteRenderer>().color.r, sprBtnTakePhoto.GetComponent<SpriteRenderer>().color.g, sprBtnTakePhoto.GetComponent<SpriteRenderer>().color.b, colorsprBtnTakePhoto.a);
                    sprTargetHUD.GetComponent<SpriteRenderer>().color = new Color(sprBtnTakePhoto.GetComponent<SpriteRenderer>().color.r, sprBtnTakePhoto.GetComponent<SpriteRenderer>().color.g, sprBtnTakePhoto.GetComponent<SpriteRenderer>().color.b, colorsprBtnTakePhoto.a);
                    txtTarget.color = new Color(txtTarget.color.r, txtTarget.color.g, txtTarget.color.b, colorsprBtnTakePhoto.a);

                    if (colorsprBtnTakePhoto.a < 0f)
                    {
                        currentCheckSnapState = EnumGameCheckSnapState.GAMESNAPCHECK_FLASH_FADEOUT;
                    }
                }
            }
            else if (currentCheckSnapState == EnumGameCheckSnapState.GAMESNAPCHECK_FLASH_FADEOUT)
            {
                if (percentCalc != true)
                {
                    percentCalcValue = level.getLevelSuccessPercent();
                    percentCalc = true;
                }

                if (level.levelFailed == false)
                {
                    Color sprFlashColor = sprFlash.GetComponent<SpriteRenderer>().color;
                    sprFlashColor.a -= 1.7f * Time.deltaTime;
                    sprFlash.GetComponent<SpriteRenderer>().color = sprFlashColor;

                    if (sprFlashColor.a < 0f)
                    {
                        timeCountUtil = Time.time;
                        currentCheckSnapState = EnumGameCheckSnapState.GAMESNAPCHECK_DISPLAY_PERCENT;
                    }
                }
                else
                {
                    timeCountUtil = Time.time;
                    currentCheckSnapState = EnumGameCheckSnapState.GAMESNAPCHECK_DISPLAY_PERCENT;
                }
            }
            else if (currentCheckSnapState == EnumGameCheckSnapState.GAMESNAPCHECK_DISPLAY_PERCENT)
            {
                Color sprPrecisionColor = txtPrecision.color;
                sprPrecisionColor.a = 1f;
                txtPrecision.color = sprPrecisionColor;

                Color sprPrecisionValueColor = txtProgressionPercent.color;
                sprPrecisionValueColor.a = 1f;
                txtProgressionPercent.color = sprPrecisionValueColor;

                float timing = Time.time - timeCountUtil;

                if (timing > 0.7f)
                {
                    //timeCountUtil = Time.time;

                    if (percentCalcAux >= percentCalcValue)
                    {
                        currentCheckSnapState = EnumGameCheckSnapState.GAMESNAPCHECK_DISPLAY_VERDICT;
                    }
                    else
                    {
                        percentCalcAux++;
                        txtProgressionPercent.text = percentCalcAux + "%";
                    }
                }
            }
            else if (currentCheckSnapState == EnumGameCheckSnapState.GAMESNAPCHECK_DISPLAY_VERDICT)
            {
                sprCorrect.transform.position = sprCameraAim.transform.position;
                sprWrong.transform.position = sprCameraAim.transform.position;

                if (percentCalcValue >= 70)
                {
                    // SUCCESS

                    if (percentCalcValue >= 90 && percentCalcValue < 100)
                    {
                        txtSuccess.text = "GREAT!";
                    }
                    else if (percentCalcValue == 100)
                    {
                        txtSuccess.text = "PERFECT!!!";
                    }
                    else
                    {
                        txtSuccess.text = "GOOD JOB";
                    }

                    Color sprCorrectColor = sprCorrect.GetComponent<SpriteRenderer>().color;
                    sprCorrectColor.a += 1.4f * Time.deltaTime;
                    sprCorrect.GetComponent<SpriteRenderer>().color = sprCorrectColor;
                    txtSuccess.color = new Color(txtSuccess.color.r, txtSuccess.color.g, txtSuccess.color.b, sprCorrectColor.a);

                    if (sprCorrectColor.a >= 1)
                    {
                        float timing = Time.time - timeCountUtil;

                        if (timing > 1)
                        {
                            currentCheckSnapState = EnumGameCheckSnapState.GAMESNAPCHECK_FADEOFF_VERDICT;
                        }
                    }
                    else
                    {
                        timeCountUtil = Time.time;
                    }
                }
                else
                {
                    if (lives <= 0)
                    {
                        lives = 0;
                        txtSuccess.text = "GAME OVER!";

                        Color sprTxtSuccess = txtSuccess.color;
                        sprTxtSuccess.a += 1.4f * Time.deltaTime;
                        txtSuccess.color = new Color(txtSuccess.color.r, txtSuccess.color.g, txtSuccess.color.b, sprTxtSuccess.a);

                        if (sprTxtSuccess.a >= 1)
                        {
                            float timing = Time.time - timeCountUtil;

                            if (timing > 2)
                            {
                                //GAME OVER 
                                currentGameState = EnumGameState.GAMESTATE_GAMEOVER;
                            }
                        }
                        else
                        {
                            timeCountUtil = Time.time;
                        }
                    }
                    else
                    {
                        Color sprWrongColor = sprWrong.GetComponent<SpriteRenderer>().color;
                        sprWrongColor.a += 1.4f * Time.deltaTime;
                        sprWrong.GetComponent<SpriteRenderer>().color = sprWrongColor;

                        sprBtnTryAgain.GetComponent<SpriteRenderer>().color = new Color(sprBtnTryAgain.GetComponent<SpriteRenderer>().color.r, sprBtnTryAgain.GetComponent<SpriteRenderer>().color.g, sprBtnTryAgain.GetComponent<SpriteRenderer>().color.b, sprWrongColor.a);

                        if (sprBtnTryAgain.GetComponent<SpriteRenderer>().color.a > 1f)
                        {
                            if (Input.GetMouseButtonDown(0))
                            {

                                Vector3 touchPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                                Collider2D btnCollider = sprBtnTryAgain.GetComponent<Collider2D>();

                                if (btnCollider.OverlapPoint(touchPoint))
                                {
                                    lives--;
                                    resetLevel();
                                }
                            }
                        }

                    }
                }
            }
            else if (currentCheckSnapState == EnumGameCheckSnapState.GAMESNAPCHECK_FADEOFF_VERDICT)
            {
                Color txtProgressionPercentColor = txtProgressionPercent.color;
                txtProgressionPercentColor.a -= 1.4f * Time.deltaTime;
                txtProgressionPercent.color = new Color(txtProgressionPercent.color.r, txtProgressionPercent.color.g, txtProgressionPercent.color.b, txtProgressionPercentColor.a);
                txtSuccess.color = new Color(txtSuccess.color.r, txtSuccess.color.g, txtSuccess.color.b, txtProgressionPercentColor.a);
                txtPrecision.color = new Color(txtPrecision.color.r, txtPrecision.color.g, txtPrecision.color.b, txtProgressionPercentColor.a);

                if (txtProgressionPercentColor.a <= 0)
                {
                    currentCheckSnapState = EnumGameCheckSnapState.GAMESNAPCHECK_DISPLAY_SCORE;
                }
            }
            else if (currentCheckSnapState == EnumGameCheckSnapState.GAMESNAPCHECK_DISPLAY_SCORE)
            {
                Color txtLevelCompletedColor = txtLevelCompleted.color;
                if (txtLevelCompletedColor.a < 1f)
                {
                    txtLevelCompletedColor.a += 1.4f * Time.deltaTime;
                    txtLevelCompleted.color = txtLevelCompletedColor;
                    txtScoreDetail.color = new Color(txtScoreDetail.color.r, txtScoreDetail.color.g, txtScoreDetail.color.b, txtLevelCompletedColor.a);
                    scoreGained = score + level.scoreForCompletion;
                }
                else if (txtLevelCompletedColor.a >= 1)
                {
                    float timing = Time.time - timeCountUtil;
                    
                    if (timing > 1)
                    {
                        if (scoreCalculated == false)
                        {
                            txtScoreDetail.text = "+ 10000: LEVEL COMPLETION";

                            score += 200;

                            if (score > scoreGained)
                            {
                                score = scoreGained;
                                bonusScore = (10000 * percentCalcValue) / 100;
                                scoreGained = score + bonusScore;
                                scoreCalculated = true;
                            }

                            txtScore.text = score.ToString("00000000");
                        }
                        else if (scoreCalculated == true && bonusCalculated == false)
                        {
                            txtScoreDetail.text = "+ 10000: LEVEL COMPLETION \n"
                            + "+  " + bonusScore + ": PRECISION BONUS";

                            score += 200;

                            if (score > scoreGained)
                            {
                                score = scoreGained;
                                bonusCalculated = true;
                            }

                            txtScore.text = score.ToString("00000000");
                        }
                        else if (scoreCalculated == true && bonusCalculated == true)
                        {
                            if (currentLevel < 7)
                            {
                                Color sprButtonNextLevelColor = sprBtnNextLevel.GetComponent<SpriteRenderer>().color;
                                if (sprButtonNextLevelColor.a < 1f)
                                {
                                    sprButtonNextLevelColor.a += 1.4f * Time.deltaTime;
                                    sprBtnNextLevel.GetComponent<SpriteRenderer>().color = sprButtonNextLevelColor;
                                }

                                if (Input.GetMouseButtonDown(0))
                                {

                                    Vector3 touchPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                                    Collider2D btnCollider = sprBtnNextLevel.GetComponent<Collider2D>();

                                    if (btnCollider.OverlapPoint(touchPoint))
                                    {
                                        currentCheckSnapState = EnumGameCheckSnapState.GAMESNAPCHECK_NEXTLEVEL;
                                    }
                                }
                            }
                            else
                            {
                                Color txtYouWonColor = txtYouWon.color;

                                txtYouWonColor.a += 0.9f;
                                txtYouWon.color = new Color(txtYouWon.color.r, txtYouWon.color.g, txtYouWon.color.b, txtYouWonColor.a);

                                Debug.Log(txtYouWonColor.a);

                                if (txtYouWonColor.a > 1f)
                                {
                                    if(Time.time - timeCountUtil >= 2)
                                    {
                                        Color sprShadowRectColor = sprShadowRect.GetComponent<SpriteRenderer>().color;
                                        sprShadowRectColor.a += 1.4f * Time.deltaTime;

                                        if (sprShadowRectColor.a > 0.89f)
                                        {
                                            currentGameState = EnumGameState.GAMESTATE_SENDSCORE;
                                            processBestMetrics();
                                        }

                                        sprShadowRect.GetComponent<SpriteRenderer>().color = sprShadowRectColor;
                                    }
                                }
                                else
                                {
                                    timeCountUtil = Time.time;
                                }                                
                            }
                        }
                    }
                }
                else
                {
                    timeCountUtil = Time.time;
                }
            }
            else if (currentCheckSnapState == EnumGameCheckSnapState.GAMESNAPCHECK_NEXTLEVEL)
            {
                Color sprShadowRectColor = sprShadowRect.GetComponent<SpriteRenderer>().color;
                sprShadowRectColor.a += 1.4f * Time.deltaTime;

                Color sprColorAuxText = txtLevelCompleted.color;
                sprColorAuxText.a -= 1.4f * Time.deltaTime;

                Color sprColorAuxSprite = sprBtnNextLevel.GetComponent<SpriteRenderer>().color;
                sprColorAuxSprite.a -= 1.4f * Time.deltaTime;

                if (sprShadowRectColor.a > 1f && sprColorAuxText.a < 0f && sprColorAuxSprite.a < 0f)
                {
                    currentLevel++;
                    initLevel();
                }

                sprShadowRect.GetComponent<SpriteRenderer>().color = sprShadowRectColor;
                txtLevelCompleted.color = sprColorAuxText;
                txtScoreDetail.color = sprColorAuxText;
                sprBtnNextLevel.GetComponent<SpriteRenderer>().color = sprColorAuxSprite;
            }
        }
        else if (currentGameState == EnumGameState.GAMESTATE_GAMEOVER)
        {
            Color sprShadowRectColor = sprShadowRect.GetComponent<SpriteRenderer>().color;
            sprShadowRectColor.a += 1.4f * Time.deltaTime;

            if (sprShadowRectColor.a > 0.89f)
            {
                currentGameState = EnumGameState.GAMESTATE_SENDSCORE;
                processBestMetrics();
            }

            sprShadowRect.GetComponent<SpriteRenderer>().color = sprShadowRectColor;
        }
        else if(currentGameState == EnumGameState.GAMESTATE_MENU)
        {
            if (currentMenuState == EnumGameMenuState.GAMEMENU_STATE_FADEIN)
            {
                // SHADOW RECT

                Color shadowRectColor = sprShadowRect.GetComponent<SpriteRenderer>().color;

                if (shadowRectColor.a < 0.7f)
                {
                    shadowRectColor.a += 2f * Time.deltaTime;
                    sprShadowRect.GetComponent<SpriteRenderer>().color = shadowRectColor;
                }

                // MENU 

                Color txtGamePausedColor = txtGamePaused.color;
                txtGamePausedColor.a += 2f * Time.deltaTime;
                txtGamePaused.color = txtGamePausedColor;
                sprMnuOptions.GetComponent<SpriteRenderer>().color = new Color(sprMnuOptions.GetComponent<SpriteRenderer>().color.r, sprMnuOptions.GetComponent<SpriteRenderer>().color.g, sprMnuOptions.GetComponent<SpriteRenderer>().color.b, txtGamePausedColor.a);

                if(txtGamePausedColor.a >= 1f)
                {
                    currentMenuState = EnumGameMenuState.GAMEMENU_STATE_IDLE;
                }
            }
            else if (currentMenuState == EnumGameMenuState.GAMEMENU_STATE_IDLE)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    currentMenuState = EnumGameMenuState.GAMEMENU_STATE_FADEOUT_RESUME;
                }

                if (Input.GetMouseButtonDown(0))
                {
                    Vector3 touchPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Collider2D btnCollider = btnMnuResume.GetComponent<Collider2D>();
                    Collider2D btnColliderExit = btnMnuExit.GetComponent<Collider2D>();

                    if (btnCollider.OverlapPoint(touchPoint))
                    {
                        currentMenuState = EnumGameMenuState.GAMEMENU_STATE_FADEOUT_RESUME;
                    }
                    else if(btnColliderExit.OverlapPoint(touchPoint) )
                    {
                        currentMenuState = EnumGameMenuState.GAMEMENU_STATE_CONFIRMEXIT;
                    }
                    else
                    {
                        currentMenuState = EnumGameMenuState.GAMEMENU_STATE_FADEOUT_RESUME;
                    }
                }
            }
            else if (currentMenuState == EnumGameMenuState.GAMEMENU_STATE_CONFIRMEXIT)
            {
                txtConfirm.color = new Color( txtConfirm.color.r, txtConfirm.color.g, txtConfirm.color.b, 1f);
                txtTapToConfirm.color = new Color(txtTapToConfirm.color.r, txtTapToConfirm.color.g, txtTapToConfirm.color.b, 1f);

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    currentMenuState = EnumGameMenuState.GAMEMENU_STATE_FADEOUT_RESUME;
                }

                if (Input.GetMouseButtonDown(0))
                {
                    Vector3 touchPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Collider2D btnCollider = btnMnuResume.GetComponent<Collider2D>();
                    Collider2D btnColliderExit = btnMnuExit.GetComponent<Collider2D>();

                    if (btnCollider.OverlapPoint(touchPoint))
                    {
                        currentMenuState = EnumGameMenuState.GAMEMENU_STATE_FADEOUT_RESUME;
                    }
                    else if (btnColliderExit.OverlapPoint(touchPoint))
                    {
                        currentMenuState = EnumGameMenuState.GAMEMENU_STATE_FADEOUT_EXIT;
                    }
                    else
                    {
                        currentMenuState = EnumGameMenuState.GAMEMENU_STATE_FADEOUT_RESUME;
                    }
                }
            }
            else if (currentMenuState == EnumGameMenuState.GAMEMENU_STATE_FADEOUT_RESUME)
            {
                Color shadowRectColor = sprShadowRect.GetComponent<SpriteRenderer>().color;
                shadowRectColor.a -= 2f * Time.deltaTime;
                sprShadowRect.GetComponent<SpriteRenderer>().color = shadowRectColor;

                // MENU 

                Color txtGamePausedColor = txtGamePaused.color;
                txtGamePausedColor.a -= 2.3f * Time.deltaTime;
                txtGamePaused.color = txtGamePausedColor;
                sprMnuOptions.GetComponent<SpriteRenderer>().color = new Color(sprMnuOptions.GetComponent<SpriteRenderer>().color.r, sprMnuOptions.GetComponent<SpriteRenderer>().color.g, sprMnuOptions.GetComponent<SpriteRenderer>().color.b, txtGamePausedColor.a);
                txtConfirm.color = new Color(txtConfirm.color.r, txtConfirm.color.g, txtConfirm.color.b, 0f);
                txtTapToConfirm.color = new Color(txtTapToConfirm.color.r, txtTapToConfirm.color.g, txtTapToConfirm.color.b, 0f);

                if (txtGamePausedColor.a < 0f)
                {
                    currentGameState = lastGameState;
                }
            }
            else if (currentMenuState == EnumGameMenuState.GAMEMENU_STATE_FADEOUT_EXIT)
            {
                Color shadowRectColor = sprShadowRect.GetComponent<SpriteRenderer>().color;
                shadowRectColor.a += 2f * Time.deltaTime;
                sprShadowRect.GetComponent<SpriteRenderer>().color = shadowRectColor;

                // MENU 

                Color txtGamePausedColor = txtGamePaused.color;
                txtGamePausedColor.a -= 2.3f * Time.deltaTime;
                txtGamePaused.color = txtGamePausedColor;
                sprMnuOptions.GetComponent<SpriteRenderer>().color = new Color(sprMnuOptions.GetComponent<SpriteRenderer>().color.r, sprMnuOptions.GetComponent<SpriteRenderer>().color.g, sprMnuOptions.GetComponent<SpriteRenderer>().color.b, txtGamePausedColor.a);
                txtConfirm.color = new Color(txtConfirm.color.r, txtConfirm.color.g, txtConfirm.color.b, txtGamePausedColor.a);
                txtTapToConfirm.color = new Color(txtTapToConfirm.color.r, txtTapToConfirm.color.g, txtTapToConfirm.color.b, txtGamePausedColor.a);

                if (shadowRectColor.a > 1f)
                {
                    if (Time.time - timeCountUtil > 2)
                    {
                        SceneManager.LoadScene(0);
                    }
                }
                else
                {
                    timeCountUtil = Time.time;
                }
            }
        }
    }

    private void processBestMetrics()
    {
        // SCORE
        int highscore = PlayerPrefs.GetInt("HIGH_SCORE");

        if(score > highscore)
        {
            PlayerPrefs.SetInt("HIGH_SCORE", score);
        }

    }

    // == CLASS EVENTS
    // ======================================================================================

    // == GETTERS AND SETTERS
    // ======================================================================================	

    public EnumGameState curState
    {
        get { return currentGameState; }
        set { currentGameState = value; }
    }

    public GameObject target
    {
        get { return sprObjTarget; }
        set { this.sprObjTarget = value; }
    }

    public GameObject collision
    {
        get { return sprObjCollision; }
    }

    public GameObject cameraAim
    {
        get { return sprCameraAim; }
    }

}
