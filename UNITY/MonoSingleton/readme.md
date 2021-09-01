# PURPOSE
Convert any MonoBehaviour object into an unique instance accesible with YourClass.Instance

# HOW TO USE:
- Create a gameobject in your scene and add your script into it
- In your script, replace MonoBehaviour with MonoSingleton<YourClass>
- Now you can access to your script by calling anywhere in your code YourClass.Instance

# WARNING:
- Awake is used by MonoSingleton so be sure to use "public override Init(){ Your code }" in your code instead of Awake

# EXAMPLE:
```cs

public class MyClass : MonoSingleton<MyClass>
{
	public string Test = "Test Valid!";

	public int GetARandomNumber(){
		return 666;
	}
	
	public override Init(){
		Debug.Log(Test + "1");
	}
}

public class AnOtherClass : MonoBehaviour
{

	void Start(){
		Debug.Log(MyClass.Instance.Test + " 2");
		
		int randomNumber = MyClass.Instance.GetARandomNumber();
		Debug.Log($"Random number:{randomNumber});
	}

}

/*	OUTPUT:
	Test Valid 1
	Test Valid 2
	666
*/
```