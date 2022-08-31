# Aircraft-Unity
A collection of scripts for rigidbody aircraft controllers in Unity. Also contains Camera scripts and rigidbody modifiers.

Aircraft controls - For controlling jets/airplanes. Requires a rigidbody component with the following values:
                        Mass: 5000
                        Drag: 1
                        Angular Drag: 2
                        Use Gravity: True
                        Is Kinematic: False
                        Interpolate: Interpolate
                        Collision Detection: Discrete

                    Every value on script can be changed, apart from the "Force Adaption" settings. These modify the input forces so the Control variables are not extreme values. Only change Force Adaption settings IF ABSOLUTELY NECESSARY. 

                    The throttle is emulative of a real aircraft throttle. It's cumulative, so you don't have to keep holding "W". 

                    Acceleration is how fast max throttle is reached.

                    Taxi settings are for when the aircraft is on the ground so you can rotate on the runway.



Camera follower - A simple camera follower script. 
                    Requires 2 GameObjects- One as a child on the vehicle as a target (marker), and one as a parent to the Camera (Cam). 
                    The cameras transform position should be zeroed.
                    High values in follow speed mean a slower camera.




Centre of Mass Editor - This script changes the location of the centre of mass of the aircraft. Unity will place the centre of mass at the centre point of the mesh,
                            but sometimes this isn't ideal. Use the vector 3 input to adjust it's location.
                            Z - forward
                            Y - Up
                            X - L/R



Toggle Mouse Lock - Set to spacebar, this toggles mouse lock on and off. 

