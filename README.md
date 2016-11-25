# WoT PogsIconSet

## Generating icons

### Preparation
1) copy `World_of_Tanks\res\scripts\item_defs` dir from game into `./resources/item_def` 
2) extract original contour icons from   `World_of_Tanks\res\packages\gui.pkg\gui\maps\icons\vehicle\contour` into `./resources/contour` 

3) extract atlases files from `World_of_Tanks\res\packages\gui.pkg\gui\flash\atlases\` into `./resources/atlases` 

4) copy all `*_vehicles.mo` files from `World_of_Tanks\res\text\lc_messages\`  into `./resources/texts` 

### Program setting
In Visual Studio, change property `gameVersion` inside `Properties/Settings.settings` dialog to current game version.


### Run the program
While running, there may be some erros about T-59g, hoever it should be fine.  
When some vehicle has long name, the program tries to find a shorter one inside *.mo* files. When this fail, it will look inside `./resources/shortNames.json` file. If, no suitable name is found there, it will generate new line into `./resources/shortNames.json` file.

To fix long names, you need to manualy edit **shortNames.json** file and re-run the program.