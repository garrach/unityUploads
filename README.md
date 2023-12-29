

# ProjetUnityVR
![Repository Banner](https://64.media.tumblr.com/cd34a1bb6047b54d36a8204998c772ff/tumblr_pul9fipFgY1wnjxxqo1_1280.png)

## Description
ProjetUnityVR is a Unity project that leverages Virtual Reality (VR) for an immersive experience.
[Garrach Hazem, Douaa Fatnasi, Hafedh mellasi]
## Table of Contents
- [Features](#features)
- [Installation](#installation)
- [Usage](#usage)
- [Contributing](#contributing)
- [License](#license)
- [build-the-project](#build)

# Features

## VR Interaction
- Utilize the power of VR to create interactive experiences.
- Seamless object manipulation and environment interaction.

## Unity Integration
- Seamless integration with the Unity game development environment.
- Leverage familiar Unity workflows for efficient development.

## Immersive Environments
- Create and explore immersive virtual environments.
- Realistic graphics, 3D audio, and elements enhancing user presence.

## Character FPS/TPS Smooth Change
- Smooth transitions between first-person and third-person perspectives.
- Versatile gameplay experience for players.

## Installation
To set up the ProjetUnityVR project, follow these steps:
1. Clone the repository: `git clone https://github.com/garrach/unityUploads.git`
2. Open the project in Unity.
3. Configure VR settings (e.g., Oculus Rift, HTC Vive) in Unity.
4. Download the Files i provide in the link [here](https://drive.google.com/drive/folders/1rlV6CrD7PG4s8czQuCYRjpoXhmCrUiwX?usp=sharing).
5. Add background music and player FBX file.
6. Explore and build the project.

## Build
 ![build-the-project](https://github.com/garrach/unityUploads/blob/main/Assets/Art/c00.PNG)


## Usage
1. Open the Unity project in the Unity Editor.
2. Navigate to the `Assets/Scenes` folder and open the main scene.
3. Play the scene to experience the VR environment.
4. Interact with objects using VR controllers.
5. ![build-01-project](https://github.com/garrach/unityUploads/blob/main/Assets/Art/c00.PNG)
6. ![build-02-project](https://github.com/garrach/unityUploads/blob/main/Assets/Art/c01.PNG)
7. ![build-03-project](https://github.com/garrach/unityUploads/blob/main/Assets/Art/c002.PNG)
8. ![build-04-project](https://github.com/garrach/unityUploads/blob/main/Assets/Art/c003.PNG)

## JoySticks Script 
```c#
using UnityEngine;
using UnityEngine.EventSystems;

    public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
    {
        [Header("References")]
        public RectTransform joystickBase;
        public RectTransform joystickHandle;

        [Header("Joystick Parameters")]
        public float joystickRadius = 50f; // Adjust this based on your UI size

        private Vector2 joystickCenter;
        private Vector2 inputDirection = Vector2.zero;
        private bool isDragging = false;

        private void Start()
        {
            // Calculate the center of the joystick base
            joystickCenter = joystickBase.position;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnDrag(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            ResetJoystick();
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 pointerPosition = eventData.position;

            // Calculate the direction from the center of the joystick to the pointer
            inputDirection = (pointerPosition - joystickCenter).normalized;

            // Limit the input distance to the joystick's radius
            float inputMagnitude = (pointerPosition - joystickCenter).magnitude;
            inputDirection *= Mathf.Clamp(inputMagnitude / joystickRadius, -1f, 1f);

            // Update the position of the joystick handle
            joystickHandle.anchoredPosition = inputDirection * joystickRadius;

            isDragging = true;
        }

        private void ResetJoystick()
        {
            // Reset the input direction and joystick handle position
            inputDirection = Vector2.zero;
            joystickHandle.anchoredPosition = Vector2.zero;
            isDragging = false;
        }

        public Vector2 GetInputDirection()
        {
            return isDragging ? inputDirection : Vector2.zero;
        }
    }
````

## FootstepSounds
````c#
    public void PlayFootstepSound()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        if (audioSource != null )
        {
            audioSource.Play();
        }
    }
````
## Switch/changeView Play-mode
````c#
    public void changeView()
    {
       
            camFPS.SetActive(!camFPS.activeSelf);
            camTPS.SetActive(!camTPS.activeSelf);

            camFPS.transform.position = PlayerMesh.transform.position+Vector3.up;
            PlayerMesh.enabled = !PlayerMesh.enabled;
        

    }
````

## Contributing
Contributions are welcome! If you want to contribute to ProjetUnityVR, follow these steps:
1. Fork the repository.
2. Create a new branch: `git checkout -b feature/new-feature`
3. Make your changes and commit them: `git commit -m 'Add new feature'`
4. Push to the branch: `git push origin feature/new-feature`
5. Submit a pull request.

## License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

doudou
