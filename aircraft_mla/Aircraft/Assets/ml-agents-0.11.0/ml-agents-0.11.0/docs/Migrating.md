# Migrating

## Migrating from ML-Agents toolkit v0.10 to v0.11

### Important Changes
* The definition of the gRPC service has changed.
* The online BC training feature has been removed.
* The BroadcastHub has been deprecated. If there is a training Python process, all LearningBrains in the scene will automatically be trained. If there is no Python process, inference will be used.
* The Brain ScriptableObjects have been deprecated. The Brain Parameters are now on the Agent and are referred to as Behavior Parameters. Make sure the Behavior Parameters is attached to the Agent GameObject.
* Several changes were made to the setup for visual observations (i.e. using Cameras or RenderTextures):
  * Camera resolutions are no longer stored in the Brain Parameters.
  * AgentParameters no longer stores lists of Cameras and RenderTextures
  * To add visual observations to an Agent, you must now attach a CameraSensorComponent or RenderTextureComponent to the agent. The corresponding Camera or RenderTexture can be added to these in the editor, and the resolution and color/grayscale is configured on the component itself.

#### Steps to Migrate
* In order to be able to train, make sure both your ML-Agents Python package and UnitySDK code come from the v0.11 release. Training will not work, for example, if you update the ML-Agents Python package, and only update the API Version in UnitySDK.
* If your Agents used visual observations, you must add a CameraSensorComponent corresponding to each old Camera in the Agent's camera list (and similarly for RenderTextures).
* Since Brain ScriptableObjects have been removed, you will need to delete all the Brain ScriptableObjects from your `Assets` folder. Then, add a `Behavior Parameters` component to each `Agent` GameObject. You will then need to complete the fields on the new `Behavior Parameters` component with the BrainParameters of the old Brain.

## Migrating from ML-Agents toolkit v0.9 to v0.10

### Important Changes
* We have updated the C# code in our repository to be in line with Unity Coding Conventions.  This has changed the name of some public facing classes and enums.
* The example environments have been updated. If you were using these environments to benchmark your training, please note that the resulting rewards may be slightly different in v0.10.

#### Steps to Migrate
* `UnitySDK/Assets/ML-Agents/Scripts/Communicator.cs` and its class `Communicator` have been renamed to `UnitySDK/Assets/ML-Agents/Scripts/ICommunicator.cs` and `ICommunicator` respectively.
* The `SpaceType` Enums `discrete`, and `continuous` have been renamed to `Discrete` and `Continuous`.
* We have removed the `Done` call as well as the capacity to set `Max Steps` on the Academy. Therefore an AcademyReset will never be triggered from C# (only from Python). If you want to reset the simulation after a fixed number of steps, or when an event in the simulation occurs, we recommend looking at our multi-agent example environments (such as BananaCollector). In our examples, groups of Agents can be reset through an "Area" that can reset groups of Agents.
* The import for `mlagents.envs.UnityEnvironment` was removed. If you are using the Python API, change `from mlagents.envs import UnityEnvironment` to `from mlagents.envs.environment import UnityEnvironment`.


## Migrating from ML-Agents toolkit v0.8 to v0.9

### Important Changes
* We have changed the way reward signals (including Curiosity) are defined in the
`trainer_config.yaml`.
* When using multiple environments, every "step" is recorded in TensorBoard.
* The steps in the command line console corresponds to a single step of a single environment.
Previously, each step corresponded to one step for all environments (i.e., `num_envs` steps).

#### Steps to Migrate
* If you were overriding any of these following parameters in your config file, remove them
from the top-level config and follow the steps below:
  * `gamma`: Define a new `extrinsic` reward signal and set it's `gamma` to your new gamma.
  * `use_curiosity`, `curiosity_strength`, `curiosity_enc_size`: Define a `curiosity` reward signal
  and set its `strength` to `curiosity_strength`, and `encoding_size` to `curiosity_enc_size`. Give it
  the same `gamma` as your `extrinsic` signal to mimic previous behavior.
See [Reward Signals](Reward-Signals.md) for more information on defining reward signals.
* TensorBoards generated when running multiple environments in v0.8 are not comparable to those generated in
v0.9 in terms of step count. Multiply your v0.8 step count by `num_envs` for an approximate comparison.
You may need to change `max_steps` in your config as appropriate as well.

## Migrating from ML-Agents toolkit v0.7 to v0.8

### Important Changes
* We have split the Python packages into two separate packages `ml-agents` and `ml-agents-envs`.
* `--worker-id` option of `learn.py` has been removed, use `--base-port` instead if you'd like to run multiple instances of `learn.py`.

#### Steps to Migrate
* If you are installing via PyPI, there is no change.
* If you intend to make modifications to `ml-agents` or `ml-agents-envs` please check the Installing for Development in the [Installation documentation](Installation.md).

## Migrating from ML-Agents toolkit v0.6 to v0.7

### Important Changes
* We no longer support TFS and are now using the [Unity Inference Engine](Unity-Inference-Engine.md)

#### Steps to Migrate
* Make sure to remove the `ENABLE_TENSORFLOW` flag in your Unity Project settings

## Migrating from ML-Agents toolkit v0.5 to v0.6

### Important Changes

* Brains are now Scriptable Objects instead of MonoBehaviors.
* You can no longer modify the type of a Brain. If you want to switch
  between `PlayerBrain` and `LearningBrain` for multiple agents,
  you will need to assign a new Brain to each agent separately.
  __Note:__ You can pass the same Brain to multiple agents in a scene by
leveraging Unity's prefab system or look for all the agents in a scene
using the search bar of the `Hierarchy` window with the word `Agent`.

* We replaced the **Internal** and **External** Brain with **Learning Brain**.
  When you need to train a model, you need to drag it into the `Broadcast Hub`
  inside the `Academy` and check the `Control` checkbox.
* We removed the `Broadcast` checkbox of the Brain, to use the broadcast
  functionality, you need to drag the Brain into the `Broadcast Hub`.
* When training multiple Brains at the same time, each model is now stored
  into a separate model file rather than in the same file under different
  graph scopes.
* The **Learning Brain** graph scope, placeholder names, output names and custom
  placeholders can no longer be modified.

#### Steps to Migrate

* To update a scene from v0.5 to v0.6, you must:
  * Remove the `Brain` GameObjects in the scene. (Delete all of the
    Brain GameObjects under Academy in the scene.)
  * Create new `Brain` Scriptable Objects using `Assets -> Create ->
    ML-Agents` for each type of the Brain you plan to use, and put
    the created files under a folder called Brains within your project.
  * Edit their `Brain Parameters` to be the same as the parameters used
    in the `Brain` GameObjects.
  * Agents have a `Brain` field in the Inspector, you need to drag the
    appropriate Brain ScriptableObject in it.
  * The Academy has a `Broadcast Hub` field in the inspector, which is
    list of brains used in the scene.  To train or control your Brain
    from the `mlagents-learn` Python script, you need to drag the relevant
    `LearningBrain` ScriptableObjects used in your scene into entries
    into this list.

## Migrating from ML-Agents toolkit v0.4 to v0.5

### Important

* The Unity project `unity-environment` has been renamed `UnitySDK`.
* The `python` folder has been renamed to `ml-agents`. It now contains two
  packages, `mlagents.env` and `mlagents.trainers`. `mlagents.env` can be used
  to interact directly with a Unity environment, while `mlagents.trainers`
  contains the classes for training agents.
* The supported Unity version has changed from `2017.1 or later` to `2017.4
  or later`. 2017.4 is an LTS (Long Term Support) version that helps us
  maintain good quality and support. Earlier versions of Unity might still work,
  but you may encounter an
  [error](FAQ.md#instance-of-corebraininternal-couldnt-be-created) listed here.

### Unity API

* Discrete Actions now use [branches](https://arxiv.org/abs/1711.08946). You can
  now specify concurrent discrete actions. You will need to update the Brain
  Parameters in the Brain Inspector in all your environments that use discrete
  actions. Refer to the
  [discrete action documentation](Learning-Environment-Design-Agents.md#discrete-action-space)
  for more information.

### Python API

* In order to run a training session, you can now use the command
  `mlagents-learn` instead of `python3 learn.py` after installing the `mlagents`
  packages. This change is documented
  [here](Training-ML-Agents.md#training-with-mlagents-learn). For example,
  if we previously ran

  ```sh
  python3 learn.py 3DBall --train
  ```

  from the `python` subdirectory (which is changed to `ml-agents` subdirectory
  in v0.5), we now run

  ```sh
  mlagents-learn config/trainer_config.yaml --env=3DBall --train
  ```

  from the root directory where we installed the ML-Agents Toolkit.

* It is now required to specify the path to the yaml trainer configuration file
  when running `mlagents-learn`. For an example trainer configuration file, see
  [trainer_config.yaml](../config/trainer_config.yaml). An example of passing
  a trainer configuration to `mlagents-learn` is shown above.
* The environment name is now passed through the `--env` option.
* Curriculum learning has been changed. Refer to the
    [curriculum learning documentation](Training-Curriculum-Learning.md)
    for detailed information. In summary:
  * Curriculum files for the same environment must now be placed into a folder.
    Each curriculum file should be named after the Brain whose curriculum it
    specifies.
  * `min_lesson_length` now specifies the minimum number of episodes in a lesson
    and affects reward thresholding.
  * It is no longer necessary to specify the `Max Steps` of the Academy to use
    curriculum learning.

## Migrating from ML-Agents toolkit v0.3 to v0.4

### Unity API

* `using MLAgents;` needs to be added in all of the C# scripts that use
  ML-Agents.

### Python API

* We've changed some of the Python packages dependencies in requirement.txt
  file. Make sure to run `pip3 install -e .` within your `ml-agents/python`
  folder
  to update your Python packages.

## Migrating from ML-Agents toolkit v0.2 to v0.3

There are a large number of new features and improvements in the ML-Agents
toolkit v0.3 which change both the training process and Unity API in ways which
will cause incompatibilities with environments made using older versions. This
page is designed to highlight those changes for users familiar with v0.1 or v0.2
in order to ensure a smooth transition.

### Important

* The ML-Agents toolkit is no longer compatible with Python 2.

### Python Training

* The training script `ppo.py` and `PPO.ipynb` Python notebook have been
  replaced with a single `learn.py` script as the launching point for training
  with ML-Agents. For more information on using `learn.py`, see
  [here](Training-ML-Agents.md#training-with-mlagents-learn).
* Hyperparameters for training Brains are now stored in the
  `trainer_config.yaml` file. For more information on using this file, see
  [here](Training-ML-Agents.md#training-config-file).

### Unity API

* Modifications to an Agent's rewards must now be done using either
  `AddReward()` or `SetReward()`.
* Setting an Agent to done now requires the use of the `Done()` method.
* `CollectStates()` has been replaced by `CollectObservations()`, which now no
  longer returns a list of floats.
* To collect observations, call `AddVectorObs()` within `CollectObservations()`.
  Note that you can call `AddVectorObs()` with floats, integers, lists and
  arrays of floats, Vector3 and Quaternions.
* `AgentStep()` has been replaced by `AgentAction()`.
* `WaitTime()` has been removed.
* The `Frame Skip` field of the Academy is replaced by the Agent's `Decision
  Frequency` field, enabling the Agent to make decisions at different frequencies.
* The names of the inputs in the Internal Brain have been changed. You must
  replace `state` with `vector_observation` and `observation` with
  `visual_observation`. In addition, you must remove the `epsilon` placeholder.

### Semantics

In order to more closely align with the terminology used in the Reinforcement
Learning field, and to be more descriptive, we have changed the names of some of
the concepts used in ML-Agents. The changes are highlighted in the table below.

| Old - v0.2 and earlier | New - v0.3 and later |
| --- | --- |
| State | Vector Observation |
| Observation | Visual Observation |
| Action | Vector Action |
| N/A | Text Observation |
| N/A | Text Action |
