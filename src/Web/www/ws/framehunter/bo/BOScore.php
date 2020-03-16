<?php

	/*

		+ =====================================================================
		|                                                            
		|	== CODECITADEL STUDIOS @ 2016                              
		|   == FRAME HUNTER - WEBSERVICES                                   
		|	====================================                     
		|   ==    FILE: BOScore.php           						 
		|	==    DATE (YYYY-MM-DD): 2017-07-25 | TIME (HH:MM:SS): 11:40:00 AM              
		|   ==   SINCE: 0.0.1a				                         
		|	==  AUTHOR: Henrique Fantini                             
		|   == CONTACT: codecraft@outlook.com                        
		|   ====================================                     
		|   ==                                                       

		
	*/

	// == IMPORT LIBS
	// ==========================================================================================

	// == CLASS
	// ==========================================================================================

	class BOScore
	{
		// == DECLARATION
		// ======================================================================================
		
		// == CONST =============================================================================
		
		// == VAR ===============================================================================

		private $daoScore;
		
		// == CLASS CONSTRUCTOR(S)
		// ======================================================================================
		
		public function __construct()
		{
			$this->daoScore = new DAOScore();
		}
		
		// == CLASS METHODS
		// ======================================================================================
			
		public function addNewScore($value)
		{
			// == DECRYPTING HASH
			$array = explode("|", $value);

			if( sizeof($array) != 6)
			{
				throw new Exception("Error");
			}
			
			// == CREATING THE OBJECT
			
			$score = new FHScore();
			$score->setScoScore($array[1]);
			$score->getUserIdentification()->setUidName($array[0]);
			$score->getUserIdentification()->setUidIPADDR($_SERVER['REMOTE_ADDR']);
			$score->getUserIdentification()->setUidMACADDR($array[2]);
			$score->getUserIdentification()->getPlatform()->setPlaID($array[3]);
			$score->setScoLevel($array[4]);
			$score->setScoTime($array[5]);
			
			// == VALIDATING DATA
			
			if( $this->validateScore($score) == false )
			{
				throw new Exception("Error");
			}
			
			// == INSERTING DATA
			$this->daoScore->insertOrUpdate($score);
		}
		
		public function getLeaderboards()
		{
			$rows = $this->daoScore->query("SELECT uid.UID_NAME, sco.SCO_SCORE, sco.SCO_LEVEL, sco.SCO_TIME FROM FH_USER_IDENTIFICATION uid, FH_SCORE sco WHERE sco.UID_ID = uid.UID_ID ORDER BY sco.SCO_SCORE DESC, sco.SCO_TIME ASC LIMIT 25");
			if( $rows != null && !empty($rows) )
			{
				return json_encode($rows);
			}
			else
			{
				return null;
			}
		}
			
		private function validateScore($score)
		{
			$retValue = true;
			
			if($score instanceof FHScore)
			{
				//SCORE
				
				//NAME
				if( empty( $score->getUserIdentification()->getUidName() ) )
				{
					$retValue = false;
				}
				
				if( strlen( $score->getUserIdentification()->getUidName() ) == 0 )
				{
					$retValue = false;
				}
				
				//MAC
				
				if( strlen( $score->getUserIdentification()->getUidMACADDR() ) != 17 )
				{
					$retValue = false;
				}

			}
			
			echo($retValue);
			return $retValue;
		}			
			
		// == CLASS EVENTS
		// ======================================================================================
		
		// == GETTERS AND SETTERS
		// ======================================================================================	
	}

?>
