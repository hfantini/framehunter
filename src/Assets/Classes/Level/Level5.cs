/*

	+ =====================================================================
	|                                                            
	|	== CODECASTLE STUDIOS @ 2016                              
	|   == FRAME HUNTER - GAME                                   
	|	====================================                     
	|   ==    FILE: Level5.cs           						 
	|	==    DATE (YYYY-MM-DD): 2017-08-15 | TIME (HH:MM:SS): 10:08:00 PM              
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

public class Level5 : Level
{

    // == DECLARATION
    // ======================================================================================

    // == CONST =============================================================================

    private const float MIN_CAMERAAIM_POS_X = -1;
    private const float MAX_CAMERAAIM_POS_X = 1;
    private const float VEL_CAMERAAIM_X = 5f;

    // == VAR ===============================================================================

    private GameObject cameraAim;
    private Vector3 movementDirection;
    private Vector3 movementDirectionAIM;
    private Vector3 originalAimPosition;
    private Boolean calcFlip;
    private float flipPointX;

    // == CLASS CONSTRUCTOR(S)
    // ======================================================================================

    // == CLASS METHODS
    // ======================================================================================

    protected override void loadLevel()
    {
        _levelNumber = 5;
        _targetSprite = Resources.Load<Sprite>("Sprite/sprLevel_6_target");
        _levelBackground = Resources.Load<Sprite>("Sprite/sprLevel_6");
        _startPosition = new Vector3(-10f, 0.9f, sprTarget.transform.position.z);
        movementDirection = Vector3.right;
        cameraAim = script.cameraAim;
        originalAimPosition = cameraAim.transform.position;

        calcFlip = false;
        calcRandomAimDirection();
    }

    public override void resetLevel()
    {
        base.resetLevel();

        calcFlip = false;
        calcRandomAimDirection();
    }

    public override void update()
    {
        if(calcFlip == false)
        {
            calcFlip = true;
            flipPointX = getRandomFlipPosition();
        }

        // == AIM MOVEMENT

        cameraAim.transform.Translate((movementDirectionAIM * VEL_CAMERAAIM_X) * Time.deltaTime, Space.World);

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
            if (Camera.main.WorldToScreenPoint(sprTarget.transform.position - (sprTarget.GetComponent<SpriteRenderer>().bounds.size / 2) ).x < flipPointX)
            {
                sprTarget.GetComponent<SpriteRenderer>().flipX = false;
            }
            else
            {
                movementDirection = Vector3.left;
                calcFlip = false;
            }
        }
        else if (movementDirection == Vector3.left)
        {
            if (Camera.main.WorldToScreenPoint(sprTarget.transform.position + sprTarget.GetComponent<SpriteRenderer>().bounds.size).x > flipPointX)
            {
                sprTarget.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                movementDirection = Vector3.right;
                calcFlip = false;
            }
        }

        sprTarget.transform.Translate((movementDirection * 5) * Time.deltaTime, Space.World);
    }

    private void calcRandomAimDirection()
    {
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

    private float getRandomFlipPosition()
    {
        float retValue = 0;

        if (movementDirection == Vector3.right)
        {
            float value1 = Camera.main.WorldToScreenPoint(originalAimPosition + (cameraAim.GetComponent<SpriteRenderer>().bounds.size / 2 ) ).x ;
            float value2 = Camera.main.pixelWidth;

            retValue = UnityEngine.Random.Range(value1, value2);
        }
        else if (movementDirection == Vector3.left)
        {
            float value1 = 0;
            float value2 = Camera.main.WorldToScreenPoint(originalAimPosition - (cameraAim.GetComponent<SpriteRenderer>().bounds.size / 2) ).x;

            retValue = UnityEngine.Random.Range(value1, value2);
        }

        return retValue;

    }

    // == CLASS EVENTS
    // ======================================================================================

    // == GETTERS AND SETTERS
    // ======================================================================================	
}
