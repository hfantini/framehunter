/*

	+ =====================================================================
	|                                                            
	|	== CODECASTLE STUDIOS @ 2016                              
	|   == FRAME HUNTER - GAME                                   
	|	====================================                     
	|   ==    FILE: Level2.cs           						 
	|	==    DATE (YYYY-MM-DD): 2017-08-02 | TIME (HH:MM:SS): 10:22:00 AM              
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

public class Level6 : Level
{

    // == DECLARATION
    // ======================================================================================

    // == CONST =============================================================================

    // == VAR ===============================================================================

    private bool trolada;
    private int velX;

    // == CLASS CONSTRUCTOR(S)
    // ======================================================================================

    // == CLASS METHODS
    // ======================================================================================

    protected override void loadLevel()
    {
        _levelNumber = 6;
        _targetSprite = Resources.Load<Sprite>("Sprite/sprLevel_2_target");
        _levelBackground = Resources.Load<Sprite>("Sprite/sprLevel_2");
        _startPosition = new Vector3(-10f, 1.3f, sprTarget.transform.position.z);
        trolada = true;
        velX = 5;
    }

    public override void resetLevel()
    {
        base.resetLevel();

        trolada = true;
        velX = 5;
    }

    public override void update()
    {
        if (Camera.main.WorldToScreenPoint(sprTarget.transform.position - sprTarget.GetComponent<SpriteRenderer>().bounds.size).x < Camera.main.pixelWidth )
        {
            sprTarget.transform.Translate((Vector3.right * velX) * Time.deltaTime, Space.World);

            if(trolada == true)
            {
                if (sprTarget.transform.position.x > -1.5f)
                {
                    velX = 10;
                    trolada = false;
                }
            }
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
