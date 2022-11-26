# Tabletop Ising Model AI
WIP. Reinforcement learning agent trained to play the Ising model game (https://github.com/akoreman/Tabletop-Ising-Model-Game). Implemented using the Unity3D ML agents package.  

Train the model by running `mlagents-learn ball_config.yaml` in the assets/ML-config folder (or `mlagents-learn --force ball_config.yaml` if there is already output present in the folder).

### Currently Implemented:
* Set-up the game to be able to support agent interaction.
* Sends ball position, power-up positions and height of the moving platforms to the brain.
* Train the agent to find the red pickup on a small map with no moving platforms by using the game score as the reward.



