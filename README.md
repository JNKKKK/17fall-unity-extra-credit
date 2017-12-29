# Overview
A unity program that can make up an image by humanoid agents with different colors.

# Screenshots
use local image ```\Assets\ResourceImages\pi.jpg```
with __Black__ and __Color__ enabled
![alt text](https://raw.githubusercontent.com/JNKKKK/17fall-unity-extra-credit/master/Screenshots/demo_pi_bc.png)

with __Black__ , __White__ and __Color__ enabled
![alt text](https://raw.githubusercontent.com/JNKKKK/17fall-unity-extra-credit/master/Screenshots/demo_pi_bwc.png)

with only __White__ enabled
![alt text](https://raw.githubusercontent.com/JNKKKK/17fall-unity-extra-credit/master/Screenshots/demo_pi_w.png)

# Features

- local image as input
  - support BMP, GIF, JPEG, PNG, TIFF
  - put your image under ```\Assets\ResourceImages\```
- online image as input
  - support BMP, GIF, JPEG, PNG, TIFF
  - some of __https__ site do __not__ work
- reuse agents when switch to another image (transition)
  - When you switch to another image (has shared color with the previous one), you will notice that some of the agents in the field are still there and others are removed. Then new agents will come in and join them to make up the new image.

# Platform

Windows __only__

Because `System.Drawing.dll` is used to process image files.

# Demo video
~~https://youtu.be/agNqiJ-XNNM~~ (old)

New videos:

https://youtu.be/BPGp0lbsqj8 (6x fast forwarded)

this video demostrates a transition from from __pi.jpg__ with __B&W&Color__ enables to the same image with __B&Color__ enabled.

https://youtu.be/Ywzol6UeBLY (6x fast forwarded)

this video demostrate a transition from a large, black and colored image to a very small, white and colored image with white and colored actors enabled.