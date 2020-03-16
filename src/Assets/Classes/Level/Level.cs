
/*

	+ =====================================================================
	|                                                            
	|	== CODECASTLE STUDIOS @ 2016                              
	|   == FRAME HUNTER - GAME                                   
	|	====================================                     
	|   ==    FILE: Level.cs           						 
	|	==    DATE (YYYY-MM-DD): 2017-06-19 | TIME (HH:MM:SS): 14:49:00 AM              
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

// == CLASS
// ==========================================================================================

public abstract class Level
{
    // == DECLARATION
    // ======================================================================================

    // == CONST =============================================================================

    // == VAR ===============================================================================

    protected GameplayScript script;
    protected int _levelNumber;
    protected GameObject sprTarget;
    protected GameObject sprCollision;
    protected int delayReappearanceMillis;
    protected Vector3 _startPosition;
    protected Sprite _targetSprite;
    protected Sprite _levelBackground;
    protected int _scoreForCompletion = 10000;
    protected bool _levelFailed = false;

    // == CLASS CONSTRUCTOR(S)
    // ======================================================================================

    // == CLASS METHODS
    // ======================================================================================

    protected abstract void loadLevel();

	public void loadContent(GameplayScript script)
    {
        // GETTING SPRITE REFERENCES
        this.script = script;
        this.sprTarget = script.target;
        this.sprCollision = script.collision;

        loadLevel();
    }

    public virtual void update()
    {

    }

    public virtual void resetLevel()
    {
        _levelFailed = false;
    }
	
    public virtual int getLevelSuccessPercent()
    {
        int retValue = 0;

        // CALCULATING SUCCESS PERCENTAGE
        sprTarget.transform.rotation = Quaternion.Euler(0, 0, 0);
        Vector3 targetPos  = sprTarget.transform.position;
        Bounds targetBounds = sprTarget.GetComponent<Renderer>().bounds;
        Vector3 aimPos  = sprCollision.transform.position;
        Bounds aimBounds = sprCollision.GetComponent<Renderer>().bounds;

        Vector2 aimRectXW = new Vector2( aimPos.x - aimBounds.extents.x, aimPos.x + aimBounds.extents.x );
        Vector2 aimTargetXW = new Vector2( targetPos.x - targetBounds.extents.x, targetPos.x + targetBounds.extents.x  );

        if (aimTargetXW.y < aimRectXW.x)
        {
            
        }
        else if (aimTargetXW.x < aimRectXW.y)
        {
            //CALCULATING THE PERCENT
            if(aimTargetXW.x >= aimRectXW.x && aimTargetXW.y <= aimRectXW.y )
            {
                retValue = 100;
            }
            else
            {
                float areaInside = 0;

                if(aimTargetXW.x < aimRectXW.x)
                {
                    float areaOutside = aimRectXW.x - aimTargetXW.x;
                    areaInside = targetBounds.size.x - areaOutside;
                }
                else if(aimTargetXW.y > aimRectXW.y)
                {
                    float areaOutside = aimTargetXW.y - aimRectXW.y;
                    areaInside = targetBounds.size.x - areaOutside;
                }

                retValue = (int) Mathf.Floor( (areaInside * 100) / targetBounds.size.x );
            }
        }
        else
        {

        }

        return retValue;
    }

	// == CLASS EVENTS
	// ======================================================================================
	
	// == GETTERS AND SETTERS
	// ======================================================================================	

    public int levelNumber
    {
        get
        {
            return this._levelNumber;
        }

        set
        {
            this._levelNumber = value;
        }
    }

    public int scoreForCompletion
    {
        get
        {
            return this._scoreForCompletion;
        }

        set
        {
            this._scoreForCompletion = value;
        }
    }

    public Sprite targetSprite
    {
        get { return this._targetSprite; }
        set { this._targetSprite = value; }
    }

    public Sprite levelBackground
    {
        get { return this._levelBackground; }
        set { this._levelBackground = value; }
    }

    public Vector3 startPosition
    {
        get { return this._startPosition; }
        set { this._startPosition = value; }
    }

    public bool levelFailed
    {
        get { return this._levelFailed; }
        set { this._levelFailed = value; }
    }

}
