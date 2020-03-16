/*

	+ =====================================================================
	|                                                            
	|	== CODECASTLE STUDIOS @ 2016                              
	|   == FRAME HUNTER - GAME                                   
	|	====================================                     
	|   ==    FILE: Level3.cs           						 
	|	==    DATE (YYYY-MM-DD): 2017-08-07 | TIME (HH:MM:SS): 08:51:00 AM              
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

public class Level3 : Level
{

    // == DECLARATION
    // ======================================================================================

    // == CONST =============================================================================

    private const float MIN_CAMERAAIM_POS_X = -7;
    private const float MAX_CAMERAAIM_POS_X = 7;
    private const float VEL_CAMERAAIM_X = 30f;

    // == VAR ===============================================================================

    private GameObject cameraAim;
    private Vector3 movementDirection;

    // == CLASS CONSTRUCTOR(S)
    // ======================================================================================

    // == CLASS METHODS
    // ======================================================================================

    protected override void loadLevel()
    {
        _levelNumber = 3;
        _targetSprite = Resources.Load<Sprite>("Sprite/sprLevel_3_target");
        _levelBackground = Resources.Load<Sprite>("Sprite/sprLevel_3");
        _startPosition = new Vector3(2.2f, 1.25f, sprTarget.transform.position.z);
        cameraAim = script.cameraAim;

        int randomNumber = UnityEngine.Random.Range(0, 2);

        if(randomNumber == 0)
        {
            movementDirection = Vector3.left;
        }
        else
        {
            movementDirection = Vector3.right;
        }

    }

    public override void resetLevel()
    {
        base.resetLevel();
    }

    public override void update()
    {
        cameraAim.transform.Translate( (movementDirection * VEL_CAMERAAIM_X) * Time.deltaTime, Space.World);

        if (cameraAim.transform.position.x < MIN_CAMERAAIM_POS_X)
        {
            movementDirection = Vector3.right;
        }
        else if(cameraAim.transform.position.x > MAX_CAMERAAIM_POS_X)
        {
            movementDirection = Vector3.left;
        }
    }

    // == CLASS EVENTS
    // ======================================================================================

    // == GETTERS AND SETTERS
    // ======================================================================================	
}
