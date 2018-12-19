var frameTexture : Texture;

var WindowHeight: int;
var WindowWidth: int;

var topMargin: int; //Automattically Set. Don't Change.

var MapLocationXPercent: double; //Percent Value
var MapLocationX: int = 0.0;//Pixel value


var frameWidthPercent: double;
var frameHeightPercent: double;

var frameHeight: int; //Automatically Set. Don't Change.
var frameWidth: int; //Automatically Set Don't Change.

function OnGUI () 
{
	if(WindowHeight != Screen.height || WindowWidth != Screen.width)
	{
		MapLocationX = ScreenWidth(MapLocationXPercent);
		
		frameWidth = ScreenWidth(frameWidthPercent);
		frameHeight = ScreenHeight(frameHeightPercent);
		
		topMargin = Screen.height - frameHeight;
		
		WindowHeight = Screen.height;
		WindowWidth = Screen.width;
	}

	
 	//Background
    GUI.DrawTexture( Rect(MapLocationX,topMargin , frameWidth, frameHeight), frameTexture, ScaleMode.StretchToFill, true, 0 );
 
}


function Start() 
{	
	WindowHeight = Screen.height;
	WindowWidth = Screen.width;
	
	MapLocationX = ScreenWidth(MapLocationXPercent);
	
	
	frameWidth = ScreenWidth(frameWidthPercent);
	frameHeight = ScreenHeight(frameHeightPercent);
	
	topMargin = Screen.height - frameHeight;
}

function Update() 
{
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