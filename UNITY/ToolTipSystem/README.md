# ![XTM](https://cdn.discordapp.com/attachments/877432819746488380/888326982499196938/Logo_01.png) Kiwi ToolTip

Kiwi ToolTip is a UI prefab for Unity Engine. Used to give a ToolTip context menu for specific UI elements.

## Requirements
***Text Mesh Pro must be installed for Kiwi ToolTip to work properly.***

---

## Installation

>- **1 -** Right clic in the Assets folder in the Project panel of Unity Editor, choose Import Package and Custom Package.
>-  **2 -** Go to the package directory and open the package.
>-  **3 -** Check all components of the selected package and clic on Import.

---

## Usage

### First steps
1. Drag and drop the "MainCamera" object in the "Render Camera" area in the canvas component of ToolTipCanvas.
2. Select the "Header" or "Content" object in the hierarchy and install the Text Mesh Pro plugin if Unity offers it. If you have already installed the Text Mesh Pro plugin, go to the next step.
3. Check the position of the object "NewToolTip" in the hierarchy. This object must be the last one in the "ToolTipCanvas".
4. Press the "play" button in the Unity editor to test the prefab.

### Add ToolTip to an object
1. Select the object to which you want to add a tooltip.
2. Drag and drop the script "KiwiToolTipTrigger" in the inspector.
3. Replace the text fields "Header" and "Content" by what you want to display in the tooltip.

---

## Documentation

### Kiwi Tool Tip
![Kiwi Tool Tip](https://cdn.discordapp.com/attachments/877432819746488380/888324292163563560/unknown.png)
>- ***Header*** : Text Mesh Pro object used to give a title to the tooltip.
>- ***Content*** : Text Mesh Pro object used to give a description to the tooltip.
>- ***Layout Element*** : Used to limit the length of the tooltip. Its value can be modified in the "Layout Element" component.

### Kiwi Tool Tip System
![Kiwi Tool Tip System](https://cdn.discordapp.com/attachments/877432819746488380/888324921665683456/unknown.png)
>- ***Kiwi Tool Tip*** : Drag and drop the "KiwiToolTip" object from the hierarchy. Used to assign the tooltip to the main script.
>- ***Distance To pointer*** : Assign the distance between the cursor and the tooltip panel.
>- ***Character Wrap Limit*** : Assign the maximum number of characters on a single line.
>- ***Orientation*** : Determines the position of the tooltip panel in relation to the cursor.

### Kiwi Tool Tip Trigger
![Kiwi Tool Tip Trigger](https://cdn.discordapp.com/attachments/877432819746488380/888325159675633674/unknown.png)
>- ***Header*** : Text box to give a title to the tooltip.
>- ***Content*** : Text box to give a description to the tooltip.

---