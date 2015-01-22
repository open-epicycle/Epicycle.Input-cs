# Epicycle.Input-cs 0.1.0.0 [IN DEVELOPMENT]
Epicycle .NET input library. Includes: keyboard abstraction and keyboard state machines, sensor abstraction, sensor simulation and recording play-back

***Note***: *This library is in it's 0.X version, that means that it's still in active development and backward compatibility is not guaranteed!*

## Links
* NuGet package: https://www.nuget.org/packages/Epicycle.Input-cs
* Git repository: https://github.com/open-epicycle/Epicycle.Input-cs
* All Epicycle Git repositories: https://github.com/open-epicycle

## Main features
* Sensor infrastructre including recording playback

## Namespaces
* **Epicycle.Input**
  * General classes, enums and interfaces
* **Epicycle.Input.Keyboard**
  * Keyboard abstraction
  * Contains state machines for:
    * Simple pressable keys
    * Toggleable keys
    * Biderection movement using two keys
* **Epicycle.Input.Sensors**
  * Active sensor abstraction
* **Epicycle.Input.Sensors.GeoLocation**
  * Geo-location based sensor abstraction
* **Epicycle.Input.Sensors.Simulation**
  * Sensor simulation and recrding playback (both real-time and not)

## License
Apache License 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
Copyright 2015 Epicycle (http://epicycle.org)

## Release Notes
### Version 0.1 

* **Version 0.1.0** [IN DEVELOPMENT]
  * Moving the sensor infrastructure from Epicycle.Physics-cs
  * Adding an abstract keyboard infrastructure