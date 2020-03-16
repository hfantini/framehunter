<?php

	/*

		+ =====================================================================
		|                                                            
		|	== CODECITADEL STUDIOS @ 2016                              
		|   == FRAME HUNTER - WEBSERVICES                                   
		|	====================================                     
		|   ==    FILE: DAOScore.php						 
		|	==    DATE (YYYY-MM-DD): 2017-07-25 | TIME (HH:MM:SS): 13:09:00 PM             
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

	class DAOScore
	{
		// == DECLARATION
		// ======================================================================================
		
		// == CONST =============================================================================
		
		// == VAR ===============================================================================
		
		private $daoUserID;
		
		// == CLASS CONSTRUCTOR(S)
		// ======================================================================================
		
		// == CLASS METHODS
		// ======================================================================================
			
		public function __construct()
		{
			$this->daoUserID = new DAOUserIdentification();
		}
			
		public function insertOrUpdate($obj)
		{
			try
			{
				$this->daoUserID->insertOrUpdate( $obj->getUserIdentification() );
				
				if($obj instanceof FHScore)
				{
					$query = mysql_query("INSERT INTO FH_SCORE ( SCO_SCORE, UID_ID, SCO_LEVEL, SCO_TIME ) VALUES(" . $obj->getScoScore() . "," . $obj->getUserIdentification()->getUidID() . "," . $obj->getScoLevel() . ",'" . $obj->getScoTime() . "')", DatabaseConnection::$instance->getConnection());

					if($query != 1)
					{
						throw new Exception("DAOUserIdentification: Insert failed. CAUSE: " . mysql_error( DatabaseConnection::$instance->getConnection() ) );
					}
				}
				else
				{
					throw new Exception("DAOUserIdentification: Invalid object instance.");
				}
			}
			catch(Exception $e)
			{
				throw $e;
			}
		}	

		public function query($sql)
		{
			$retValue = Array();
			
			$query = mysql_query($sql, DatabaseConnection::$instance->getConnection());
			
			while( $row = mysql_fetch_array($query, MYSQL_ASSOC) ) 
			{
				$row_array['UID_NAME'] = $row['UID_NAME'];
				$row_array['SCO_SCORE'] = $row['SCO_SCORE'];
				$row_array['SCO_LEVEL'] = $row['SCO_LEVEL'];
				$row_array['SCO_TIME'] = $row['SCO_TIME'];
				
				array_push($retValue ,$row_array);
			}
			
			return $retValue;
		}
			
		// == CLASS EVENTS
		// ======================================================================================
		
		// == GETTERS AND SETTERS
		// ======================================================================================	
	}

?>
