/*

	+ =====================================================================
	|                                                            
	|	== CODECITADEL STUDIOS @ 2016                              
	|   == FRAME HUNTER - GAME                                   
	|	====================================                     
	|   ==    FILE: MenuContainerLeaderboards.cs           						 
	|	==    DATE (YYYY-MM-DD): 2017-06-12 | TIME (HH:MM:SS): 09:33:00 AM              
	|   ==   SINCE: 0.0.1a				                         
	|	==  AUTHOR: Henrique Fantini                             
	|   == CONTACT: codecraft@outlook.com                        
	|   ====================================                     
	|   ==                                                       

	
// == IMPORT LIBS
// ==========================================================================================

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// == CLASS
// ==========================================================================================

public class MenuContainerLeaderboards : MenuContainer
{
    // == DECLARATION
    // ======================================================================================

    // == CONST =============================================================================

    // == VAR ===============================================================================

    private bool isBackOption = false;

    // == CONTROLLERS
    private ControllerLeaderboard controllerLeaderboards;

    // == STATES
    private EnumMenuLeaderboardState _currentState;

    // == SPRITES

    private GameObject sprLeaderLoading;
    private GameObject btnLeaderCancel;
    private GameObject btnLeaderRefresh;
    private GameObject btnLeaderBack;

    // == UI

    private Text txtLoadingData;
    private Text txtLeaderboards;
    private Text txtLastUpdate;
    private Text txtColName;
    private Text txtColScore;
    private Text txtColTime;
    private Text txtColLevel;
    private GameObject pnlScroll;
    private GameObject pnlScrollableContent;
    private GameObject pnlEntryModel;

    // == CLASS CONSTRUCTOR(S)
    // ======================================================================================

    public MenuContainerLeaderboards(MonoBehaviour parent) : base(parent)
    {

    }

    // == CLASS METHODS
    // ======================================================================================

    public override void Start()
    {
        _containerContents = GameObject.Find("mnuContainerLeaderboards");
        enable(false);

        // == INITIALIAZING VALUES: CONTROLLERS
        controllerLeaderboards = new ControllerLeaderboard();

        // == INITIALIAZING VALUES: STATES

        _currentState = EnumMenuLeaderboardState.LEADERBOARDSCONTAINER_STATE_LOADING;
        _menuContainerState = EnumMenuContainerState.MENUCONTAINERSTATE_TRANSITON_INACTIVE;

        // == INITIALIAZING VALUES: SPRITES

        sprLeaderLoading = _containerContents.transform.Find("sprLeaderLoading").gameObject;
        btnLeaderCancel = _containerContents.transform.Find("btnLeaderCancel").gameObject;
        btnLeaderRefresh = _containerContents.transform.Find("btnLeaderRefresh").gameObject;
        btnLeaderBack = _containerContents.transform.Find("btnLeaderBack").gameObject;

        // == INITIALIAZING VALUES: UI

        txtLoadingData = _containerContents.transform.Find("txtLoadingData").gameObject.GetComponent<Text>();
        txtLeaderboards = _containerContents.transform.Find("txtLeaderboards").gameObject.GetComponent<Text>();
        txtLastUpdate = _containerContents.transform.Find("txtLastUpdate").gameObject.GetComponent<Text>();
        txtColName = _containerContents.transform.Find("txtColName").gameObject.GetComponent<Text>();
        txtColScore = _containerContents.transform.Find("txtColScore").gameObject.GetComponent<Text>();
        txtColTime = _containerContents.transform.Find("txtColTime").gameObject.GetComponent<Text>();
        txtColLevel = _containerContents.transform.Find("txtColLevel").gameObject.GetComponent<Text>();
        pnlScroll = _containerContents.transform.Find("pnlScroll").gameObject;
        pnlScrollableContent = pnlScroll.transform.Find("pnlScrollableContent").gameObject;
        pnlEntryModel = pnlScrollableContent.transform.Find("leaderEntryModel").gameObject;

        // == INITIALIAZING VALUES
    }

    public override void Update()
    {
        enable(true);

        if (_menuContainerState == EnumMenuContainerState.MENUCONTAINERSTATE_TRANSITON_IN)
        {
            Color colorAuxText = txtLoadingData.color;
            colorAuxText.a += 3f * Time.deltaTime;

            Color colorAuxSprite = sprLeaderLoading.GetComponent<SpriteRenderer>().color;
            colorAuxSprite.a += 3f * Time.deltaTime;

            if(colorAuxText.a > 1f && colorAuxText.a > 1f)
            {
                _menuContainerState = EnumMenuContainerState.MENUCONTAINERSTATE_TRANSITON_IDLE;
            }

            sprLeaderLoading.transform.Rotate(Vector3.forward, -100f * Time.deltaTime, Space.Self);
            sprLeaderLoading.GetComponent<SpriteRenderer>().color = colorAuxSprite;
            btnLeaderCancel.GetComponent<SpriteRenderer>().color = colorAuxSprite;
            txtLoadingData.color = colorAuxText;
            _currentState = EnumMenuLeaderboardState.LEADERBOARDSCONTAINER_STATE_LOADING;
        }
        else if (_menuContainerState == EnumMenuContainerState.MENUCONTAINERSTATE_TRANSITON_IDLE)
        {
            Vector3 touchPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (_currentState == EnumMenuLeaderboardState.LEADERBOARDSCONTAINER_STATE_LOADING)
            {
                sprLeaderLoading.transform.Rotate(Vector3.forward, -100f * Time.deltaTime, Space.Self);

                // == RETRIEVING INFORMATION

                _parent.StartCoroutine( controllerLeaderboards.getLeaderboards() );

                // == PROCESSING INFORMATION

                if(controllerLeaderboards.currentState == EnumControllerLeaderboardStatus.CONTROLLERSTATE_LEADERBOARD_COMPLETED_SUCCESS)
                {
                    // == UPDATE REFRESH TIME

                    DateTime dateTime = DateTime.Now;
                    txtLastUpdate.text = "LAST UPDATED: " + dateTime.Year.ToString("00") + "-" + dateTime.Month.ToString("00") + "-" + dateTime.Day.ToString("00") + " / " + dateTime.Hour.ToString("00") + ":" + dateTime.Minute.ToString("00") + ":" + dateTime.Second.ToString("00");

                    // == UPDATE DATA

                    for (int count = 0; count < controllerLeaderboards.result.Count; count++)
                    {
                        GameObject entry = GameObject.Instantiate(pnlEntryModel);
                        entry.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, count * -70);
                        entry.transform.SetParent(pnlScrollableContent.transform, false);

                        // == BACKGROUND

                        if (count % 2 != 0)
                        {
                            entry.GetComponent<Image>().color = new Color( (237f/255f), (163f/255f), (42f/255f), 1f );
                        }

                        // == VALUES
                        entry.transform.Find("txtPlayerName").gameObject.GetComponent<Text>().text = (count + 1) + " - " + controllerLeaderboards.result[count].UID_NAME.ToUpper();
                        entry.transform.Find("txtPlayerScore").gameObject.GetComponent<Text>().text = controllerLeaderboards.result[count].SCO_SCORE.ToString("0000000000");
                        entry.transform.Find("txtPlayerLevel").gameObject.GetComponent<Text>().text = controllerLeaderboards.result[count].SCO_LEVEL.ToString("00");
                        entry.transform.Find("txtPlayerTime").gameObject.GetComponent<Text>().text = controllerLeaderboards.result[count].SCO_TIME;
                    }

                    pnlScrollableContent.GetComponent<RectTransform>().position = new Vector2(pnlScrollableContent.GetComponent<RectTransform>().position.x, controllerLeaderboards.result.Count * -70);
                    pnlScrollableContent.GetComponent<RectTransform>().sizeDelta = new Vector2(pnlScrollableContent.GetComponent<RectTransform>().sizeDelta.x, controllerLeaderboards.result.Count * 70);

                    _currentState = EnumMenuLeaderboardState.LEADERBOARDSCONTAINER_STATE_LOADING_SUCCESS;
                }
                else if(controllerLeaderboards.currentState == EnumControllerLeaderboardStatus.CONTROLLERSTATE_LEADERBOARD_COMPLETED_FAIL)
                {
                    _currentState = EnumMenuLeaderboardState.LEADERBOARDSCONTAINER_STATE_LOADING_FAIL;
                }

                // == CANCEL BUTTON EVENT

                if (Input.GetMouseButtonUp(0))
                {
                    Collider2D btnCollider = btnLeaderCancel.GetComponent<Collider2D>();

                    if (btnCollider.OverlapPoint(touchPoint))
                    {
                        _currentState = EnumMenuLeaderboardState.LEADERBOARDSCONTAINER_STATE_LOADING_CANCEL;
                    }
                }
            }
            else if(_currentState == EnumMenuLeaderboardState.LEADERBOARDSCONTAINER_STATE_LOADING_CANCEL)
            {
                _menuContainerState = EnumMenuContainerState.MENUCONTAINERSTATE_TRANSITON_OUT;
            }
            else if(_currentState == EnumMenuLeaderboardState.LEADERBOARDSCONTAINER_STATE_LOADING_SUCCESS)
            {
                Color colorAuxText = txtLoadingData.color;
                colorAuxText.a -= 3f * Time.deltaTime;

                Color colorAuxSprite = sprLeaderLoading.GetComponent<SpriteRenderer>().color;
                colorAuxSprite.a -= 3f * Time.deltaTime;

                if (colorAuxText.a < 0f && colorAuxSprite.a < 0f)
                {
                    _currentState = EnumMenuLeaderboardState.LEADERBOARDSCONTAINER_STATE_SHOW_FADEIN;
                }

                sprLeaderLoading.transform.Rotate(Vector3.forward, -100f * Time.deltaTime, Space.Self);
                sprLeaderLoading.GetComponent<SpriteRenderer>().color = colorAuxSprite;
                btnLeaderCancel.GetComponent<SpriteRenderer>().color = colorAuxSprite;
                txtLoadingData.color = colorAuxText;
            }
            else if (_currentState == EnumMenuLeaderboardState.LEADERBOARDSCONTAINER_STATE_SHOW_FADEIN)
            {
                Color colorAuxText = txtLeaderboards.color;
                colorAuxText.a += 3f * Time.deltaTime;

                Color colorAuxSprite = btnLeaderRefresh.GetComponent<SpriteRenderer>().color;
                colorAuxSprite.a += 3f * Time.deltaTime;

                if (colorAuxText.a > 1f && colorAuxSprite.a > 1f)
                {
                    _currentState = EnumMenuLeaderboardState.LEADERBOARDSCONTAINER_STATE_SHOW_IDLE;
                }

                txtLeaderboards.color = colorAuxText;
                txtLastUpdate.color = colorAuxText;
                txtColName.color = new Color(txtColName.color.r, txtColName.color.g, txtColName.color.b, colorAuxText.a);
                txtColScore.color = new Color(txtColScore.color.r, txtColScore.color.g, txtColScore.color.b, colorAuxText.a);
                txtColTime.color = new Color(txtColTime.color.r, txtColTime.color.g, txtColTime.color.b, colorAuxText.a);
                txtColLevel.color = new Color(txtColLevel.color.r, txtColLevel.color.g, txtColLevel.color.b, colorAuxText.a);
                btnLeaderRefresh.GetComponent<SpriteRenderer>().color = new Color(btnLeaderRefresh.GetComponent<SpriteRenderer>().color.r, btnLeaderRefresh.GetComponent<SpriteRenderer>().color.g, btnLeaderRefresh.GetComponent<SpriteRenderer>().color.b, colorAuxSprite.a);
                btnLeaderBack.GetComponent<SpriteRenderer>().color = new Color(btnLeaderBack.GetComponent<SpriteRenderer>().color.r, btnLeaderBack.GetComponent<SpriteRenderer>().color.g, btnLeaderBack.GetComponent<SpriteRenderer>().color.b, colorAuxSprite.a);
                pnlScroll.GetComponent<CanvasGroup>().alpha = colorAuxText.a;
            }
            else if (_currentState == EnumMenuLeaderboardState.LEADERBOARDSCONTAINER_STATE_SHOW_IDLE)
            {
                // == BUTTON EVENTS

                if (Input.GetMouseButtonDown(0))
                {
                    // BUTTON: BACK

                    Collider2D btnCollider = btnLeaderBack.GetComponent<Collider2D>();

                    if (btnCollider.OverlapPoint(touchPoint))
                    {
                        isBackOption = true;
                        _menuContainerState = EnumMenuContainerState.MENUCONTAINERSTATE_TRANSITON_OUT;
                    }

                    // BUTTON REFRESH

                    btnCollider = btnLeaderRefresh.GetComponent<Collider2D>();

                    if (btnCollider.OverlapPoint(touchPoint))
                    {
                        isBackOption = false;
                        _menuContainerState = EnumMenuContainerState.MENUCONTAINERSTATE_TRANSITON_OUT;
                    }
                }
            }
        }
        else if (_menuContainerState == EnumMenuContainerState.MENUCONTAINERSTATE_TRANSITON_OUT)
        {
            if (_currentState == EnumMenuLeaderboardState.LEADERBOARDSCONTAINER_STATE_LOADING)
            {
                Color colorAuxText = txtLoadingData.color;
                colorAuxText.a -= 3f * Time.deltaTime;

                Color colorAuxSprite = sprLeaderLoading.GetComponent<SpriteRenderer>().color;
                colorAuxSprite.a -= 3f * Time.deltaTime;

                if (colorAuxText.a < 0f && colorAuxText.a < 0f)
                {
                    _menuContainerState = EnumMenuContainerState.MENUCONTAINERSTATE_TRANSITON_INACTIVE;
                }

                sprLeaderLoading.transform.Rotate(Vector3.forward, -100f * Time.deltaTime, Space.Self);
                sprLeaderLoading.GetComponent<SpriteRenderer>().color = colorAuxSprite;
                btnLeaderCancel.GetComponent<SpriteRenderer>().color = colorAuxSprite;
                txtLoadingData.color = colorAuxText;
            }
            else if(_currentState == EnumMenuLeaderboardState.LEADERBOARDSCONTAINER_STATE_SHOW_IDLE)
            {
                Color colorAuxText = txtLeaderboards.color;
                colorAuxText.a -= 3f * Time.deltaTime;

                Color colorAuxSprite = btnLeaderRefresh.GetComponent<SpriteRenderer>().color;
                colorAuxSprite.a -= 3f * Time.deltaTime;

                if (colorAuxText.a < 0f && colorAuxSprite.a < 0f)
                {
                    foreach(Transform child in pnlScrollableContent.transform)
                    {
                        if (child.name != "leaderEntryModel")
                        {
                            GameObject.Destroy(child.gameObject);
                        }
                    }

                    if (isBackOption == true)
                    {
                        _menuContainerState = EnumMenuContainerState.MENUCONTAINERSTATE_TRANSITON_INACTIVE;
                    }
                    else if(isBackOption == false)
                    {
                        _menuContainerState = EnumMenuContainerState.MENUCONTAINERSTATE_TRANSITON_IN;
                    }

                    controllerLeaderboards.resetState();
                }

                txtLeaderboards.color = colorAuxText;
                txtLastUpdate.color = colorAuxText;
                txtColName.color = new Color(txtColName.color.r, txtColName.color.g, txtColName.color.b, colorAuxText.a);
                txtColScore.color = new Color(txtColScore.color.r, txtColScore.color.g, txtColScore.color.b, colorAuxText.a);
                txtColTime.color = new Color(txtColTime.color.r, txtColTime.color.g, txtColTime.color.b, colorAuxText.a);
                txtColLevel.color = new Color(txtColLevel.color.r, txtColLevel.color.g, txtColLevel.color.b, colorAuxText.a);
                btnLeaderRefresh.GetComponent<SpriteRenderer>().color = new Color(btnLeaderRefresh.GetComponent<SpriteRenderer>().color.r, btnLeaderRefresh.GetComponent<SpriteRenderer>().color.g, btnLeaderRefresh.GetComponent<SpriteRenderer>().color.b, colorAuxSprite.a);
                btnLeaderBack.GetComponent<SpriteRenderer>().color = new Color(btnLeaderBack.GetComponent<SpriteRenderer>().color.r, btnLeaderBack.GetComponent<SpriteRenderer>().color.g, btnLeaderBack.GetComponent<SpriteRenderer>().color.b, colorAuxSprite.a);
                pnlScroll.GetComponent<CanvasGroup>().alpha = colorAuxText.a;
            }
        }

         

        // == CLASS EVENTS
        // ======================================================================================

        // == GETTERS AND SETTERS
        // ======================================================================================	
    }
}
*/
