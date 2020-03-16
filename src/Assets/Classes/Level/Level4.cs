/*

	+ =====================================================================
	|                                                            
	|	== CODECASTLE STUDIOS @ 2016                              
	|   == FRAME HUNTER - GAME                                   
	|	====================================                     
	|   ==    FILE: Level4.cs           						 
	|	==    DATE (YYYY-MM-DD): 2017-08-07 | TIME (HH:MM:SS): 14:02:00 PM              
	|   ==   SINCE: 0.0.1a				                         
	|	==  AUTHOR: Henrique Fantini                             
	|   == CONTACT: codecraft@outlook.com                        
	|   ====================================                     
	|   ==                                                       

	
*/

// == IMPORT LIBS
// ==========================================================================================

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// == CLASS
// ==========================================================================================

public class Level4 : Level
{

    // == DECLARATION
    // ======================================================================================

    // == CONST =============================================================================

    private const float MIN_CAMERAAIM_POS_X = -7;
    private const float MAX_CAMERAAIM_POS_X = 7;

    // == VAR ===============================================================================

    private GameObject cameraAim;
    private Vector3 movementDirection;

    // == CLASS CONSTRUCTOR(S)
    // ======================================================================================

    // == CLASS METHODS
    // ======================================================================================

    protected override void loadLevel()
    {
        _levelNumber = 4;
        _targetSprite = Resources.Load<Sprite>("Sprite/sprLevel_4_target");
        _levelBackground = Resources.Load<Sprite>("Sprite/sprLevel_4");
        _startPosition = new Vector3(10f, 1.2f, sprTarget.transform.position.z);
    }

    public override void resetLevel()
    {
        base.resetLevel();
    }

    public override void update()
    {
        if (Camera.main.WorldToScreenPoint(sprTarget.transform.position + sprTarget.GetComponent<SpriteRenderer>().bounds.size).x > 0)
        {
            sprTarget.transform.Translate((Vector3.left * 15) * Time.deltaTime, Space.World);
            sprTarget.transform.Rotate( Vector3.forward * (400 * Time.deltaTime) );
        }
        else
        {
            _levelFailed = true;
            script.curState = EnumGameState.GAMESTATE_CHECKSNAP;
        }
    }

    // == CLASS EVENTS
    // ======================================================================================

    // == GETTERS AND SETTERS
    // ======================================================================================	
}
