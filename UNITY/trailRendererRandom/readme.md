Template for Random trail color

# HOW TO USE:

Add this script in gameObject <br><br>

colorKey = new GradientColorKey[2]; = 0 and 1 so 2 color <br>
colorKey = new GradientColorKey[5]; = 0,1,2,3,4,5 so 5 color with 5 key so all 25% <br>
colorKey[0].time = 0.0f; = Start Color <br>
colorKey[1].time = 1.0f; = End Color <br><br>
alphaKey = new GradientAlphaKey[2]; = 0 and 1 so 2 Alpha fade <br>
alphaKey = new GradientAlphaKey[5]; = 0,1,2,3,4,5 so 5 Alpha fade with 5 key so all 25% <br>
alphaKey[0].time = 0.0f; Start Color <br>
alphaKey[1].time = 1.0f; = End Color <br><br>
