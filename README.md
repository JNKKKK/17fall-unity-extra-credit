# Overview
A unity program that can make up an image by humanoid agents with different colors.

# Screenshots
use local image ```\Assets\ResourceImages\pi.jpg```
![alt text](https://raw.githubusercontent.com/JNKKKK/17fall-unity-extra-credit/master/Screenshots/demo_pi.png)

use local image ```\Assets\ResourceImages\ru.jpg```
![alt text](https://raw.githubusercontent.com/JNKKKK/17fall-unity-extra-credit/master/Screenshots/demo_ru.png)

use local image ```\Assets\ResourceImages\rainbow.jpg```
![alt text](https://raw.githubusercontent.com/JNKKKK/17fall-unity-extra-credit/master/Screenshots/demo_rainbow.png)

use online image http://image.coolapk.com/apk_logo/2016/0123/12202_1453560019_4001.png
![alt text](https://raw.githubusercontent.com/JNKKKK/17fall-unity-extra-credit/master/Screenshots/demo_android.png)

# Features

- local image as input
  - support BMP, GIF, JPEG, PNG, TIFF
- online image as input
  - support BMP, GIF, JPEG, PNG, TIFF
  - some of __https__ site do __not__ work
- reuse agents when switch to another image (transition)
  - When you switch to another image (has shared color with the previous one), you will notice that some of the agents in the field are still there and others are removed. Then new agents will come in and join them to make up the new image.

# Platform

Windows __only__
Because I use `System.Drawing.dll` to process image files.

# Demo video
Since the limited CPU&GPU performance of my laptop, the program in demo video runs with very low fps. Sorry about that.
https://youtu.be/agNqiJ-XNNM