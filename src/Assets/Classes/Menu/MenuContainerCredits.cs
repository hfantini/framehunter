
/*

    + =====================================================================
    |                                                            
    |   == CODECASTLE STUDIOS @ 2016                              
    |   == FRAME HUNTER - GAME                                   
    |   ====================================                     
    |   ==    FILE: MenuContainerCredits.cs                                 
    |   ==    DATE (YYYY-MM-DD): 2017-07-18 | TIME (HH:MM:SS): 10:00:00 AM              
    |   ==   SINCE: 0.0.1a                                       
    |   ==  AUTHOR: Henrique Fantini                             
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

public class MenuContainerCredits : MenuContainer
{
    // == DECLARATION
    // ======================================================================================

    // == CONST =============================================================================

    // == VAR ===============================================================================

    // == STATES

    // == SPRITES
    private GameObject btnBackCredits;
    private GameObject sprCredits;

    // == UI
    private Text txtBackCredits;

    // == VALUES
    private Vector3 markerOriginalPosAux;

    // == CLASS CONSTRUCTOR(S)
    // ======================================================================================

    public MenuContainerCredits(MonoBehaviour parent) : base(parent)
    {

    }

    // == CLASS METHODS
    // ======================================================================================

    public override void Start()
    {
        _containerContents = GameObject.Find("mnuContainerCredits");
        enable(false);

        // == INITIALIAZING VALUES: STATES

        _menuContainerState = EnumMenuContainerState.MENUCONTAINERSTATE_TRANSITON_INACTIVE;

        // == INITIALIAZING VALUES: SPRITES

        sprCredits = _containerContents.transform.Find("sprCredits").gameObject;
        btnBackCredits = _containerContents.transform.Find("btnBackCredits").gameObject;

        // == INITIALIAZING VALUES: UI

        txtBackCredits = btnBackCredits.transform.Find("txtBackCredits").gameObject.GetComponent<Text>();

        // == INITIALIAZING VALUES

        markerOriginalPosAux = btnBackCredits.transform.Find("sprMarkBackCredits").gameObject.transform.position;
    }

    public override void Update()
    {
        enable(true);

        if (_menuContainerState == EnumMenuContainerState.MENUCONTAINERSTATE_TRANSITON_IN)
        {
            Color colorAuxText = txtBackCredits.color;
            colorAuxText.a += 3f * Time.deltaTime;

            Color colorMarkerAux = btnBackCredits.transform.Find("sprMarkBackCredits").gameObject.GetComponent<SpriteRenderer>().color;
            colorMarkerAux.a += 3f * Time.deltaTime;

            // SHOW: btnNewGame
            txtBackCredits.color = colorAuxText;
            btnBackCredits.transform.Find("sprMarkBackCredits").gameObject.GetComponent<SpriteRenderer>().color = colorMarkerAux;
            sprCredits.GetComponent<SpriteRenderer>().color = new Color(sprCredits.GetComponent<SpriteRenderer>().color.r, sprCredits.GetComponent<SpriteRenderer>().color.g, sprCredits.GetComponent<SpriteRenderer>().color.b, colorMarkerAux.a);

            if (colorAuxText.a > 1f)
            {
                _menuContainerState = EnumMenuContainerState.MENUCONTAINERSTATE_TRANSITON_IDLE;
            }

        }
        else if (_menuContainerState == EnumMenuContainerState.MENUCONTAINERSTATE_TRANSITON_IDLE)
        {
            var pointerPos = Input.mousePosition;

            Vector3 touchPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // == PROCESSING EVENTS

            // MOUSE OVER: btnBackCredits

            Collider2D btnCollider = btnBackCredits.transform.Find("btnBackCreditsHit").GetComponent<Collider2D>();
            if (btnCollider.OverlapPoint(touchPoint))
            {
                OnOptionMouseOver(txtBackCredits, btnBackCredits.transform.Find("sprMarkBackCredits").gameObject);

                // MOUSE RELEASE: btnBackCredits
                if ( Input.GetMouseButtonUp(0) )
                {
                    resetButton(txtBackCredits, btnBackCredits.transform.Find("sprMarkBackCredits").gameObject);
                    ( (MenuScript)_parent ).creditsOptionSelected = EnumMenuCreditsOptionSelected.MENUCONTAINER_CREDITS_OPT_EXIT;
                }
            }
            else
            {
                OnOptionMouseOut(txtBackCredits, btnBackCredits.transform.Find("sprMarkBackCredits").gameObject);
            }
        }
        else if (_menuContainerState == EnumMenuContainerState.MENUCONTAINERSTATE_TRANSITON_OUT)
        {
            Color colorAuxText = txtBackCredits.color;
            colorAuxText.a -= 3f * Time.deltaTime;

            Color colorMarkerAux = btnBackCredits.transform.Find("sprMarkBackCredits").gameObject.GetComponent<SpriteRenderer>().color;
            colorMarkerAux.a -= 3f * Time.deltaTime;

            // HIDE: btnNewGame
            txtBackCredits.color = colorAuxText;
            btnBackCredits.transform.Find("sprMarkBackCredits").gameObject.GetComponent<SpriteRenderer>().color = colorMarkerAux;
            sprCredits.GetComponent<SpriteRenderer>().color = new Color(sprCredits.GetComponent<SpriteRenderer>().color.r, sprCredits.GetComponent<SpriteRenderer>().color.g, sprCredits.GetComponent<SpriteRenderer>().color.b, colorMarkerAux.a);

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
        text.color = new Color(0f, 0f, 0f, 1f);

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
        marker.transform.position = markerOriginalPosAux;
    }

    // == GETTERS AND SETTERS
    // ======================================================================================   

}
