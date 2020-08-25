# Airplane-Game-in-Unity3d-using-Reinforcement-Learning
# Introduction
This Project is a convergence of AI and Video Games. This game is developed using Unity Game Engine along with aid from their Built in ML Agents Package. 
In this game you race along with other AI controlled airplanes. The game provides two racing environments to the player, Desert and Snow. The desert level was designed to be as close to a real desert with rocks and canyons and sand, however in the snow level we let our imagination go wild. The goal is to fly through each checkpoint and be the first to reach the final checkpoint to finish first place.



# Training
The game provides the player with 3 difficulty choices which are Easy, Normal and Hard. For each of these difficulties a seperate neural network has been trained and inserted into the agents. A network of 1 hidden layer with 128 nodes in each layer was setup in unity. We found this configuration to be the most optimal, however more experimentation might provide a better trained network. The network was trained on the desert level with 4 seperate copies of the desert level with 4 agents in each of them, hence at once 16 agents were being trained. During training curriculum training was also implemented with 5 different learning levels. To encourage the agents to fly throught the checkpoints only a sphere of certain radius is created around the checkpoints. Initially the radius is such that the sphere is as large as the checkpoint and even if the agents fly throught the sphere but not the checkpoint they receive certain reward. When a certain threshold of reward is reached then the radius of the spheres are decreased and this continues till the agents reach the final level in which the radius is 0, which effectively means the sphere is non existent and the agents are actually flying through the checkpoint. The number of copies for the level can certainly be increased. The same trained networks is also used in the Snow level with pleasently positive results with performance of the agents being as superb as in the desert level.

# Level Design and Logic
The assets were created in Blender which includes the airplanes and their individual parts, checkpoints, rocks, etc. This game does not include the process of landing and takeoff in a plane for keeping the project as simple as possible and mainly focusing on the training part and the agents. The inputs available to the player are moving down(W Key), moving up (S key), turn left (A Key), turn right (D Key), boosting (SPACE Key), pausing button (ESC Key). Additionally controller support is also added if the player wishes to use one. If the player does not fly throught the next checkpoint in 15 seconds or collides with an obstacle the plane in reset to the last checkpoint through which the player flew through. There is a total of 61 Ray perception lines emerging from each agent airplane as seen in Fig 4. There are 3 to check and observe the aircraft velocity, another 3 to calculate where the next checkpoint is and more 3 find the orientation of the next checkpoint. For the purpose of looking forward and pitching upward there are 4 rays: 2 of which detect whether the airplane has passed through a checkpoint or collided with an object, 1 ray calculates whether to pitch up or go straight forward and the last ray calculates the distances to objects in the way. These 4 rays are distributed along 3 different angles: 60o, 90o and 120o, hence in reality there are 12 rays for the purpose of calculation of whether the agent airplane has to pitch up or go forward. Similarly, there are 12 rays for the calculations of pitching down or go straight forward. Along with these 33 rays in total there are 28 more rays which are situated along the front of the airplane along various different angles. 2 rays are used to detect whether the airplane has passed through a checkpoint or collided with an object, 1 ray to calculate whether to go forward or not and the last ray to calculates the distances to objects in the way. These 4 rays are distributed along 7 different angles: 60o, 70o, 80o, 90o, 100o, 110o, 120o. This results in there being 28 rays for the calculation looking in the center. Overall, we have 3 + 3 + 3 + 12 + 12 + 28 = 61 Ray casts around each AI airplane for collecting observations in the environment.

# Notes
This project was created in unity3d version 2019.3.3f1 along with the ML- Agents version (0.11).

