# KlazTween - Advanced Tweening Library for Unity

## Introduction
KlazTween is an advanced tweening library specifically designed for Unity. It provides developers with powerful, high-performance tools for creating smooth and customizable animations. By leveraging Unity's Job System and Burst Compiler, KlazTween ensures optimized performance for complex animation scenarios, making it ideal for game and UI development.

## Features
- **Versatile Tweening Options**: Supports tweening for floats, vectors, quaternions, and colors, allowing for a wide range of animation possibilities.
- **Job System Integration**: Utilizes Unity's Job System to handle multiple tweening operations concurrently, providing significant performance benefits.
- **Ease Functions**: Offers a variety of easing functions to simulate realistic motion dynamics.
- **Burst Compilation**: Optimized with Unity's Burst compiler for enhanced performance at runtime.
- **Callbacks and Delays**: Features include delays for tween start times and callbacks for tween start/end events, offering greater control over animation flows.

## Dependencies
KlazTween requires:
- **Unity 2020.3 LTS** or newer for stable API support.
- **Unity Mathematics Library** for complex mathematical calculations.
- **Unity Collections Library** for managing data efficiently.
- **Unity Jobs System** for performance (optional but recommended for heavy use cases).

## Installation
To install KlazTween in your Unity project:
1. Download the latest version of KlazTween from the [GitHub repository](https://github.com/klazapp/klaz-tween).
2. Import the package into your Unity project via `Assets > Import Package > Custom Package`.
3. Ensure that Unity's Mathematics, Collections, and Jobs packages are installed and updated through the Unity Package Manager.

## Usage
Here is how you can easily set up a tween using KlazTween:

```csharp
// Example of tweening a float from 0 to 1 over 2 seconds with a linear easing function
KlazTweenManager.Instance.DoTween(0f, 1f, 2f, value => transform.scale.x = value, 0f, EaseType.Linear);
```

### Advanced Usage:
For more complex scenarios, such as delaying the start of the tween or adding callbacks for completion:

```csharp
KlazTweenManager.Instance.DoTween(
    0f, 
    1f, 
    2f, 
    value => transform.scale.x = value, 
    0.5f, // Start after a 0.5-second delay
    EaseType.EaseInOutQuad, 
    () => Debug.Log("Tween started!"), 
    () => Debug.Log("Tween completed!")
);
```

## Customization
Extend the capabilities of KlazTween by:
- Modifying the `KlazTweenManager` to include new types of tweens.
- Adding or editing easing functions within the `Easing` class to create custom motion effects.

## To-Do List (Future Features)
- Integrate more closely with Unity's DOTS architecture to further enhance performance.
- Include path-following animations and more geometrically complex tween types.
- Develop a visual editor for creating and managing tweens directly within Unity.

## License
KlazTween is freely available under the MIT License, which permits both personal and commercial use, modification, and distribution of the code. See the LICENSE file for more details.
