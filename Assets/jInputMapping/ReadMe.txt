

### jInput Mapping ###

Add input mapping system in your game !
 key Assign / Key Config



===== First play the Demo =====

It is require Unity 5.3 or higher.
There are all imported files located in 'jInputMapping'folder.
All import operation successful completion, you can play the Demo scene and confirm operation.
there are two types of the demo in project, UI is made of UnityGUI or 3D objects.
The UnityGUI version has some additional elements such as scrollbar function and it would be better to use this if you do not have some reason.
3D object version is used when your game does not use UnityGUI for some reason.


===== How to use in your game =====

after imported,

The HierarchyWindow in Unity, copy and add 'jInputMappingSet'object from Demo
to the scene that your game player will set input mapping.
First of all you need to assign the basic settings in the InspectorWindow of this for fit your game.
The most number of players, default keys, and so on.(described later)

When basic settings have completed, Play the scene once and check for whether any errors
appear or not.

The basic usage in script such as move the character, move of Cube in Demo and 'DemoCube.cs' as reference.
Input setting elements are stored array 'Mapper.InputArray[]' (case of Player1).
To write 'jInput.Get...' usually write Input.Get....
Example: var v = jInput.GetAxis(Mapper.InputArray[0]);
This get the input 0th array of Player1.

Can be mapping to any, JoystickAxis, JoystickButton, KeyBoard, Mouse.
And can be perception to any, GetKey&Button, KeyUp, KeyDown, GetAxis&Raw.


========== Update to a new Version ==========

Immediately after you have imported the new Version, please be sure to open once the scene that is put 'jInputMappingSet'.
The changes of the update will be adapted to function.
Please note if you do not do this, there is a possibility that does not work normally, because the part of update disagree the data until that time.


===============================================================


 * Most of basic settings are automatically by simply assign to jinputSettings component
   in the InspectorWindow on jInputMappingSet object.

	-'Menu Item Headings' set the number and name of items of key mapping.
		Items at jInputMappingSet/MainWindow/InMapperMenuItems in the HierarchyWindow
		will be added or deleted automatically according to the number.
		If set to less than the number of be actually used elements in InputArray[],
		it is an error when you play the scene.
		For example, the Demo scene is using 0-6 in InputArray[], so it is an error
		that less than 7.

	-'Max Players in Same Place' is maximum number of players to connection
	 	one game program in the same place at the same time.
	 	For example, frends meet in one room and connect some GamePad
	 	to the one game machine and not use internet.
		Each 2-4 Players input setting elements are stored array
		'Mapper.InputArray2p[]' 'Mapper.InputArray3p[]' 'Mapper.InputArray4p[]'.
		Example: var jump = jInput.GetKeyDown(Mapper.InputArray2p[5]);
		This get the input 5th array of Player2.

	-'Default Input Mapping' set the default key that used when a game player
	 	do not set the input mapping yet.
		To write KeyCode name
		Example: A / LeftShift / Mouse0 / Joystick1Button1
		Or Joystick Axis regularity name
		'Joystick1Axis1'...'Joystick1Axis20'...'Joystick4Axis20' add the end of it '+'or'-'.
		Example: Joystick1Axis1+
		MouseWheel same so 'MouseWheel' add the end of it '+'or'-'.
		Example: MouseWheel-
		Be careful in case of add the end of it '+'or'-'.
		Actually play the scene and mapping the key, you can know the setting name of it.
		In addition, you can set the default keys by press the actual key in the same way
		as a game player to Input mapping, or also set by select from the drop-down list.
		Mistake of the upper-lowercase is fixed in the automatic.
		And if the setting name is incorrect, it is suggested in error log when you play the scene.
		Just after setting the default key, make sure no more errors by Play scene once.

	- 'Deal with Same Key' ('Preclude Same Mapping' in the using 3D objects version)
		is whether to make can not duplicate mapping of the same key on some items.
		If 'Preclude Same Mapping ' is checked, a game user cannot assign the same key
		 in another separate item.
		'Same KeyName Color' is the color that attached to the input name text
		when the same key has been assigned to some separate items.
		This color item is only towards the using UnityGUI version of jInput.
		In the case of the using 3D objects version, there are the same usage color item
		in the Inspector window of each input item.

	-'Unuseble Mapping' set the keys that a game user can not be used in Input mapping.
		For example, if you want to set a fixed special behavior to the Return key,
		you may want to that key is not be mapped another operation by a game user.

	-'Exclude Device' is used when you want to be mapped GamePad only, or keyboard only.
		Mouse and touch operation is not excluded because these are necessary as insurance
		in any case, such as a player input device is broken and emergency operate for
		opening the mapping window.
		As an applied using, you set 2Players at 'Max Players in Same Place' in your
		single player game and set to exclude keyboard from Player1 and set
		to exclude joystick from Player2, so it allows that a game player can have
		both input mapping a GamePad and a keyboard separately.

	-'UI Operation Settings' is specified the key items that are used to move direction
		in the mapping window.
		And operation of move direction in UnityGUI are also same set by this.
		UGUI Submit, UGUI Cancel designate the operation in UnityGUI, too.
		If there is already EventSystem of UnityGUI in the scene, there is a need to stop
		the different behave from it, or you can also stop the operation of UnityGUI from jInput.
		It is necessary to be explained that including specification of UnityGUI itself,
		so please look at the explanation site at last of this text.
		'Exclude Decision Fnc' specify to exclude input from the decision work
		in the mapping window.
		The direction keys and the cancel key that are written in above
		will be checked automatically and be excluded from the decision work.
		Almost all keys are adapted to decision work in the mapping window
		because the player operate easily.
		For instance, if the direction keys also work as decision, a game player
		can not move the cursor by those keys because it would determine
		at the same time as direction move.
		So the key item have such a function must be excluded from the decision work.
		As operation of key item in your game, there is a need to be checked the key item
		that work strangely by the decision to work in the mapping window.
		
	-'Use Esc Decided Behavior' is whether to use the Escape key as determined
		cancel operation in the mapping window.
		If it is checked, Escape key is assigned to operate such as cancel waiting key
		input for mapping, close the window of confirmation.
		And if you check in the item 'SetActive to Open' in above,
		the mapping window is opened and closed by the Escape key.
		When it is not check, these behaviors does not take.
		But in either case Escape key is special part and can not be mapped by a game user permanently.

	-'jInput Open/Close Settings' is set the way of opening and closing the mapping window.
		This setting here is different in how to open and close the mapping window
		in any way your game, so look at explanation site at last of this text.

	-'Axes Advance Settings' set DeadZone, Gravity, Sensitivity of Axes.
		These are applied all Axis input.
		Those values are hold as it is after Play the scene unlike InspectorWindow normally,
		so it is a good idea to the optimal settings while actually moving your game character.

 * SaveData is made encrypted binary file in standalone and in Unity editor.
	'SaveData'folder is created in same directory of Unity game execution,
	and be saved as 'InputMapping.dat' in the folder.
	It Not use Directory.
	It is also easy that a game player holds different input mapping data files in a PC game.
	On other platforms, it is saved by PlayerPrefs of Unity compliant.

 * It can be chosen whether to use the behavior of scrolling menu with UnityGUI version
    by checking 'Use VerticanScroll' of UGUIMenuVerticalScroll.cs in
    jInputMappingSet/MainWindow gameObject.



Please look at the site that More!
http://kakatte.webcrow.jp/jinput/index_en.html

producer: Myouji



This pacage embed OrvitronFont in accordance with SIL Open Font License v1.10.

