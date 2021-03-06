# Epicycle.Input-cs 0.1.4.0 [IN DEVELOPMENT]
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
* **Epicycle.Controllers**
  * Controllers are input devices that control a variable
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
* **Epicycle.Indicators**
  * Indicators are devices that can present a variable

## License
Apache License 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
Copyright 2015 Epicycle (http://epicycle.org)

## Release Notes
### Version 0.1 

* **Version 0.1.4.0** [IN DEVELOPMENT]

* **Version 0.1.3.0** [2015-09-14]
  * Keyboard:
    * Key events can now contain additional metadata
    * Some typos fixes, small API changes and more unit tests
  * Added a primitive Controllers infrastructre
  * Added a primitive Indicators infrastructre
  * Upgrading to Epicycle.Commons-cs.0.1.8.0
  * Upgrading to Epicycle.Math-cs.0.1.6.0
  * Upgrading to Epicycle.Geodesy-cs.0.1.4.0
 
* **Version 0.1.2.0** [2015-01-28]
  * Upgrading Epicycle.Math-cs: 0.1.4.0 => 0.1.5.0
  * Upgrading Epicycle.Geodesy-cs: 0.1.0.0 => 0.1.3.0

* **Version 0.1.1.0** [2015-01-26]
  * Upgrading Epicycle.Math-cs: 0.1.3.0 => 0.1.4.0

* **Version 0.1.0.0** [2015-01-25]
  * Moving the sensor infrastructure from Epicycle.Physics-cs
  * Adding an abstract keyboard infrastructure