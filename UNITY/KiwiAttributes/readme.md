Bunch of Attribute to add above your variables to improve your inspector ! 

# HOW TO USE:
- Install & import unity package



All possibility with Attributes:

![](https://cdn.discordapp.com/attachments/796831997813194752/893340816947425320/unknown.png)

Example : 

```cs
using KiwiAttributes;


    [Button]
    void myFunction() {
        Debug.Log("<b><color=red>Meow</color></b>");
    }
```

![](https://cdn.discordapp.com/attachments/796831997813194752/893342388418932776/unknown.png)

```cs
[AnimatorParam("animator0")]
```
![](https://cdn.discordapp.com/attachments/796831997813194752/893451864392232981/unknown.png)
```cs
[CurveRange(-1, -1, 1, 1, EColor.Red)]
```
![](https://cdn.discordapp.com/attachments/796831997813194752/893451943974952960/unknown.png)
```cs
[Dropdown("intValues")]
        public int intValue;
        private int[] intValues = new int[] { 1, 2, 3 };
```
![](https://cdn.discordapp.com/attachments/796831997813194752/893452034538364968/unknown.png)
```cs
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
        [EnumFlags]
        public TestEnum flags0;

        public EnumFlagsNest1 nest1;
```
![](https://cdn.discordapp.com/attachments/796831997813194752/893452171109085184/unknown.png)
```cs
[Expandable]
```
![](https://cdn.discordapp.com/attachments/796831997813194752/893452357537521714/unknown.png)
```cs
[HorizontalLine(color: EColor.Black)]
        [Header("Black")]
        [HorizontalLine(color: EColor.Blue)]
        [Header("Blue")]
```
![](https://cdn.discordapp.com/attachments/796831997813194752/893452580234072094/unknown.png)
```cs
[InfoBox("Normal", EInfoBoxType.Normal)]
[InfoBox("Warning", EInfoBoxType.Warning)]
[InfoBox("Error", EInfoBoxType.Error)]
```
![](https://cdn.discordapp.com/attachments/796831997813194752/893452698249199626/unknown.png)
```cs
[InputAxis]
        public string inputAxis0;
```
![](https://cdn.discordapp.com/attachments/796831997813194752/893452959906684990/unknown.png)
```cs
        [Layer]
        public int layerNumber0;

        [Layer]
        public string layerName0;
```
![](https://cdn.discordapp.com/attachments/796831997813194752/893453068786610196/unknown.png)
```cs
[MinMaxSlider(0.0f, 1.0f)]
```
![](https://cdn.discordapp.com/attachments/796831997813194752/893453333673672734/unknown.png)
```cs
[Header("Constant ProgressBar")]
        [ProgressBar("Health", 100, EColor.Red)]
        public float health = 50.0f;

        [Header("Dynamic ProgressBar")]
        [ProgressBar("Elixir", "maxElixir", color: EColor.Violet)]
        public int elixir = 50;
        public int maxElixir = 100;
```
![](https://cdn.discordapp.com/attachments/796831997813194752/893453416582488074/unknown.png)
```cs
[ReorderableList]
```
![](https://cdn.discordapp.com/attachments/796831997813194752/893453554210181130/unknown.png)
```cs
[ResizableTextArea]
```
![](https://cdn.discordapp.com/attachments/796831997813194752/893453709873397790/unknown.png)
```cs
[Scene]
```
![](https://cdn.discordapp.com/attachments/796831997813194752/893453852538458122/unknown.png)
```cs
[ShowAssetPreview]
[ShowAssetPreview(96, 96)]
```
![](https://cdn.discordapp.com/attachments/796831997813194752/893453948411838474/unknown.png)
```cs
[ShowNativeProperty]
```
![](https://cdn.discordapp.com/attachments/796831997813194752/893454111649964093/unknown.png)
```cs
[ShowNonSerializedField]
```
![](https://cdn.discordapp.com/attachments/796831997813194752/893454223075864676/unknown.png)
```cs
[Tag]
```
![](https://cdn.discordapp.com/attachments/796831997813194752/893454304470523925/unknown.png)
```cs
[BoxGroup("Integers")]
[BoxGroup("Floats")]
[BoxGroup("Sliders")]
[BoxGroup]
```
![](https://cdn.discordapp.com/attachments/796831997813194752/893454479507214346/unknown.png)
```cs
[Foldout("Integers")]
[Foldout("Floats")]
[Foldout("Sliders")]
[Foldout("Transforms")]
```
![](https://cdn.discordapp.com/attachments/796831997813194752/893454651054227496/unknown.png)
```cs
[EnableIf(EConditionOperator.And, "enable1", "enable2")]
[EnableIf(EConditionOperator.Or, "enable1", "enable2")]
[DisableIf(EConditionOperator.And, "disable1", "disable2")]
[DisableIf(EConditionOperator.Or, "disable1", "disable2")]
```
![](https://cdn.discordapp.com/attachments/796831997813194752/893454930008997888/unknown.png)
```cs
[ShowIf(EConditionOperator.And, "show1", "show2")]
[ShowIf(EConditionOperator.Or, "show1", "show2")]
[HideIf(EConditionOperator.And, "hide1", "hide2")]
[HideIf(EConditionOperator.Or, "hide1", "hide2")]
```
![](https://cdn.discordapp.com/attachments/796831997813194752/893455172817264670/unknown.png)
```cs
[Label("Label 0")]
public int int0;
```
![](https://cdn.discordapp.com/attachments/796831997813194752/893455354426458112/unknown.png)
```cs
[OnValueChanged("OnValueChangedMethod1")]
        [OnValueChanged("OnValueChangedMethod2")]
        public int int0;

        private void OnValueChangedMethod1()
        {
            Debug.LogFormat("int0: {0}", int0);
        }

        private void OnValueChangedMethod2()
        {
            Debug.LogFormat("int0: {0}", int0);
        }
```
![](https://cdn.discordapp.com/attachments/796831997813194752/893455585171894302/unknown.png)
```cs
[ReadOnly]
```
![](https://cdn.discordapp.com/attachments/796831997813194752/893455711936327700/unknown.png)
```cs
[MinValue(0), MaxValue(1)]
```
![](https://cdn.discordapp.com/attachments/796831997813194752/893455840701448213/unknown.png)

```cs
[Required]
        public Transform trans0;
```
![](https://cdn.discordapp.com/attachments/796831997813194752/893456022130262016/unknown.png)

```cs
[ValidateInput("NotZero0", "int0 must not be zero")]
        public int int0;

        private bool NotZero0(int value)
        {
            return value != 0;
        }
```
![](https://cdn.discordapp.com/attachments/796831997813194752/893456142880104518/unknown.png)


# WARNING
DON'T touch Editor folder and don"t move
