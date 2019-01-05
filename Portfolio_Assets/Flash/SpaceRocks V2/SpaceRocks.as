package {
	import flash.display.*;
	import flash.events.*;
	import flash.text.*;
	import flash.utils.getTimer;
	import flash.utils.Timer;
	import flash.geom.Point;
	import flash.media.Sound;
	
	/*	Update History
	Seminar 2 - Mouse Follower & Distance Based Collsion Update
	Seminar 3 - Multiple Collisions Update
	Seminar 4 - Movement Tweaks & Sounds Update
	Seminar 5 - More Movement Tweaks & Sounds
	Seminar 6 - Wrap Up & Clean Up
	
	Modified By Matthew Dodson
	*/
	
	public class SpaceRocks extends MovieClip {
		static const shipRotationSpeed:Number = .1;
		static const rockSpeedStart:Number = .03;
		static const rockSpeedIncrease:Number = .02;
		static const missileSpeed:Number = .2;
		static const thrustPower:Number = .050;
		static const shipRadius:Number = 20;
		static const startingShips:uint = 3;
	
		// game objects
		private var ship:Ship;
		private var rocks:Array;
		private var missiles:Array;
		
		// animation timer
		private var lastTime:uint;
		
		// arrow keys									- Removed for Mouse Follower Update - 
		
		// ship velocity
		private var shipMoveX:Number;
		private var shipMoveY:Number;
		
		
		// timers
		private var delayTimer:Timer;
		private var shieldTimer:Timer;
		
		// game mode
		private var gameMode:String;
		private var shieldOn:Boolean;
		
		// ships and shields
		private var shipsLeft:uint;
		private var shieldsLeft:uint;
		private var shipIcons:Array;
		private var shieldIcons:Array;
		private var scoreDisplay:TextField;

		// score and level
		private var gameScore:Number;
		private var gameLevel:uint;

		// sprites
		private var gameObjects:Sprite;
		private var scoreObjects:Sprite;
		
		// Sounds													- Added for the Movement & Sounds Update - 
		private var misImp:Sound = new missileImpact;
		private var misLaun:Sound = new missileLaunch;
		private var shipExplo:Sound = new shipExplosion;
		private var rockBoun:Sound = new rockBounce;
		private var shieldS:Sound = new shieldSound;
		private var warningS:Sound = new nextWaveWarn;
		
		// start the game
		public function startSpaceRocks() {
			// set up sprites
			gameObjects = new Sprite();
			addChild(gameObjects);
			scoreObjects = new Sprite();
			addChild(scoreObjects);
			
			// reset score objects
			gameLevel = 1;
			shipsLeft = startingShips;
			gameScore = 0;
			createShipIcons();
			createScoreDisplay();
						
			// set up listeners
			addEventListener(Event.ENTER_FRAME,moveGameObjects);
			stage.addEventListener(KeyboardEvent.KEY_DOWN,keyDownFunction);
			stage.addEventListener(Event.ENTER_FRAME, moveShip);
			
			// start 
			gameMode = "delay";
			shieldOn = false;
			missiles = new Array();
			nextRockWave(null);
			newShip(null);
		}
		
		
		// SCORE OBJECTS
		
		// draw number of ships left
		public function createShipIcons() {
			shipIcons = new Array();
			for(var i:uint=0;i<shipsLeft;i++) {
				var newShip:ShipIcon = new ShipIcon();
				newShip.x = 20+i*15;
				newShip.y = 375;
				scoreObjects.addChild(newShip);
				shipIcons.push(newShip);
			}
		}
		
		// draw number of shields left
		public function createShieldIcons() {
			shieldIcons = new Array();
			for(var i:uint=0;i<shieldsLeft;i++) {
				var newShield:ShieldIcon = new ShieldIcon();
				newShield.x = 530-i*15;
				newShield.y = 375;
				scoreObjects.addChild(newShield);
				shieldIcons.push(newShield);
			}
		}
		
		// put the numerical score at the upper right
		public function createScoreDisplay() {
			scoreDisplay = new TextField();
			scoreDisplay.x = 500;
			scoreDisplay.y = 10;
			scoreDisplay.width = 40;
			scoreDisplay.selectable = false;
			var scoreDisplayFormat = new TextFormat();
			scoreDisplayFormat.color = 0xFFFFFF;
			scoreDisplayFormat.font = "Arial";
			scoreDisplayFormat.align = "right";
			scoreDisplay.defaultTextFormat = scoreDisplayFormat;
			scoreObjects.addChild(scoreDisplay);
			updateScore();
		}
		
		// new score to show
		public function updateScore() {
			scoreDisplay.text = String(gameScore);
		}
		
		// remove a ship icon
		public function removeShipIcon() {
			scoreObjects.removeChild(shipIcons.pop());
		}
		
		// remove a shield icon
		public function removeShieldIcon() {
			scoreObjects.removeChild(shieldIcons.pop());
		}
		
		// remove the rest of the ship icons
		public function removeAllShipIcons() {
			while (shipIcons.length > 0) {
				removeShipIcon();
			}
		}
		
		// remove the rest of the shield icons
		public function removeAllShieldIcons() {
			while (shieldIcons.length > 0) {
				removeShieldIcon();
			}
		}
		
		
		// SHIP CREATION AND MOVEMENT
		
		// create a new ship
		public function newShip(event:TimerEvent) {
			// if ship exists, remove it
			if (ship != null) {
				gameObjects.removeChild(ship);
				ship = null;
			}
			
			// no more ships
			if (shipsLeft < 1) {
				endGame();
				return;
			}
			
			// create, position, and add new ship
			ship = new Ship();
			ship.gotoAndStop(1);
			ship.x = 275;
			ship.y = 200;
			ship.rotation = -90;
			ship.shield.visible = false;
			gameObjects.addChild(ship);
			
			// set up ship properties
			shipMoveX = 0.0;
			shipMoveY = 0.0;
			gameMode = "play";
			
			// set up shields
			shieldsLeft = 3;
			createShieldIcons();
			
			// all lives but the first start with a free shield
			if (shipsLeft != startingShips) {
				startShield(true);
			}
		}
		
		// register key presses										- Modified for Mouse Follower Update -
		public function keyDownFunction(event:KeyboardEvent) {
			if (event.keyCode == 32) { // space
					newMissile();
			} else if (event.keyCode == 90) { // z
					startShield(false);
			}
		}
		
		// Causes the ship to follow the player's mouse
		// animate ship  									- Modified for Mouse Follower Update -
		public function moveShip(event:Event):void
		{
			// rotate and thrust
			// ship.rotation -= shipRotationSpeed*timeDiff;
			
			var spaceshipdx:Number = mouseX - ship.x;
			var spaceshipdy:Number = mouseY - ship.y;
			var angle:Number  = Math.atan2(spaceshipdy, spaceshipdx);
			ship.rotation = angle * 180 / Math.PI;
																//- Modified for Movement & Sound Update
			var ax:Number = (ship.x - mouseX) * thrustPower;
			var ay:Number = (ship.y - mouseY) * thrustPower;
			
			// Turns on thruster animation
			if (gameMode == "play") ship.gotoAndStop(2);
			
			ship.x -= ax;
			ship.y -= ay;
			
			// wrap around screen
			if ((shipMoveX > 0) && (ship.x > 570)) {
				ship.x -= 590;
			}
			if ((shipMoveX < 0) && (ship.x < -20)) {
				ship.x += 590;
			}
			if ((shipMoveY > 0) && (ship.y > 420)) {
				ship.y -= 440;
			}
			if ((shipMoveY < 0) && (ship.y < -20)) {
				ship.y += 440;
			}
		}
		
		// remove ship
		public function shipHit() {
			gameMode = "delay";
			shipExplo.play();
			ship.gotoAndPlay("explode");
			removeAllShieldIcons();
			delayTimer = new Timer(2000,1);
			delayTimer.addEventListener(TimerEvent.TIMER_COMPLETE,newShip);
			delayTimer.start();
			removeShipIcon();
			shipsLeft--;
		}
		
		// turn on shield for 3 seconds
		public function startShield(freeShield:Boolean) {
			if (shieldsLeft < 1) return; // no shields left
			if (shieldOn) return; // shield already on
			
			// turn on shield and set timer to turn off
			ship.shield.visible = true;
			shieldS.play();
			shieldTimer = new Timer(3000,1);
			shieldTimer.addEventListener(TimerEvent.TIMER_COMPLETE,endShield);
			shieldTimer.start();
			
			// update shields remaining
			if (!freeShield) {
				removeShieldIcon();
				shieldsLeft--;
			}
			shieldOn = true;
		}
		
		// turn off shield
		public function endShield(event:TimerEvent) {
			ship.shield.visible = false;
			shieldOn = false;
		}
		
		// ROCKS		
		
		// create a single rock of a specific size
		public function newRock(x,y:int, rockType:String) {
			
			// create appropriate new class
			var newRock:MovieClip;
			var rockRadius:Number;
			if (rockType == "Big") {
				newRock = new Rock_Big();
				rockRadius = 35;
			} else if (rockType == "Medium") {
				newRock = new Rock_Medium();
				rockRadius = 20;
			} else if (rockType == "Small") {
				newRock = new Rock_Small();
				rockRadius = 10;
			}
			
			
			// choose a random look
			newRock.gotoAndStop(Math.ceil(Math.random()*3));
			
			// set start position
			newRock.x = x;
			newRock.y = y;
			
			// set random movement and rotation
			var dx:Number = Math.random()*2.0-1.0;
			var dy:Number = Math.random()*2.0-1.0;
			var dr:Number = Math.random();
			
			// add to stage and to rocks list
			gameObjects.addChild(newRock);
			rocks.push({rock:newRock, dx:dx, dy:dy, dr:dr, rockType:rockType, rockRadius: rockRadius});
		}
		
		// create four rocks
		public function nextRockWave(event:TimerEvent) {
			rocks = new Array();
			newRock(100,100,"Big");
			newRock(100,300,"Big");
			newRock(450,100,"Big");
			newRock(450,300,"Big");
			gameMode = "play";
		}
		
		// animate all rocks
		public function moveRocks(timeDiff:uint) {
			for(var i:int=rocks.length-1;i>=0;i--) {
				
				// move the rocks
				var rockSpeed:Number = rockSpeedStart + rockSpeedIncrease*gameLevel;
				rocks[i].rock.x += rocks[i].dx*timeDiff*rockSpeed;
				rocks[i].rock.y += rocks[i].dy*timeDiff*rockSpeed;
				
				// rotate rocks
				rocks[i].rock.rotation += rocks[i].dr*timeDiff*rockSpeed;
				
				// wrap rocks
				if ((rocks[i].dx > 0) && (rocks[i].rock.x > 570)) {
					rocks[i].rock.x -= 590;
				}
				if ((rocks[i].dx < 0) && (rocks[i].rock.x < -20)) {
					rocks[i].rock.x += 590;
				}
				if ((rocks[i].dy > 0) && (rocks[i].rock.y > 420)) {
					rocks[i].rock.y -= 440;
				}
				if ((rocks[i].dy < 0) && (rocks[i].rock.y < -20)) {
					rocks[i].rock.y += 440;
				}
			}
		}
		
		public function rockHit(rockNum:uint) {							//	- Modified for Wrap Up & Clean Up -
			// create two smaller rocks
			if (rocks[rockNum].rockType == "Big") {
				newRock(rocks[rockNum].rock.x,rocks[rockNum].rock.y,"Medium");
				newRock(rocks[rockNum].rock.x + 30,rocks[rockNum].rock.y + 30,"Medium");
			} else if (rocks[rockNum].rockType == "Medium") {
				newRock(rocks[rockNum].rock.x,rocks[rockNum].rock.y,"Small");
				newRock(rocks[rockNum].rock.x + 20,rocks[rockNum].rock.y + 20,"Small");
			}
			
			rockBoun.play();
			
			// remove original rock
			gameObjects.removeChild(rocks[rockNum].rock);
			rocks.splice(rockNum,1);
		}

		
		// MISSILES
		
		// create a new Missile
		public function newMissile() {
			// create
			var newMissile:Missile = new Missile();
			
			// set direction
			newMissile.dx = Math.cos(Math.PI*ship.rotation/180);
			newMissile.dy = Math.sin(Math.PI*ship.rotation/180);
			
			// placement
			newMissile.x = ship.x + newMissile.dx*shipRadius;
			newMissile.y = ship.y + newMissile.dy*shipRadius;
	
			misLaun.play();
			
			// add to stage and array
			gameObjects.addChild(newMissile);
			missiles.push(newMissile);
		}
		
		// animate missiles
		public function moveMissiles(timeDiff:uint) {
			for(var i:int=missiles.length-1;i>=0;i--) {
				// move
				missiles[i].x += missiles[i].dx*missileSpeed*timeDiff;
				missiles[i].y += missiles[i].dy*missileSpeed*timeDiff;
				// moved off screen
				if ((missiles[i].x < 0) || (missiles[i].x > 550) || (missiles[i].y < 0) || (missiles[i].y > 400)) {
					gameObjects.removeChild(missiles[i]);
					delete missiles[i];
					missiles.splice(i,1);
				}
			}
		}
			
		// remove a missile
		public function missileHit(missileNum:uint) {
			misImp.play();
			
			gameObjects.removeChild(missiles[missileNum]);
			missiles.splice(missileNum,1);
		}
		
		// GAME INTERACTION AND CONTROL
		
		
		public function moveGameObjects(event:Event) {
			
			// get timer difference and animate
			var timePassed:uint = getTimer() - lastTime;
			lastTime += timePassed;
			moveRocks(timePassed);
			
			
			moveMissiles(timePassed);
			checkCollisions();
		}
		
		// look for missiles colliding with rocks                       - Modify for the Distance Based Collisions Update -
		public function checkCollisions() {
			// loop through rocks
			rockloop: for(var j:int=rocks.length-1;j>=0;j--) {
				// loop through missiles
				missileloop: for(var i:int=missiles.length-1;i>=0;i--) {
					
					var distx:Number = rocks[j].rock.x - missiles[i].x;
					var disty:Number = rocks[j].rock.y - missiles[i].y;
					
					// Distance based collision formula
					var dist:Number = Math.sqrt(distx*distx + disty*disty);
					
					// collision detection 
					if (dist < rocks[j].rockRadius) {
						
						// remove rock and missile
						rockHit(j);
						missileHit(i);
						
						// add score
						gameScore += 10;
						updateScore();
						
						// break out of this loop and continue next one
						continue rockloop;
					}
				}
				
				// check for rock hitting ship
				if (gameMode == "play") {
					if (shieldOn == false) { // only if shield is off
					
					var distsx:Number = rocks[j].rock.x - ship.x;
					var distsy:Number = rocks[j].rock.y - ship.y;
					
					// Distance based collision formula
					var dists:Number = Math.sqrt(distsx*distsx + distsy*distsy);
					
						if (dists < rocks[j].rockRadius+shipRadius) {
							
							// remove ship and rock
							shipHit();
							rockHit(j);
						}
					}
				}
			}
			
			//Checks rock-to-rock collisions										- Added for the Multi Collision Update -
				for(i = 0; i < rocks.length - 1; i++)
				{
					for(var jx:Number = i + 1; jx < rocks.length; jx++)
					{
						
						checkMCollision(i, jx);
					}
				}
			
			// all out of rocks, change game mode and trigger more
			if ((rocks.length == 0) && (gameMode == "play")) {
				gameMode = "betweenlevels";
				gameLevel++; // advance a level
				
				warningS.play();											// - Added for More Movement Tweaks & Sounds Update -
				
				delayTimer = new Timer(2000,1);
				delayTimer.addEventListener(TimerEvent.TIMER_COMPLETE,nextRockWave);
				delayTimer.start();
			}
		}
		
		public function endGame() {
			// remove all objects and listeners
			removeChild(gameObjects);
			removeChild(scoreObjects);
			gameObjects = null;
			scoreObjects = null;
			removeEventListener(Event.ENTER_FRAME,moveGameObjects);
			stage.removeEventListener(KeyboardEvent.KEY_DOWN,keyDownFunction);
			stage.removeEventListener(Event.ENTER_FRAME, moveShip); // Stops trying to move the Ship
			
			gotoAndStop("gameover");
		}
		
		// Checks for, and handles, collisions between rocks						- Added for the Multi Collision Update -
		public function checkMCollision(rockNum1:uint, rockNum2:uint):void	
		{
			
			var dx:Number = rocks[rockNum2].rock.x - rocks[rockNum1].rock.x;
			var dy:Number = rocks[rockNum2].rock.y - rocks[rockNum1].rock.y;
			var dist:Number = Math.sqrt(dx*dx + dy*dy);
			var RadiusSum:Number = rocks[rockNum1].rockRadius + rocks[rockNum2].rockRadius;

			if(dist < rocks[rockNum1].rockRadius + rocks[rockNum2].rockRadius)
			{
				// calculate angle, sine and cosine
				var angle:Number = Math.atan2(dy, dx);
				var sin:Number = Math.sin(angle);
				var cos:Number = Math.cos(angle);
				
				// rotate rock1's position
				var pos0:Point = new Point(0, 0);
				
				// rotate rock2's position
				var pos1:Point = rotate(dx, dy, sin, cos, true);
				
				// rotate rock1's velocity
				var vel0:Point = rotate(rocks[rockNum1].dx,
										rocks[rockNum1].dy,
										sin,
										cos,
										true);
				
				// rotate rock2's velocity
				var vel1:Point = rotate(rocks[rockNum2].dx,
										rocks[rockNum2].dy,
										sin,
										cos,
										true);
				
				// collision reaction	
				var vxTotal:Number = vel0.x - vel1.x;
				
				vel0.x = ((rocks[rockNum1].rockRadius - rocks[rockNum2].rockRadius) * vel0.x + 
				          2 * rocks[rockNum2].rockRadius * vel1.x) / 
				          (rocks[rockNum1].rockRadius + rocks[rockNum2].rockRadius);
				vel1.x = vxTotal + vel0.x;

				// update position
				pos0.x += vel0.x;
				pos1.x += vel1.x;
				
				// rotate positions back
				var pos0F:Object = rotate(pos0.x,
										  pos0.y,
										  sin,
										  cos,
										  false);
										  
				var pos1F:Object = rotate(pos1.x,
										  pos1.y,
										  sin,
										  cos,
										  false);

				// adjust positions to actual screen positions
				rocks[rockNum2].rock.x = rocks[rockNum1].rock.x + pos1F.x;
				rocks[rockNum2].rock.y = rocks[rockNum1].rock.y + pos1F.y;
				rocks[rockNum1].rock.x = rocks[rockNum1].rock.x + pos0F.x;
				rocks[rockNum1].rock.y = rocks[rockNum1].rock.y + pos0F.y;
				
				// rotate velocities back
				var vel0F:Object = rotate(vel0.x,
										  vel0.y,
										  sin,
										  cos,
										  false);
				var vel1F:Object = rotate(vel1.x,
										  vel1.y,
										  sin,
										  cos,
										  false);
				rocks[rockNum1].dx = vel0F.x;
				rocks[rockNum1].dy = vel0F.y;
				rocks[rockNum2].dx = vel1F.x;
				rocks[rockNum2].dy = vel1F.y;
			}
		}
		
		public function rotate(x:Number,
								y:Number,
								sin:Number,
								cos:Number,
								reverse:Boolean):Point
		{
			var result:Point = new Point();
			if(reverse)
			{
				result.x = x * cos + y * sin;
				result.y = y * cos - x * sin;
			}
			else
			{
				result.x = x * cos - y * sin;
				result.y = y * cos + x * sin;
			}
			return result;
		}		
		
		
		
	}
}
		
	