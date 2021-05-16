# Projekti I : Siguria e të dhënave

##### Detyra 27: Steganografia, fshehja e informatës në fotografi duke ndryshuar 3 bits brenda një pixel.


## Hyrje
Steganografia është arti dhe shkenca mbi fshehjen e informatës përmes fshehjes së mesazhit brenda një mesazhi tjetër. Steganografia funksionon në atë mënyrë që bitat më pak të rëndësishëm të të dhënave të ruajtura në files, zëvendësohen me bita të tjerë të informatës tonë. Informata e fshehur mund të jetë teskt, fotografi, tekst i shifruar. 

Në rastin tonë, informata që do të fshehet është tekst dhe fshehja do të bëhet në bitat më pak signifikant brenda pixelave të fotografisë.


---

## Sqarimi
1. Kuptimi i konceptit të detyrës - fshehja e informatës në fotografi duke ndryshuar 3 bits brenda një pixeli të fotografisë
    
    * Një pixel i fotografisë përbëhet nga 3 bytes, përkatësisht elementet RGB (secili nga 1 byte), kurse 1 byte përbëhet nga 8 bits.
    * Ndryshimi i 3 bitave brenda një pixeli nënkupton që për secilin byte brenda 1 pixeli duhet të ndryshohet 1 bit. 

2. Fshehja e tekstit brenda fotografisë

    * Iterimi në çdo pixel të fotografisë dhe ndarja e vlerave RGB si integer.
    * Për çdo RGB, biti më pak signifikant bëhet 0. Këta bita do të përdoren për fshehje të shkronjave të tekstit.

3. Nxjerrja e tekstit nga fotografia

    * Kontrollohet secili pixel derisa të hasim 8 zero të njëpasnjëshme. 
    * Merret biti më pak signifikant i secilit element të pixelit (R, G, B), shndërrohen në shkronjë dhe shkronjat bashkangjiten në tekst.  



---

## Kontribuesit
Detyra është realizuar nga tre studentë të drejtimit Inxhinieri Kompjuterike në Fakultetin e Inxhinierisë Elektrike dhe Kompjuterike - Universiteti i Prishtinës.

* Ora Vrapcani [@oravrp](https://github.com/oravrp)
* Lum Dukaj [@lumdukaj](https://github.com/lumdukaj)
* Loreta Pajaziti [@LoretaPajaziti](https://github.com/LoretaPajaziti)

## Resurset e shfrytëzuara 

   * [Steganography](https://www.ijcaonline.org/volume9/number7/pxc3871887.pdf),

