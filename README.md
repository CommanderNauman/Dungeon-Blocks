# Dungeon Blocks
Dungeon Blocks is an open source procedural dungeon room creation system for Unity. 

Dungeon Blocks creates randomly generated rooms using two basic pieces: A Room object and a Block object. 

The Room object works by creating a 3D grid and assigning values to each position on the grid; it then builds the room out of Prefabs that are attached to the Room Script. 
Each prefab is assigned a Block component. On initialization, the prefab is randomly assigned different art pieces into different "sockets" to make each prefab look unique.

The package includes a camera and player prefab, and an example scene which should work. The player is controlled with WASD, "E" will activate doors, and holding down the right mouse button will allow you to rotate the camera. 

As this is my first open source project, I'll work on improving the readme by including examples and images, and by incorporating more features, like ladders. Also, in this package there aren't any materials, which I plan to update soon. In the mean time, simply create a base material and assign them to the prefabs. As the concept of 3D grid based systems is separate from the basic workings of unity, I'm curious about implementing this system to other games engines eventually.

Best of luck.

@MIT License.@Stephen Nauman @Stephennauman.com @2016
