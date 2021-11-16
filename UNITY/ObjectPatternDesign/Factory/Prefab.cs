using UnityEngine;

/// <summary>
/// Example of object using factory
/// </summary>
public class Prefab : MonoBehaviour
{
	
    // Reference to used factory
    [SerializeField]
    private AudioFactory factory;

    public void OnRandomEventPlayASound()
    {
            var inst = factory.GetNewInstance();
			inst.PlayOneShot();
    }
}