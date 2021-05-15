## Assignment I : Data Security

Nr27 assignment: Image Steganography, changing 3 bits per pixel.
---

#### Introduction
Steganography is the art and science of hiding information by embedding messages within others. Steganography works by replacing bits of useless or unused data in regular computer files with bits of different, invisible information. This hidden information can be plain text, cipher text, or even images.

In our case, our data will be the plain text that we need to hide, and the unused data is the least significant bits (LSBs) in the image pixels.

---

#### Brief Explanation
1. Hiding the text inside the image
    
    ..* Loop through the pixels of the image. In each iteration, get the RGB values separated each in a separate integer.
    ..* For each of R, G, and B, make the LSB equals to 0. These bits will be used in hiding characters..

2. Extracting the text from the image

    ..* Pass through the pixels of the image until you find 8 consecutive zeros.
    ..* Pick the LSB from each pixel element (R, G, B) and attach it into an empty value. When the 8 bits of this value are done, convert it back to character, then add that character to the result text you are seeking.


---

#### Contributors
This assignment is done by three Computer Engineering students at University of Prishtina.

* Ora Vrapcani [@oravrp](https://github.com/oravrp)
* Lum Dukaj [@lumdukaj](https://github.com/lumdukaj)
* Loreta Pajaziti [@LoretaPajaziti](https://github.com/LoretaPajaziti)

#### Used Resources

*[Steganography](https://www.ijcaonline.org/volume9/number7/pxc3871887.pdf),

