/*

	+ =====================================================================
	|                                                            
	|	== CODECASTLE STUDIOS @ 2016                              
	|   == FRAME HUNTER - GAME                                   
	|	====================================                     
	|   ==    FILE: Level5.cs           						 
	|	==    DATE (YYYY-MM-DD): 2017-08-15 | TIME (HH:MM:SS): 08:53:00 PM              
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

public class Level2 : Level
{

    // == DECLARATION
    // ======================================================================================

    // == CONST =============================================================================

    private const float MIN_CAMERAAIM_POS_X = -7;
    private const float MAX_CAMERAAIM_POS_X = 7;
    private const float VEL_CAMERAAIM_X = 15f;

    // == VAR ===============================================================================

    private GameObject cameraAim;
    private Vector3 movementDirection;
    private Vector3 movementDirectionAIM;

    // == CLASS CONSTRUCTOR(S)
    // ======================================================================================

    // == CLASS METHODS
    // ======================================================================================

    protected override void loadLevel()
    {
        _levelNumber = 2;
        _targetSprite = Resources.Load<Sprite>("Sprite/sprLevel_5_target");
        _levelBackground = Resources.Load<Sprite>("Sprite/sprLevel_5");
        _startPosition = new Vector3(-10f, 1.3f, sprTarget.transform.position.z);
        movementDirection = Vector3.right;
        cameraAim = script.cameraAim;

        int randomNumber = UnityEngine.Random.Range(0, 2);

        if (randomNumber == 0)
        {
            movementDirectionAIM = Vector3.left;
        }
        else
        {
            movementDirectionAIM = Vector3.right;
        }
    }

    public override void resetLevel()
    {
        base.resetLevel();
    }

    public override void update()
    {
        // == AIM MOVEMENT

        cameraAim.transform.Translate( (movementDirectionAIM * VEL_CAMERAAIM_X) * Time.deltaTime, Space.World);

        if (cameraAim.transform.position.x < MIN_CAMERAAIM_POS_X)
        {
            movementDirectionAIM = Vector3.right;
        }
        else if (cameraAim.transform.position.x > MAX_CAMERAAIM_POS_X)
        {
            movementDirectionAIM = Vector3.left;
        }

        // == BOAT MOVEMENT

        if (movementDirection == Vector3.right)
        {
            if (Camera.main.WorldToScreenPoint(sprTarget.transform.position - (sprTarget.GetComponent<SpriteRenderer>().bounds.size / 2 ) ).x < Camera.main.pixelWidth)
            {
                sprTarget.GetComponent<SpriteRenderer>().flipX = false;
            }
            else
            {
                movementDirection = Vector3.left;
            }
        }
        else if(movementDirection == Vector3.left)
        {
            if (Camera.main.WorldToScreenPoint(sprTarget.transform.position + sprTarget.GetComponent<SpriteRenderer>().bounds.size).x > 0)
            {
                sprTarget.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                movementDirection = Vector3.right;
            }
        }

        sprTarget.transform.Translate((movementDirection * 5) * Time.deltaTime, Space.World);
    }

    // == CLASS EVENTS
    // ======================================================================================

    // == GETTERS AND SETTERS
    // ======================================================================================	
}
