<?php

	/*

		+ =====================================================================
		|                                                            
		|	== CODECITADEL STUDIOS @ 2016                              
		|   == FRAME HUNTER - WEBSERVICES                                   
		|	====================================                     
		|   ==    FILE: DAOUserIdentification.php           						 
		|	==    DATE (YYYY-MM-DD): 2017-07-25 | TIME (HH:MM:SS): 09:33:00 AM              
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

	class DAOUserIdentification
	{
		// == DECLARATION
		// ======================================================================================
		
		// == CONST =============================================================================
		
		// == VAR ===============================================================================
		
		// == CLASS CONSTRUCTOR(S)
		// ======================================================================================
		
		// == CLASS METHODS
		// ======================================================================================
			
		public function insertOrUpdate($obj)
		{
			try
			{
				if($obj instanceof FHUserIdentification)
				{
					$query = mysql_query("INSERT INTO FH_USER_IDENTIFICATION ( UID_NAME, UID_IPADDR, UID_MACADDR, PLA_ID) VALUES('" . $obj->getUidName() . "','" . $obj->getUidIPADDR() . "','" . $obj->getUidMACADDR() . "','" . $obj->getPlatform()->getPlaID() . "' )", DatabaseConnection::$instance->getConnection());

					if($query != 1)
					{
						throw new Exception("DAOUserIdentification: Insert failed | " . mysql_error(DatabaseConnection::$instance->getConnection()) );
					}
					else
					{
						$obj->setUidID( mysql_insert_id( DatabaseConnection::$instance->getConnection() ) );
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
			
		// == CLASS EVENTS
		// ======================================================================================
		
		// == GETTERS AND SETTERS
		// ======================================================================================	
	}

?>
