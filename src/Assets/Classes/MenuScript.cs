
/*

	+ =====================================================================
	|                                                            
	|	== CODECASTLE STUDIOS @ 2016                              
	|   == FRAME HUNTER - GAME                                   
	|	====================================                     
	|   ==    FILE: MenuScript.cs           						 
	|	==    DATE (YYYY-MM-DD): 2017-07-13 | TIME (HH:MM:SS): 11:22:00 AM              
	|   ==   SINCE: 0.0.1a				                         
	|	==  AUTHOR: Henrique Fantini                             
	|   == CONTACT: codecraft@outlook.com                        
	|   ====================================                     
	|   ==                                                       

*/

// == IMPORT LIBS
// ==========================================================================================

using UnityEngine;
using UnityEngine.SceneManagement;

// == CLASS
// ==========================================================================================

public class MenuScript : MonoBehaviour
{
    // == DECLARATION
    // ======================================================================================

    // == CONST =============================================================================

    // == VAR ===============================================================================

    // == STATES
    private EnumMenuState _currentMenuState;
	private EnumMenuOptionSelected _optionSelected;
    private EnumMenuCreditsOptionSelected _creditsOptionSelected;

    // == SPRITES
    private GameObject sprFlash;
    private GameObject sprShadow;
    private GameObject sprFilm1;
    private GameObject sprFilm2;
    private GameObject sprGameLogo;
    private GameObject sprSep;

    // == AUDIO
    private AudioSource audSrcMusic;
    private AudioSource audSrcCameraShot;

    // == CONTAINERS
    private MenuContainer mnuActiveContainer;
    private MenuContainerMain mnuContainerMain;
    private MenuContainerCredits mnuContainerCredits;

    // == VALUES
    private Vector3 cameraBounds;
    private bool filmAnimationInvert;
    private float timeAux;
    private bool soundPlay = false;

    // == CLASS CONSTRUCTOR(S)
    // ======================================================================================

    // == CLASS METHODS
    // ======================================================================================

    void Start()
    {
        Application.targetFrameRate = 60;

        // == INITIALIZING VALUES: STATES

        _currentMenuState = EnumMenuState.MENUSTATE_MAIN_ENTER_FLASH_IN;
        _optionSelected = EnumMenuOptionSelected.MENUCONTAINER_MAIN_OPT_NONE;
        _creditsOptionSelected = EnumMenuCreditsOptionSelected.MENUCONTAINER_CREDITS_OPT_NONE;

        // == INITIALIZING VALUES: SOUND AND MUSIC
        audSrcMusic = GameObject.Find("audSrcMusic").GetComponent<AudioSource>();
        audSrcMusic.loop = true;

        audSrcCameraShot = GameObject.Find("audSrcCameraShot").GetComponent<AudioSource>();

        // == INITIALIZING VALUES: SPRITES

        sprFlash = GameObject.Find("sprFlash");
        sprShadow = GameObject.Find("sprShadow");

        sprFilm1 = GameObject.Find("sprFilm1");
        sprFilm1.GetComponent<SpriteRenderer>().enabled = false;

        sprFilm2 = GameObject.Find("sprFilm2");
        sprFilm2.GetComponent<SpriteRenderer>().enabled = false;

        sprGameLogo = GameObject.Find("sprGameLogo");
        sprGameLogo.GetComponent<SpriteRenderer>().enabled = false;

        sprSep = GameObject.Find("sprSep");
        sprSep.GetComponent<SpriteRenderer>().enabled = false;

        // == INITIALIAZING VALUES: CONTAINERS

        mnuContainerMain = new MenuContainerMain(this);
        mnuContainerMain.Start();

        mnuContainerCredits = new MenuContainerCredits(this);
        mnuContainerCredits.Start();

        mnuActiveContainer = mnuContainerMain;

        // == INITIALIAZING VALUES: UI

        // == INITIALIZING VALUES

        cameraBounds = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, 0));
        filmAnimationInvert = false;
    }

    void Update()
    {
        // == GENERAL UPDATES

        Vector3 sprFilm1Pos = sprFilm1.transform.position;
        sprFilm1Pos.y -= 1.3f * Time.deltaTime;

        Vector3 sprFilm2Pos = sprFilm2.transform.position;
        sprFilm2Pos.y -= 1.3f * Time.deltaTime;
   
        if( filmAnimationInvert == false )
        {
            if ( sprFilm1Pos.y < -cameraBounds.y)
            {
                sprFilm1Pos.y = sprFilm2Pos.y + sprFilm2.GetComponent<SpriteRenderer>().bounds.size.y + 0.33f;
                filmAnimationInvert = true;
            }
        }
        else
        {
            if ( sprFilm2Pos.y < -cameraBounds.y)
            {
                sprFilm2Pos.y = sprFilm1Pos.y + sprFilm1.GetComponent<SpriteRenderer>().bounds.size.y + 0.33f;
                filmAnimationInvert = false;
            }
        }

        sprFilm1.transform.position = sprFilm1Pos;
        sprFilm2.transform.position = sprFilm2Pos;

        // == SPECIFIC UPDATES
        if (_currentMenuState == EnumMenuState.MENUSTATE_MAIN_ENTER_FLASH_IN)
        {
            Color sprFlashColor = sprFlash.GetComponent<SpriteRenderer>().color;
            sprFlashColor.a += 10.0f * Time.deltaTime;
            sprFlash.GetComponent<SpriteRenderer>().color = sprFlashColor;

            if (sprFlashColor.a > 1f)
            {
                mnuActiveContainer.enable(true);
                _currentMenuState = EnumMenuState.MENUSTATE_MAIN_ENTER_FLASH_OUT;
            }
        }
        else if (_currentMenuState == EnumMenuState.MENUSTATE_MAIN_ENTER_FLASH_OUT)
        {

            sprFilm1.GetComponent<SpriteRenderer>().enabled = true;
            sprFilm2.GetComponent<SpriteRenderer>().enabled = true;
            sprGameLogo.GetComponent<SpriteRenderer>().enabled = true;
            sprSep.GetComponent<SpriteRenderer>().enabled = true;

            Color sprFlashColor = sprFlash.GetComponent<SpriteRenderer>().color;
            sprFlashColor.a -= 1.7f * Time.deltaTime;
            sprFlash.GetComponent<SpriteRenderer>().color = sprFlashColor;

            if (sprFlashColor.a < 0f)
            {
                mnuActiveContainer.menuContainerState = EnumMenuContainerState.MENUCONTAINERSTATE_TRANSITON_IDLE;
                _currentMenuState = EnumMenuState.MENUSTATE_MAIN_IDLE;
            }
        }
        else if (_currentMenuState == EnumMenuState.MENUSTATE_MAIN_IDLE)
        {
            mnuActiveContainer.Update();
        
            if (mnuActiveContainer is MenuContainerMain)
            {
                if (_optionSelected == EnumMenuOptionSelected.MENUCONTAINER_MAIN_OPT_NEWGAME)
                {
                    _currentMenuState = EnumMenuState.MENUSTATE_MAIN_LEAVE_FLASH_IN;
                }
                else if (_optionSelected == EnumMenuOptionSelected.MENUCONTAINER_MAIN_OPT_EXIT)
                {
                    _currentMenuState = EnumMenuState.MENUSTATE_MAIN_LEAVE_FLASH_IN;
                }
                else if (_optionSelected == EnumMenuOptionSelected.MENUCONTAINER_MAIN_OPT_CREDITS)
                {
                    if (mnuActiveContainer.menuContainerState == EnumMenuContainerState.MENUCONTAINERSTATE_TRANSITON_INACTIVE)
                    {
                        // CHANGES THE CONTAINER TO CREDITS
                        _optionSelected = EnumMenuOptionSelected.MENUCONTAINER_MAIN_OPT_NONE;
                        mnuContainerCredits.menuContainerState = EnumMenuContainerState.MENUCONTAINERSTATE_TRANSITON_IN;
                        mnuActiveContainer = mnuContainerCredits;
                    }
                    else
                    {
                        mnuActiveContainer.menuContainerState = EnumMenuContainerState.MENUCONTAINERSTATE_TRANSITON_OUT;
                    }
                }
            }
            else if (mnuActiveContainer is MenuContainerCredits)
            {
                if (_creditsOptionSelected == EnumMenuCreditsOptionSelected.MENUCONTAINER_CREDITS_OPT_EXIT)
                {
                    if (mnuActiveContainer.menuContainerState == EnumMenuContainerState.MENUCONTAINERSTATE_TRANSITON_INACTIVE)
                    {
                        // CHANGES THE CONTAINER TO MAIN MENU
                        _creditsOptionSelected = EnumMenuCreditsOptionSelected.MENUCONTAINER_CREDITS_OPT_NONE;
                        mnuContainerMain.menuContainerState = EnumMenuContainerState.MENUCONTAINERSTATE_TRANSITON_IN;
                        mnuActiveContainer = mnuContainerMain;
                    }
                    else
                    {
                        mnuActiveContainer.menuContainerState = EnumMenuContainerState.MENUCONTAINERSTATE_TRANSITON_OUT;
                    }
                }
            }
        }
        else if (_currentMenuState == EnumMenuState.MENUSTATE_MAIN_LEAVE_FLASH_IN)
		{
            Color sprFlashColor = sprFlash.GetComponent<SpriteRenderer>().color;
            sprFlashColor.a += 10.0f * Time.deltaTime;
            sprFlash.GetComponent<SpriteRenderer>().color = sprFlashColor;

            if (sprFlashColor.a > 1f)
            {
                mnuActiveContainer.enable(true);
                _currentMenuState = EnumMenuState.MENUSTATE_MAIN_LEAVE_FLASH_OUT;
            }
		}
        else if (_currentMenuState == EnumMenuState.MENUSTATE_MAIN_LEAVE_FLASH_OUT)
        {
            Color sprShadowColor = sprShadow.GetComponent<SpriteRenderer>().color;
            sprShadowColor.a = 1f;
            sprShadow.GetComponent<SpriteRenderer>().color = sprShadowColor;

            Color sprFlashColor = sprFlash.GetComponent<SpriteRenderer>().color;
            sprFlashColor.a -= 1.7f * Time.deltaTime;

            if (sprFlashColor.a < 0f )
            {
                if( Time.time - timeAux > 2)
                {
                    if (_optionSelected == EnumMenuOptionSelected.MENUCONTAINER_MAIN_OPT_NEWGAME)
                    {
                        SceneManager.LoadScene(1);
                    }
                    else if (_optionSelected == EnumMenuOptionSelected.MENUCONTAINER_MAIN_OPT_EXIT)
                    {
                        Application.Quit();
                    }
                }
            }
            else
            {
                timeAux = Time.time;
            }   

            sprFlash.GetComponent<SpriteRenderer>().color = sprFlashColor;
        }
    }
	
	// == CLASS EVENTS
	// ======================================================================================
	
	// == GETTERS AND SETTERS
	// ======================================================================================	

    public EnumMenuOptionSelected optionSelected
    {
        get { return this._optionSelected; }
        set { this._optionSelected = value; }
    }

    public EnumMenuCreditsOptionSelected creditsOptionSelected
    {
        get { return this._creditsOptionSelected; }
        set { this._creditsOptionSelected = value; }
    }

}
