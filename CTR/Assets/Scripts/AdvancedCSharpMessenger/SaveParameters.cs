using UnityEngine;

public static class SaveParameters
{
		/// <summary>
		/// Game Config Save Parameters.
		/// </summary>
		public static class GameConfig
		{
				public const string GAMEGRAVITY = "_gameGravity";
				public const string PLAYERJUMPSPEED = "_playerJumpSpeed";
				public const string GAMESPEED = "_gameSpeed";
				public const string PLAYERCOUNTERFORCE = "_playerCounterForce";
				public const string BGLEVELONESPEEDFACTOR = "_bgLevelOneSpeedFactor";
				public const string BGLEVELTWOSPEEDFACTOR = "_bgLevelTwoSpeedFactor";
				public const string BGLEVELTHREESPEEDFACTOR = "_bgLevelThreeSpeedFactor";
		}

		/// <summary>
		/// Level Editor Save Parameters.
		/// </summary>
		public static class LevelEditor
		{
				//Platform Maker
				public const string PLATFORM_WIDTHINBLOCKS = "_widthInBlocks";
				public const string PLATFORM_HEIGHTINBLOCKS = "_heightInBlocks";
				public const string PLATFORM_PIXELWIDHTOFONEBLOCK = "-_pixelWidthOfOneBLock";
				public const string PLATFORM_PIXELHEIGHTTOFONEBLOCK = "_pixelHeightOfOneBLock";

				//Content wizard

				public const string CONTENT_CATEGORY = "_category";
				public const string CONTENT_ENEMYTYPE = "_enemyType";
				public const string CONTENT_COLLECTIBLETYPE = "_collectibleType";
				public const string CONTENT_CONDITIONTRIGGERTYPE = "_conditionTriggerType";
				public const string CONTENT_OBSTACLETYPE = "_obstacleType";
				public const string CONTENT_POWERUPTYPE = "_powerUpType";
		}

		
		public static class PlayerProgress
		{
			public const string BLUEFALL1 = "_bluefall1";
	 		public const string BLUEFALL2 = "_bluefall2";
		 	public const string BLUEFALL3 = "_bluefall3";
			public const string FIRSTTIME = "_firstTime";
		}

}
