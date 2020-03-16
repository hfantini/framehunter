
/*

	+ =====================================================================
	|                                                            
	|	== CODECITADEL STUDIOS @ 2016                              
	|   == FRAME HUNTER - GAME                                   
	|	====================================                     
	|   ==    FILE: ObjectLeaderboardsEntry.cs           						 
	|	==    DATE (YYYY-MM-DD): 2017-06-12 | TIME (HH:MM:SS): 09:33:00 AM              
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

public class ObjectLeaderboardsEntry : MonoBehaviour
{
    // == DECLARATION
    // ======================================================================================

    // == CONST =============================================================================

    // == VAR ===============================================================================

    // == SPRITES

    private GameObject sprBackground;

    // == UI

    // == VALUES

    // == CLASS CONSTRUCTOR(S)
    // ======================================================================================

    // == CLASS METHODS
    // ======================================================================================

    public void Start()
    {
        // == INITIALIZING VALUES: SPRITES
        sprBackground = new GameObject();

        sprBackground.transform.SetParent( GameObject.Find("mnuContainerLeaderboards/pnlScroll/pnlScrollableContent").transform );
        sprBackground.AddComponent<CanvasRenderer>();

        Image i = sprBackground.AddComponent<Image>();
        i.color = Color.black;

        sprBackground.AddComponent<RectTransform>();
        sprBackground.GetComponent<RectTransform>().anchorMin = new Vector2(0f, 0f);
        sprBackground.GetComponent<RectTransform>().anchorMax = new Vector2(1f, 1f);
        sprBackground.GetComponent<RectTransform>().pivot = new Vector2(0f, 1f);
        sprBackground.GetComponent<RectTransform>().offsetMin = new Vector2(0f, 0f);
        sprBackground.GetComponent<RectTransform>().offsetMax = new Vector2(100f, 100f);
    }

    public void Update()
    {
        
    }

    // == CLASS EVENTS
    // ======================================================================================

    // == GETTERS AND SETTERS
    // ======================================================================================	
}
