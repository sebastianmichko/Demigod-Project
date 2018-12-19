var blackTex : Texture;

var WindowHeight: int;
var WindowWidth: int;

//var leftMargin: int = 0; //Automattically Set. Don't Change.
var topMargin: int = 0; //Automattically Set. Don't Change.


var frameWidthPercent: double = 100;
var frameHeightPercent: double = 3;

var frameHeight: int; //Automatically Set. Don't Change.
var frameWidth: int; //Automatically Set Don't Change.
//change bar size using these variables

//var healing : boolean = false;

var HealthBarFULLHeight: double = 0.0;
var HealthBarWidth: double = 0.0; //Automatically Set Don't Change. 

var HealthPercentage: int = 100;

 
function OnGUI () 
{
	if(WindowHeight != Screen.height || WindowWidth != Screen.width)
	{
		HealthBarLocationX = 0;
		
		frameWidth = ScreenWidth(frameWidthPercent);
		//frameHeight = ScreenHeight(frameHeightPercent);
		
		HealthBarFULLHeight = frameHeight - 2 ;
		HealthBarWidth = frameWidth - 2;
		
		topMargin = Screen.height;
		
		WindowHeight = Screen.height;
		WindowWidth = Screen.width;
	}
	
 	//Top
    GUI.DrawTexture( Rect(HealthBarLocationX,topMargin, WindowWidth, (frameHeight)*-1), blackTex, ScaleMode.StretchToFill, true, 0 );
    //Bottom
    GUI.DrawTexture( Rect(HealthBarLocationX, 0, WindowWidth, (frameHeight)), blackTex, ScaleMode.StretchToFill, true, 0 );
}

function Start() 
{	
	WindowHeight = Screen.height;
	WindowWidth = Screen.width;
	
	HealthBarLocationX = 0;
	
	frameWidth = ScreenWidth(frameWidthPercent);
	//frameHeight = ScreenHeight(frameHeightPercent);
	
	topMargin = Screen.height;

	HealthBarFULLHeight = frameHeight - 2 ;
	HealthBarWidth = frameWidth - 2; 
}
function Update() 
{
    Blink();
}
function healthToPixels(health)
{
	return (HealthBarFULLHeight/100)*health;
}

function ScreenWidth(X)//X = Percent Width
{
	var width: int = Screen.width;
	var pixelPerPercent: double = width/100;
	var desiredPixels: int = pixelPerPercent*X;
	//Debug.Log("Width: "+width);
	//Debug.Log("Pixel Per Percent X: "+pixelPerPercent);
	//Debug.Log("Desired Pixels X: "+desiredPixels);
	return (desiredPixels);
}

function ScreenHeight(Y)//Y = Percent Height
{
	var height: int = Screen.height;
	var pixelPerPercent: double = height/100;
	var desiredPixels: int = pixelPerPercent*Y;
	//Debug.Log("Height: "+height);
	//Debug.Log("Pixel Per Percent Y: "+pixelPerPercent);
	//Debug.Log("Desired Pixels Y: "+desiredPixels);
	return (desiredPixels);
}

function Blink()
{
    frameHeight = 0;
    while(frameHeight < 300)
    {
        frameHeight += Time.deltaTime;//Broke the Unity Editor
    }

    //Time that screen is totally black
    yield WaitForSeconds(2);
    
    while(frameHeight > 0)
    {
        frameHeight--;
    }
    
}