# Revit-API-ViewFiltersFactory

## Description

Revit API IExternalApplication plugin executable via PushButton in the Revit Ribbon Tab Panel "BH Plugins".
The application carries out the following tasks:
* Creates set of View Filters in the Revit Active View each one named after a different frame family type name.
* Assigns to each View Filter a different colour based on a selected colour palette
* Assigns all the frame instances to the corresponding View Filter based on the corresponding family type name.

The automated creation of such View Filters allow to display quickly and visually the differences between the types of frame section assigned to the elements visible in the view.

## UML Classes Diagram

![UML Diagram](https://github.com/GCRA101/Revit-API-ViewFiltersFactory/blob/main/UML%20Diagrams/Classes%20Diagram.png?raw=true)

## Install

To install the plugin, first make sure to have no Revit instance running on the computer and follow the steps below:
1. Build the code
2. Copy and paste the .addin file in the folder c:\Users\UserName\AppData\Roaming\Autodesk\Revit\Addins\Version\
3. Create the folder ViewFiltersFactory\ in the folder at point 2. and place in it a copy of the assembly file (ViewFiltersFactory.dll)
4. Unblock both the .addin and the .dll file (right click on it -> Properties -> Check the checkbox "Unblock")
5. Run Revit
6. Find the plugin button in the BH Plugins Ribbon Tab
