Bunch of Attributes options for Unity

# HOW TO USE:
just import unity package



All possibility with Attributes

![](https://cdn.discordapp.com/attachments/796831997813194752/893340816947425320/unknown.png)

Exemple : 

```cs
using KiwiAttributes;


    [Button]
    void myFunction() {
        Debug.Log("<b><color=red>Meow</color></b>");
    }
```

![](https://cdn.discordapp.com/attachments/796831997813194752/893342388418932776/unknown.png)

```cs
using KiwiAttributes; // Always

[AnimatorParam("animator0")] // Animator config

[Button] // add Button 

[CurveRange(-1, -1, 1, 1, EColor.Red)] // add curve with Color or not 

[Dropdown("intValues")] // Add dropdown for multiple value int, string, etc 
public int intValue;
private int[] intValues = new int[] { 1, 2, 3 };


[EnumFlags] // add dropdown for Enum value
public TestEnum flags0;
	public enum TestEnum
	{
		None = 0,
		B = 1 << 0,
		C = 1 << 1,
		D = 1 << 2,
		E = 1 << 3,
		F = 1 << 4,
		All = ~0
	}


[HorizontalLine(color: EColor.Black)] //Add horizontal line with color as separator
[Header("Black")]
[HorizontalLine(color: EColor.Blue)]
[Header("Blue")]
[HorizontalLine(color: EColor.Gray)]
[Header("Gray")]
[HorizontalLine(color: EColor.Green)]
[Header("Green")]
[HorizontalLine(color: EColor.Indigo)]
[Header("Indigo")]
[HorizontalLine(color: EColor.Orange)]
[Header("Orange")]
[HorizontalLine(color: EColor.Pink)]
[Header("Pink")]
[HorizontalLine(color: EColor.Red)]
[Header("Red")]
[HorizontalLine(color: EColor.Violet)]
[Header("Violet")]
[HorizontalLine(color: EColor.White)]
[Header("White")]
[HorizontalLine(color: EColor.Yellow)]
[Header("Yellow")]


[InfoBox("Normal", EInfoBoxType.Normal)] // Add warning in inspector 
[InfoBox("Warning", EInfoBoxType.Warning)]
[InfoBox("Error", EInfoBoxType.Error)]


[Header("Constant ProgressBar")] // Add custom progressbar in inspector
[ProgressBar("Health", 100, EColor.Red)]
public float health = 50.0f;
[Header("Dynamic ProgressBar")]
[ProgressBar("Elixir", "maxElixir", color: EColor.Violet)]
public int elixir = 50;
public int maxElixir = 100;


//and more more more 
```

![](https://cdn.discordapp.com/attachments/796831997813194752/893345277543591976/unknown.png)

# WARNING
DON'T touch Editor folder and don"t move