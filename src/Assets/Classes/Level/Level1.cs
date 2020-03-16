
/*

	+ =====================================================================
	|                                                            
	|	== CODECASTLE STUDIOS @ 2016                              
	|   == FRAME HUNTER - GAME                                   
	|	====================================                     
	|   ==    FILE: Level1.cs           						 
	|	==    DATE (YYYY-MM-DD): 2017-06-12 | TIME (HH:MM:SS): 09:33:00 AM              
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

public class Level1 : Level
{

    // == DECLARATION
    // ======================================================================================

    // == CONST =============================================================================

    // == VAR ===============================================================================

    // == CLASS CONSTRUCTOR(S)
    // ======================================================================================

    // == CLASS METHODS
    // ======================================================================================

    protected override void loadLevel()
    {
        _levelNumber = 1;
        _targetSprite = Resources.Load<Sprite>("Sprite/sprLevel_1_target");
        _levelBackground = Resources.Load<Sprite>("Sprite/sprLevel_1");
        _startPosition = new Vector3(10.4f, 1.3f, sprTarget.transform.position.z);
        _scoreForCompletion = 10000;
    }

    public override void update()
    {
        if ( Camera.main.WorldToScreenPoint( sprTarget.transform.position + sprTarget.GetComponent<SpriteRenderer>().bounds.size).x > 0)
        {
            sprTarget.transform.Translate( (Vector3.left * 8) * Time.deltaTime, Space.World);
        }
        else
        {
            sprTarget.transform.position = startPosition;
        }
    }
	
	// == CLASS EVENTS
	// ======================================================================================
	
	// == GETTERS AND SETTERS
	// ======================================================================================	
}
