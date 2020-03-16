
/*

	+ =====================================================================
	|                                                            
	|	== CODECASTLE STUDIOS @ 2016                              
	|   == FRAME HUNTER - GAME                                   
	|	====================================                     
	|   ==    FILE: MenuContainerMain.cs           						 
	|	==    DATE (YYYY-MM-DD): 2017-07-14 | TIME (HH:MM:SS): 09:19:00 AM              
	|   ==   SINCE: 0.0.1a				                         
	|	==  AUTHOR: Henrique Fantini                             
	|   == CONTACT: codecraft@outlook.com                        
	|   ====================================                     
	|   ==                                                       

	
*/

// == IMPORT LIBS
// ==========================================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// == CLASS
// ==========================================================================================

public class MenuContainerMain : MenuContainer
{
    // == DECLARATION
    // ======================================================================================

    // == CONST =============================================================================

    // == VAR ===============================================================================

    // == SPRITES
    private GameObject btnNewGame;
    private GameObject btnCredits;
    private GameObject btnExit;

    // == UI
    private Text txtNewGame;
    private Text txtCredits;
    private Text txtExit;

    // == VALUES
    private Vector3 markerOriginalPosAux;
    private bool mouseOptionChanged = true;
 
    // == CLASS CONSTRUCTOR(S)
    // ======================================================================================

    public MenuContainerMain(MonoBehaviour parent) : base(parent)
    {

    }

    // == CLASS METHODS
    // ======================================================================================

    public override void Start()
    {
        _containerContents = GameObject.Find("mnuContainerMainOptions");
        enable(false);

        // == INITIALIAZING VALUES: STATES

        _menuContainerState = EnumMenuContainerState.MENUCONTAINERSTATE_TRANSITON_INACTIVE;

        // == INITIALIAZING VALUES: SPRITES

        btnNewGame = _containerContents.transform.Find("btnNewGame").gameObject;
        txtNewGame = btnNewGame.transform.Find("txtNewGame").gameObject.GetComponent<Text>();
        //txtLeaderboards = btnLeaderboards.transform.Find("txtLeaderboards").gameObject.GetComponent<Text>();

        btnCredits = _containerContents.transform.Find("btnCredits").gameObject;
        txtCredits = btnCredits.transform.Find("txtCredits").gameObject.GetComponent<Text>();

        btnExit = _containerContents.transform.Find("btnExit").gameObject;
        txtExit = btnExit.transform.Find("txtExit").gameObject.GetComponent<Text>();

        // == INITIALIAZING VALUES: UI

        markerOriginalPosAux = btnNewGame.transform.Find("sprMarkNewGame").gameObject.transform.position;

        // == INITIALIAZING VALUES

        mouseOptionChanged = true;

    }

    public override void Update()
    {
        if (_menuContainerState == EnumMenuContainerState.MENUCONTAINERSTATE_TRANSITON_IN)
        {
            Color colorAuxText = txtNewGame.color;
            colorAuxText.a += 3f * Time.deltaTime;

            Color colorMarkerAux = btnNewGame.transform.Find("sprMarkNewGame").gameObject.GetComponent<SpriteRenderer>().color;
            colorMarkerAux.a += 3f * Time.deltaTime;

            // SHOW: btnNewGame
            txtNewGame.color = colorAuxText;
            btnNewGame.transform.Find("sprMarkNewGame").gameObject.GetComponent<SpriteRenderer>().color = colorMarkerAux;

            // SHOW: btnLeaderBoards
            //txtLeaderboards.color = colorAuxText;
            //btnLeaderboards.transform.Find("sprMarkLeader").gameObject.GetComponent<SpriteRenderer>().color = colorMarkerAux;

            // SHOW: btnCredits
            txtCredits.color = colorAuxText;
            btnCredits.transform.Find("sprMarkCredits").gameObject.GetComponent<SpriteRenderer>().color = colorMarkerAux;

            // SHOW: btnExit
            txtExit.color = colorAuxText;
            btnExit.transform.Find("sprMarkExit").gameObject.GetComponent<SpriteRenderer>().color = colorMarkerAux;

            if (colorAuxText.a > 1f)
            {
                _menuContainerState = EnumMenuContainerState.MENUCONTAINERSTATE_TRANSITON_IDLE;
            }
        }
        else if (_menuContainerState == EnumMenuContainerState.MENUCONTAINERSTATE_TRANSITON_IDLE)
        {
            enable(true);

            var pointerPos = Input.mousePosition;

            Vector3 touchPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // == PROCESSING EVENTS

            // MOUSE OVER: btnNewGame

            Collider2D btnCollider = btnNewGame.GetComponent<Collider2D>();

            if (btnCollider.OverlapPoint(touchPoint))
            {

                OnOptionMouseOver(txtNewGame, btnNewGame.transform.Find("sprMarkNewGame").gameObject);

				// MOUSE RELEASE: btnNewGame
				if ( Input.GetMouseButtonUp(0) )
				{
                    resetButton(txtNewGame, btnNewGame.transform.Find("sprMarkNewGame").gameObject);
                    ( (MenuScript)_parent ).optionSelected = EnumMenuOptionSelected.MENUCONTAINER_MAIN_OPT_NEWGAME;
				}
            }
            else
            {
                OnOptionMouseOut(txtNewGame, btnNewGame.transform.Find("sprMarkNewGame").gameObject);
            }

            // MOUSE OVER: btnLeaderboards

            /*
            btnCollider = btnLeaderboards.transform.Find("btnLeaderboardsHit").GetComponent<Collider2D>();
            if (btnCollider.OverlapPoint(touchPoint))
            {
                if (!((MenuScript)_parent).menuMouseOverAudioSource.isPlaying)
                {
                    ((MenuScript)_parent).menuMouseOverAudioSource.Play();
                }

                OnOptionMouseOver(txtLeaderboards, btnLeaderboards.transform.Find("sprMarkLeader").gameObject);

                // MOUSE RELEASE: btnCredits
                if (Input.GetMouseButtonUp(0))
                {
                    resetButton(txtLeaderboards, btnLeaderboards.transform.Find("sprMarkLeader").gameObject);
                    ((MenuScript)_parent).optionSelected = EnumMenuOptionSelected.MENUCONTAINER_MAIN_OPT_LEADERBOARDS;
                }
            }
            else
            {
                OnOptionMouseOut(txtLeaderboards, btnLeaderboards.transform.Find("sprMarkLeader").gameObject);
            }*/

            // MOUSE OVER: btnCredits

            btnCollider = btnCredits.transform.Find("btnCreditsHit").GetComponent<Collider2D>();
            if (btnCollider.OverlapPoint(touchPoint))
            {

                OnOptionMouseOver(txtCredits, btnCredits.transform.Find("sprMarkCredits").gameObject);

                // MOUSE RELEASE: btnCredits
                if ( Input.GetMouseButtonUp(0) )
                {
                    resetButton(txtCredits, btnCredits.transform.Find("sprMarkCredits").gameObject);
                    ( (MenuScript)_parent ).optionSelected = EnumMenuOptionSelected.MENUCONTAINER_MAIN_OPT_CREDITS;
                }
            }
            else
            {
                OnOptionMouseOut(txtCredits, btnCredits.transform.Find("sprMarkCredits").gameObject);
            }

            // MOUSE OVER: btnExit

            btnCollider = btnExit.transform.Find("btnExitHit").GetComponent<Collider2D>();
            if (btnCollider.OverlapPoint(touchPoint))
            {
                OnOptionMouseOver(txtExit, btnExit.transform.Find("sprMarkExit").gameObject);

                // MOUSE RELEASE: btnNewGame
                if ( Input.GetMouseButtonUp(0) )
                {
                    resetButton(txtExit, btnExit.transform.Find("sprMarkExit").gameObject);
                    ( (MenuScript)_parent ).optionSelected = EnumMenuOptionSelected.MENUCONTAINER_MAIN_OPT_EXIT;
                }
            }
            else
            {
                OnOptionMouseOut(txtExit, btnExit.transform.Find("sprMarkExit").gameObject);
            }
        }
        else if (_menuContainerState == EnumMenuContainerState.MENUCONTAINERSTATE_TRANSITON_OUT)
        {
            Color colorAuxText = txtNewGame.color;
            colorAuxText.a -= 3f * Time.deltaTime;

            Color colorMarkerAux = btnNewGame.transform.Find("sprMarkNewGame").gameObject.GetComponent<SpriteRenderer>().color;
            colorMarkerAux.a -= 3f * Time.deltaTime;

            // HIDE: btnNewGame
            txtNewGame.color = colorAuxText;
            btnNewGame.transform.Find("sprMarkNewGame").gameObject.GetComponent<SpriteRenderer>().color = colorMarkerAux;

            // HIDE: btnLeaderBoards
            //txtLeaderboards.color = colorAuxText;
            //btnLeaderboards.transform.Find("sprMarkLeader").gameObject.GetComponent<SpriteRenderer>().color = colorMarkerAux;

            // HIDE: btnCredits
            txtCredits.color = colorAuxText;
            btnCredits.transform.Find("sprMarkCredits").gameObject.GetComponent<SpriteRenderer>().color = colorMarkerAux;

            // HIDE: btnExit
            txtExit.color = colorAuxText;
            btnExit.transform.Find("sprMarkExit").gameObject.GetComponent<SpriteRenderer>().color = colorMarkerAux;

            if (colorAuxText.a < 0f && colorMarkerAux.a < 0f)
            {
                _menuContainerState = EnumMenuContainerState.MENUCONTAINERSTATE_TRANSITON_INACTIVE;
            }
        }
    }
	
	// == CLASS EVENTS
	// ======================================================================================
	
    void OnOptionMouseOver(Text text, GameObject marker)
    {
        // == CHANGING COLOR
        text.color = new Color(77f/255f, 77f/255f, 77f/255f, 1f);

        // == MARKER MOV
        marker.transform.position = Vector3.MoveTowards(marker.transform.position, new Vector3(markerOriginalPosAux.x + 0.2f, marker.transform.position.y, marker.transform.position.z), 2f * Time.deltaTime);
    }

    void OnOptionMouseOut(Text text, GameObject marker)
    {
        // == CHANGING COLOR
        text.color = new Color(1f, 1f, 1f, 1f);

        // == MARKER MOV
        marker.transform.position = Vector3.MoveTowards(marker.transform.position, new Vector3(markerOriginalPosAux.x, marker.transform.position.y, marker.transform.position.z), 2f * Time.deltaTime);
    }

    void resetButton(Text text, GameObject marker)
    {
        // == CHANGING COLOR
        text.color = new Color(1f, 1f, 1f, 1f);

        // == MARKER
        marker.transform.position = new Vector3(markerOriginalPosAux.x, marker.transform.position.y, marker.transform.position.z);
    }

	// == GETTERS AND SETTERS
	// ======================================================================================	

}
