var target : Transform;
var smooth : float = 5;
var cameraHeight : float = 25;
function Update () {
    transform.position.x = Mathf.Lerp(transform.position.x,target.position.x,Time.deltaTime*smooth);
    transform.position.y = Mathf.Lerp(transform.position.y,target.position.y + cameraHeight,Time.deltaTime*smooth);
    transform.position.z = Mathf.Lerp(transform.position.z,target.position.z,Time.deltaTime*smooth);
 
}