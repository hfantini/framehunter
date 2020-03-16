
<?php

/*

	+ =====================================================================
	|                                                            
	|	== CODECITADEL STUDIOS @ 2016                              
	|   == FRAME HUNTER - WEBSERVICES                                   
	|	====================================                     
	|   ==    FILE: ServiceLeaderboards.php          						 
	|	==    DATE (YYYY-MM-DD): 2017-07-25 | TIME (HH:MM:SS): 08:03:00 AM              
	|   ==   SINCE: 0.0.1a				                         
	|	==  AUTHOR: Henrique Fantini                             
	|   == CONTACT: codecraft@outlook.com                        
	|   ====================================                     
	|   ==                                                       

*/

// == IMPORT LIBS
// ==========================================================================================

include_once("../../../../include/rest.inc.php");
include_once("../../config/DatabaseConnection.php");
include_once("../../bo/BOScore.php");
include_once("../../model/FHScore.php");
include_once("../../model/FHUserIdentification.php");
include_once("../../model/FHPlatform.php");
include_once("../../model/FHBanlistMAC.php");
include_once("../../dao/DAOScore.php");
include_once("../../dao/DAOUserIdentification.php");

// == CLASS
// ==========================================================================================

class API extends REST
{
	// == DECLARATION
	// ======================================================================================
	
	// == CONST =============================================================================
	
	// == VAR ===============================================================================
	
	private $boScore;
	
	// == CLASS CONSTRUCTOR(S)
	// ======================================================================================
	
	public function __construct()
	{
        parent::__construct();
		$this->boScore = new BOScore();
	}
	
	// == CLASS METHODS
	// ======================================================================================

	public function requestFunction()
	{
		try
		{
			$func = strtolower( trim( str_replace("/", "", $_REQUEST['rquest']) ) );
			
			if( method_exists($this, $func) == true )
			{
				$this->$func();
				$this->response('', 200); 
			}
			else
			{
				$this->response('', 404);
			}
		}
		catch(Exception $e)
		{
			echo($e->getMessage());

			$this->response('', 404);
		}
	}
	
	function addScore()
	{
		try
		{
			$param = $this->_request['value'];
			
			if($param != null)
			{
				$this->boScore->addNewScore($param);  
			}
		}
		catch(Exception $e)
		{
			throw $e;
		}
	}
	
	function getLeaderboards()
	{
		try
		{
			print $this->boScore->getLeaderboards(); 		
		}
		catch(Exception $e)
		{
			throw $e;
		}
	}
		
	// == CLASS EVENTS
	// ======================================================================================
		
	// == GETTERS AND SETTERS
	// ======================================================================================	
	
	private function set_headers()
	{
		header("HTTP/1.1 ".$this->_code." ".$this->get_status_message());
		header("Content-Type:".$this->_content_type);
	}	
}	
	
// == INITIALIZE API
     
$api = new API;
$api->requestFunction();
	
?>