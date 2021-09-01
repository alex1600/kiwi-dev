# PURPOSE
Allow to shake your camera using Cinemachine Package

# HOW TO USE:
- Create a gameobject in your scene and add the script as a component on it
- Set the variable in the inspector (from your cinemachine virtual camera)
- IMPORTANT: in your virtual camera, set Noise to "Basic Multi Channel Perlin" then create or choose a noise profile in function of what you need. 
- My personnal noise profile for 2D (It doesn't rotate your camera, it moves only on X/Y axis):
- Set the other values to 0 except the Frequency Gain to 1 (or what ever you like)

- Finally, you can call anywhere in your script CameraShake.Shake(amp, duration) to shake your camera ! 